using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FleeFromPlayer : MonoBehaviour
{
    public Transform player; // Referencia al objeto que representa al jugador
    private NavMeshAgent agent; // Referencia al NavMeshAgent del NPC
    public float fleeDistance = 1f; // Distancia para la que el NPC debe huir del jugador
    public bool shouldFlee = true; // Bandera para activar o desactivar la huida
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    void Start()
    {

        

    }

    void Update()
    {
        if (shouldFlee && player != null)
        {
            // Calcular la dirección opuesta al jugador
            Vector3 directionFromPlayer = (transform.position - player.position).normalized;

            // Establecer el destino a cierta distancia lejos del jugador
            Vector3 fleeTarget = transform.position + directionFromPlayer * fleeDistance;

            // Establecer el destino del agente
            agent.SetDestination(fleeTarget);
        }
    }

    // Método para detener la huida
    public void StopFleeing()
    {
        shouldFlee = false; // Desactivar la huida
    }

    // Método para comenzar a huir
    public void StartFleeing()
    {
        shouldFlee = true; // Activar la huida
    }
}