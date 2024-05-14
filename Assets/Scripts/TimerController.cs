using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerController : MonoBehaviour
{
    public TextMeshProUGUI timerText; // Para mostrar el tiempo en un componente UI
    private float elapsedTime = 0f; // Tiempo transcurrido
    public bool isRunning; // Bandera para controlar si el cron�metro est� en marcha
    private void Start()
    {
        isRunning = false;
    }
    void Update()
    {
        if (isRunning)
        {
            elapsedTime += Time.deltaTime; // Incrementar el tiempo transcurrido
            UpdateTimerDisplay(); // Actualizar el texto del cron�metro
        }
    }

    public void StartStopwatch()
    {
        isRunning = true; // Iniciar el cron�metro
    }

    public void StopStopwatch()
    {
        isRunning = false; // Detener el cron�metro
    }

    public void ResetStopwatch()
    {
        isRunning = false; // Detener el cron�metro
        elapsedTime = 0f; // Restablecer el tiempo transcurrido
        UpdateTimerDisplay(); // Actualizar el texto del cron�metro
    }

    private void UpdateTimerDisplay()
    {
        // Convertir el tiempo transcurrido a formato de minutos y segundos
        int minutes = (int)(elapsedTime / 60);
        int seconds = (int)(elapsedTime % 60);
        float milliseconds = (elapsedTime % 1) * 1000;

        // Formatear el texto para mostrar el tiempo
        timerText.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }
}
