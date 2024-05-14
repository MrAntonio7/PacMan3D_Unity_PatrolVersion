using System.Collections.Generic;
using UnityEngine;

public class OverlappingAudio : MonoBehaviour
{
    public AudioClip soundClip; // El clip de audio que se va a reproducir
    private List<AudioSource> audioSources = new List<AudioSource>(); // Lista de AudioSources

    void Start()
    {
        // Crear algunos AudioSource inicialmente
        for (int i = 0; i < 3; i++) // Cambia 3 por el número de fuentes necesarias
        {
            var newSource = gameObject.AddComponent<AudioSource>();
            newSource.clip = soundClip;
            audioSources.Add(newSource);
        }
    }

    public void PlaySound()
    {
        // Buscar un AudioSource que no esté reproduciendo
        AudioSource availableSource = audioSources.Find(src => !src.isPlaying);

        if (availableSource != null)
        {
            // Reproducir el sonido si hay un AudioSource disponible
            availableSource.Play();
        }
        else
        {
            // Si no hay ninguno disponible, crear uno nuevo
            var newSource = gameObject.AddComponent<AudioSource>();
            newSource.clip = soundClip;
            newSource.Play();
            audioSources.Add(newSource); // Añadirlo a la lista para usarlo luego
        }
    }
}