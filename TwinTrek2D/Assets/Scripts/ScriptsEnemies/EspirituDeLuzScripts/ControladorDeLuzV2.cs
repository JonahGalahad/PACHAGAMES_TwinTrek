using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ControladorDeLuzV2 : MonoBehaviour
{
    [SerializeField] private UnityEngine.Rendering.Universal.Light2D luzPadre; // Luz spot del padre
    [SerializeField] private UnityEngine.Rendering.Universal.Light2D luzHijo; // Luz global del hijo
    [SerializeField] private float duracionLuzEncendida = 4f;
    [SerializeField] private float duracionTransicionLuz = 2f;
    [SerializeField] private float duracionOscuridad = 6f;
    [SerializeField] private ParticleSystem particleSystem; // Sistema de partículas

    private Color colorInicialHijo;
    private float radioInternoInicialPadre;
    private float radioExternoInicialPadre;
    private Coroutine controlLuzCoroutine; // Variable para controlar la corutina
    private ParticleSystemRenderer particleSystemRenderer;

    void Start()
    {
        // Guarda los valores iniciales de las luces al inicio
        colorInicialHijo = luzHijo.color;
        radioInternoInicialPadre = luzPadre.pointLightInnerRadius;
        radioExternoInicialPadre = luzPadre.pointLightOuterRadius;
        particleSystemRenderer = particleSystem.GetComponent<ParticleSystemRenderer>();
   }

    public void ActivarLuces()
    {
        if (controlLuzCoroutine == null)
        {
            controlLuzCoroutine = StartCoroutine(ControlarLuces());
        }
    }

    public void DesactivarLuces()
    {
        if (controlLuzCoroutine != null)
        {
            StopCoroutine(controlLuzCoroutine);
            controlLuzCoroutine = null;
            ResetearLuces();
        }
    }

    private IEnumerator ControlarLuces()
    {
        while (true)
        {
            // Cambiar color de la luz del hijo a negro
            luzHijo.color = Color.black;

            // Incrementar radio de la luz del padre
            luzPadre.pointLightInnerRadius = 25;
            luzPadre.pointLightOuterRadius = 50;
            
            // Configurar tamaño máximo de partículas
            float maxParticleSize = 0.05f;
            particleSystemRenderer.maxParticleSize = maxParticleSize;

            yield return new WaitForSeconds(duracionLuzEncendida);

            // Reducir gradualmente el radio de la luz del padre
            float tiempoTranscurrido = 0;
            while (tiempoTranscurrido < duracionTransicionLuz)
            {
                luzPadre.pointLightInnerRadius = Mathf.Lerp(25, radioInternoInicialPadre, tiempoTranscurrido / duracionTransicionLuz);
                luzPadre.pointLightOuterRadius = Mathf.Lerp(50, radioExternoInicialPadre, tiempoTranscurrido / duracionTransicionLuz);
                
                // Aumentar gradualmente el tamaño máximo de partículas
                particleSystemRenderer.maxParticleSize = Mathf.Lerp(0.05f, 0.25f, tiempoTranscurrido / duracionTransicionLuz);
                
                tiempoTranscurrido += Time.deltaTime;
                yield return null;
            }

            yield return new WaitForSeconds(duracionOscuridad);

            // Aumentar gradualmente el radio de la luz del padre de nuevo
            tiempoTranscurrido = 0;
            while (tiempoTranscurrido < duracionTransicionLuz)
            {
                luzPadre.pointLightInnerRadius = Mathf.Lerp(radioInternoInicialPadre, 25, tiempoTranscurrido / duracionTransicionLuz);
                luzPadre.pointLightOuterRadius = Mathf.Lerp(radioExternoInicialPadre, 50, tiempoTranscurrido / duracionTransicionLuz);
                
                // Disminuir gradualmente el tamaño máximo de partículas
                particleSystemRenderer.maxParticleSize = Mathf.Lerp(0.25f, 0.05f, tiempoTranscurrido / duracionTransicionLuz);
                
                tiempoTranscurrido += Time.deltaTime;
                yield return null;
            }
        }
    }

    private void ResetearLuces()
    {
        // Resetear valores iniciales de las luces
        luzHijo.color = colorInicialHijo;
        luzPadre.pointLightInnerRadius = radioInternoInicialPadre;
        luzPadre.pointLightOuterRadius = radioExternoInicialPadre;
        
        // Resetear tamaño de partícula
        particleSystemRenderer.maxParticleSize = 0.05f; 
    }
}