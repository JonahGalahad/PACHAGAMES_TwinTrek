using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrilloAlternante : MonoBehaviour
{
    private ParticleSystem particleSystem;
    private float maxAlpha = 1f; // Opacidad máxima
    private float minAlpha = 0.2f; // Opacidad mínima
    private float switchInterval = 1f; // Intervalo de cambio (1 segundo)

    private float timer = 0f;
    private bool isHighAlpha = true;

    private void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        // Actualiza el temporizador
        timer += Time.deltaTime;

        // Alterna entre alta y baja opacidad
        if (timer >= switchInterval)
        {
            isHighAlpha = !isHighAlpha;
            timer = 0f;
        }

        // Asigna la opacidad adecuada a las partículas
        float targetAlpha = isHighAlpha ? maxAlpha : minAlpha;
        var mainModule = particleSystem.main;
        mainModule.startColor = new Color(mainModule.startColor.color.r, mainModule.startColor.color.g, mainModule.startColor.color.b, targetAlpha);
    }
}