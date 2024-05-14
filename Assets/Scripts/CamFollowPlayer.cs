using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowPlayer : MonoBehaviour
{
    public Transform player; // Referencia al Transform del jugador
    public Vector3 offset = new Vector3(0, 7, 7); // Desplazamiento para la cámara
    public float smoothSpeed = 0.125f; // Velocidad para suavizar el movimiento

    void LateUpdate()
    {
        // Calcular la posición deseada para la cámara usando un desplazamiento
        Vector3 desiredPosition = player.position + offset;

        // Suavizar el movimiento para que la cámara siga al jugador suavemente
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Establecer la posición de la cámara
        transform.position = smoothedPosition;

        // Opcionalmente, fijar la rotación para que la cámara mire siempre hacia adelante o en una dirección específica
        transform.rotation = Quaternion.Euler(140, 0, -180); // Mantener una orientación fija
    }
}