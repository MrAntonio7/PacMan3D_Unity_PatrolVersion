using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportation : MonoBehaviour
{
    public Transform teleportDestination; // Punto de destino para la teleportación
    private string playerTag = "Player"; // Tag del jugador para identificarlo

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            // Teletransportar el jugador al punto de destino
            other.gameObject.GetComponent<PlayerController>().agent.Warp(teleportDestination.position);

            // Ajustar la rotación si es necesario (opcional)
            //other.transform.rotation = teleportDestination.rotation;
        }
    }
}