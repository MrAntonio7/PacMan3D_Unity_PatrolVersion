using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointEatGhost : MonoBehaviour
{
    private string playerTag = "Player"; // Tag del jugador
    public GameObject player;
    public GameObject[] enemys;
    public int pointsToAdd = 5; // Puntos a sumar cuando el objeto colisiona con el jugador
    private bool pointCatched;
    public float timeVulnarability;

    private void Start()
    {
        pointCatched = false;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag) && !pointCatched)
        {
            GameManager.Instance.audioCanEat.Play();
            GameManager.Instance.audioWalk.Stop();
            GameManager.Instance.AddScore(pointsToAdd);
            GameManager.Instance.AddPointCollect();
            player.GetComponent<PlayerController>().pacmanEatGhost = true;
            pointCatched=true;
            StartCoroutine(ResetTriggerAfterDelay(timeVulnarability)); // Llamar a la coroutine
            for (int i = 0; i < enemys.Length; i++)
            {
                enemys[i].GetComponentInParent<EnemyController>().followOrFlee = false;
                enemys[i].GetComponentInParent<EnemyController>().vulnerableGhost = true ;
                enemys[i].GetComponent<ColorController>().StartVulnerableGhost();
            }
            pointCatched = true;
            gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    private IEnumerator ResetTriggerAfterDelay(float time)
    {
        yield return new WaitForSeconds(time); // Esperar el tiempo especificado
        player.GetComponent<PlayerController>().pacmanEatGhost = false; // Cambiar de vuelta a false
        GameManager.Instance.audioCanEat.Stop();
        GameManager.Instance.audioWalk.Play();
        for (int i = 0; i < enemys.Length; i++)
        {
            if (!enemys[i].GetComponentInParent<EnemyController>().deadGhost)
            {
                enemys[i].GetComponentInParent<EnemyController>().followOrFlee = true;
                enemys[i].GetComponentInParent<EnemyController>().vulnerableGhost = false;
                enemys[i].GetComponent<ColorController>().StopVulnerableGhost();
            }
            
        }
    }
}
