using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutodestruirEnemigo : MonoBehaviour
{
    [SerializeField] private float tiempoAntesDeDestruir = 6f; // Tiempo antes de destruir el objeto
    [SerializeField] private float intervaloDeParpadeo = 0.2f; // Intervalo entre parpadeos
    [SerializeField] private float transparenciaMinima = 0.2f; // Transparencia mínima durante el parpadeo
    [SerializeField] private float tiempoNormal = 5f; // Tiempo que el objeto permanece normal antes de comenzar a parpadear
    private SpriteRenderer spriteRenderer;
    private Color colorOriginal;

    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        colorOriginal = spriteRenderer.color;
        StartCoroutine(DestruirConParpadeo());
    }

    private IEnumerator DestruirConParpadeo()
    {
        // Permanecer normal durante tiempoNormal
        yield return new WaitForSeconds(tiempoNormal);

        float tiempoParpadeo = tiempoAntesDeDestruir - tiempoNormal;
        
        while (tiempoParpadeo > 0)
        {
            // Alternar la transparencia
            Color nuevoColor = colorOriginal;
            nuevoColor.a = (spriteRenderer.color.a == transparenciaMinima) ? 1.0f : transparenciaMinima;
            spriteRenderer.color = nuevoColor;

            yield return new WaitForSeconds(intervaloDeParpadeo);
            tiempoParpadeo -= intervaloDeParpadeo;
        }

        Destroy(gameObject);
    }
}
