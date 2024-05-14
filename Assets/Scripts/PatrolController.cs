using UnityEngine;
using UnityEngine.AI;

public class PatrolController : MonoBehaviour
{
    public Transform[] points;
    public Transform player; // Referencia al jugador
    public NavMeshAgent agent;
    public int destPoint = 0;
    public bool chasing = false; // Estado de persecución

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        GotoNextPoint();
    }

     public void GotoNextPoint()
    {
        if (points.Length == 0 || chasing) return;

        // Asigna el destino al NavMesh Agent de manera aleatoria
        destPoint = Random.Range(0, points.Length);
        agent.destination = points[destPoint].position;
    }

    void Update()
    {
        // Checa si el agente está cerca del destino para moverlo a un nuevo punto
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            GotoNextPoint();
    }


}