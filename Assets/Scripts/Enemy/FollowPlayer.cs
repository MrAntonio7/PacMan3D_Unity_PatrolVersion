using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowPlayer : MonoBehaviour
{
    public Transform player; // Referencia al objeto que representa al jugador
    private NavMeshAgent agent; // Referencia al NavMeshAgent del NPC
    public float followDistance = 2.0f; // Distancia m�nima para comenzar a seguir al jugador
    public bool shouldFollow = true; // Bandera para activar o desactivar el seguimiento

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    void Start()
    {

         

    }

    void Update()
    {
        // Verificar si el agente debe seguir al jugador
        if (shouldFollow && player != null)
        {
            // Establecer el destino como la posici�n del jugador
            agent.SetDestination(player.position);
        }
    }

    // M�todo para detener el seguimiento
    public void StopFollowing()
    {
        shouldFollow = false; // Desactivar el seguimiento
    }

    // M�todo para comenzar a seguir al jugador
    public void StartFollowing()
    {
        shouldFollow = true; // Activar el seguimiento
    }
}