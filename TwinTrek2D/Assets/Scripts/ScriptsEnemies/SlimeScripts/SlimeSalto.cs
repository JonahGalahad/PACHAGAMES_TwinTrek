using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSalto : MonoBehaviour
{
    [SerializeField] private float fuerzaSalto = 4.5f;
    [SerializeField] private float distanciaSalto = 2.5f;
    [SerializeField] private float duracionSalto = 0.5f;

    public IEnumerator Saltar(Transform objetivoActual, Transform puntoA, Transform puntoB, float tiempoInicioMovimiento, SpriteRenderer spriteRenderer, float umbralDistancia)
    {
        float tiempoTranscurrido = 0;
        Vector3 posicionInicial = transform.position;
        Vector3 direccionSalto = (objetivoActual.position - transform.position).normalized;
        Vector3 posicionObjetivo = new Vector3(transform.position.x + direccionSalto.x * distanciaSalto, transform.position.y, transform.position.z);
        Vector3 apexSalto = new Vector3((posicionInicial.x + posicionObjetivo.x) / 2, transform.position.y + fuerzaSalto, transform.position.z);

        while (tiempoTranscurrido < duracionSalto)
        {
            tiempoTranscurrido += Time.deltaTime;
            float t = tiempoTranscurrido / duracionSalto;

            // Movimiento en parábola
            transform.position = Vector3.Lerp(Vector3.Lerp(posicionInicial, apexSalto, t), Vector3.Lerp(apexSalto, posicionObjetivo, t), t);
            yield return null;
        }

        // Asegurarse de que el Slime no supere el objetivo, es decir los puntos A o B
        if (Vector3.Distance(transform.position, objetivoActual.position) <= umbralDistancia)
        {
            objetivoActual = (objetivoActual == puntoA) ? puntoB : puntoA;
            tiempoInicioMovimiento = Time.time;

            // Cambiar la dirección del sprite volteando la escala en el eje X
            Vector3 nuevaEscala = spriteRenderer.transform.localScale;
            nuevaEscala.x *= -1;
            spriteRenderer.transform.localScale = nuevaEscala;
        }

        yield return new WaitForSeconds(0.5f); // Pausa después del salto
    }
}