using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AreaGhost : MonoBehaviour
{
    public Transform playerStartPosition; // La posici�n a la que se debe devolver el player

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Si el player intenta entrar, revierte su posici�n
            other.GetComponent<NavMeshAgent>().Warp(playerStartPosition.position);
            Debug.Log("Player intent� entrar en el BoxCollider y fue rechazado.");
        }
    }
}
