using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public float speedX = 5f; // Velocidad de movimiento en el eje x
    public float speedY = 5f; // Velocidad de movimiento en el eje y
    private Rigidbody rb; // Referencia al Rigidbody
    private Animator animator; // Referencia al Animator
    private string boolName = "walking"; // Nombre del booleano en el Animator
    public bool pacmanEatGhost;
    public bool vulnerablePlayer;
    public GameObject respawn;
    public float moveX,moveY;
    public GameObject[] enemys;
    public NavMeshAgent agent;
    public GameObject ghostPatrol;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        enemys = GameObject.FindGameObjectsWithTag("Ghost");
        vulnerablePlayer = true;
        pacmanEatGhost = false;
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        //Debug.Log(rb.velocity.magnitude);
        //if (rb.velocity.magnitude > 0)
        //{
        //    animator.SetBool(boolName, true);
        //}
        //else
        //{
        //    animator.SetBool(boolName, false);
        //}
        
    }
    void FixedUpdate()
    {   
        moveX = -Input.GetAxis("Horizontal") * speedX * Time.deltaTime; // Movimiento horizontal
        moveY = -Input.GetAxis("Vertical") * speedY * Time.deltaTime; // Movimiento vertical


        // Determinar si el objeto se está moviendo
        bool isMoving = moveX != 0 || moveY != 0;
        animator.SetBool(boolName, isMoving); // Ajustar el booleano del Animator

        // Rotación y movimiento en el eje x
        if (moveX < 0)
        {
            SetRotationAndMove(Quaternion.Euler(0, -90, 0), Mathf.Abs(moveX));
        }
        else if (moveX > 0)
        {
            SetRotationAndMove(Quaternion.Euler(0, 90, 0), Mathf.Abs(moveX));
        }

        // Rotación y movimiento en el eje y
        if (moveY < 0)
        {
            SetRotationAndMove(Quaternion.Euler(0, 180, 0), Mathf.Abs(moveY));
        }
        else if (moveY > 0)
        {
            SetRotationAndMove(Quaternion.Euler(0, 0, 0), Mathf.Abs(moveY));
        }
    }

    // Función para ajustar rotación y movimiento
    private void SetRotationAndMove(Quaternion rotation, float distance)
    {
        transform.rotation = rotation;
        transform.Translate(new Vector3(0, 0, distance));
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ghost"))
        {
            if (!other.gameObject.GetComponent<EnemyController>().vulnerableGhost && !other.gameObject.GetComponent<EnemyController>().deadGhost)
            {
                GameManager.Instance.audioDead.Play();
                GameManager.Instance.life--;
                vulnerablePlayer = false;
                speedX = 0;
                speedY = 0;
                for (int i = 0; i < enemys.Length; i++)
                {
                    enemys[i].GetComponent<EnemyController>().StopGhost();
                }
                StartCoroutine(VulnerableTimer());
            }
            else if(other.gameObject.GetComponent<EnemyController>().vulnerableGhost && !other.gameObject.GetComponent<EnemyController>().deadGhost)
            {
                other.gameObject.GetComponent<EnemyController>().DeadGhost();
                GameManager.Instance.AddScore(10);
                GameManager.Instance.audioEatGhost.GetComponent<OverlappingAudio>().PlaySound();
            }

        }
        if (other.CompareTag("Proyectil"))
        {
            GameManager.Instance.audioDead.Play();
            GameManager.Instance.life--;
            vulnerablePlayer = false;
            speedX = 0;
            speedY = 0;
            for (int i = 0; i < enemys.Length; i++)
            {
                enemys[i].GetComponent<EnemyController>().StopGhost();
            }
            ghostPatrol.GetComponent<ChaseController>().canShoot = false;
            StartCoroutine(VulnerableTimer());
        }
    }

    private IEnumerator VulnerableTimer()
    {
        //StartCoroutine(SwitchMaterialPeriodically());
        yield return new WaitForSeconds(2f);
        vulnerablePlayer=true;
        for (int i = 0; i < enemys.Length; i++)
        {
            enemys[i].GetComponent<EnemyController>().ResumeGhost();
        }
        speedX = 5;
        speedY = 5;
        agent.Warp(respawn.transform.position);
        ghostPatrol.GetComponent<ChaseController>().canShoot = true;
    }

}