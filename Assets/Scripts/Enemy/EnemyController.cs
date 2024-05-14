using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float velocity;
    private NavMeshAgent agent;
    private FollowPlayer followPlayer;
    private FleeFromPlayer fleePlayer;
    public bool followOrFlee; // Bandera para controlar el estado del componente 
    public GameObject respawnGhost;
    public Transform player; // Referencia al objeto que representa al jugador
    public bool deadGhost;
    public bool vulnerableGhost;

    // Start is called before the first frame update
    void Start()
    {
        vulnerableGhost = false;
        deadGhost = false;
        agent = GetComponent<NavMeshAgent>();
        followPlayer = GetComponent<FollowPlayer>();
        fleePlayer = GetComponent<FleeFromPlayer>();
        followOrFlee = true;
        fleePlayer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    // Alternar el estado del componente
        //    followOrFlee = !followOrFlee;

        //}

        // Activar o desactivar el componente según la bandera
        followPlayer.enabled = followOrFlee;
        fleePlayer.enabled = !followOrFlee;


    }

    public void StopGhost()
    {
        agent.speed = 0;
        

    }
    public void ResumeGhost()
    {
        agent.Warp(respawnGhost.transform.position);
        agent.speed = GameManager.Instance.ghostVelocity;
        
    }
    public void DeadGhost()
    {
        deadGhost = true;
        agent.Warp(respawnGhost.transform.position);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SafeAreaGhost") && deadGhost)
        {
            Debug.Log("Fantasma curado " + gameObject.name);
            agent.SetDestination(player.position);
            agent.speed = GameManager.Instance.ghostVelocity;
            deadGhost = false;
        }
    }

}
