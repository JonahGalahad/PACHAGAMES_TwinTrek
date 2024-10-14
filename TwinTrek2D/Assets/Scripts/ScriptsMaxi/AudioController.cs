using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private AudioSource audioSource;
    private Camera mainCamera;

    // Define el rango máximo en el que se escuchará el audio y el volumen máximo.
    public float maxDistance = 10.0f;
    public float maxVolume = 1.0f; // Volumen máximo, ajusta según tus necesidades.

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (mainCamera != null)
        {
            float distanceToCamera = Vector3.Distance(transform.position, mainCamera.transform.position);

            // Calcula el volumen basado en la distancia.
            float volume = Mathf.Clamp01(1.0f - (distanceToCamera / maxDistance)) * maxVolume;

            // Aplica el volumen al AudioSource.
            audioSource.volume = volume;

            if (distanceToCamera <= maxDistance)
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                }
            }
            else
            {
                if (audioSource.isPlaying)
                {
                    audioSource.Stop();
                }
            }
        }
    }

}
