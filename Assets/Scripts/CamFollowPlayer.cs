using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowPlayer : MonoBehaviour
{
    public Transform player; // Referencia al Transform del jugador
    public Vector3 offset = new Vector3(0, 7, 7); // Desplazamiento para la c�mara
    public float smoothSpeed = 0.125f; // Velocidad para suavizar el movimiento

    void LateUpdate()
    {
        // Calcular la posici�n deseada para la c�mara usando un desplazamiento
        Vector3 desiredPosition = player.position + offset;

        // Suavizar el movimiento para que la c�mara siga al jugador suavemente
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Establecer la posici�n de la c�mara
        transform.position = smoothedPosition;

        // Opcionalmente, fijar la rotaci�n para que la c�mara mire siempre hacia adelante o en una direcci�n espec�fica
        transform.rotation = Quaternion.Euler(140, 0, -180); // Mantener una orientaci�n fija
    }
}