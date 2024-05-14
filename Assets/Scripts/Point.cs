using UnityEngine;

public class Point : MonoBehaviour
{
    private string playerTag = "Player"; // Tag del jugador
    public int pointsToAdd = 1; // Puntos a sumar cuando el objeto colisiona con el jugador

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            // Sumar puntos al Game Manager
            GameManager.Instance.AddScore(pointsToAdd);
            GameManager.Instance.AddPointCollect();

            // Destruir el objeto punto
            Destroy(gameObject);
        }
    }
}