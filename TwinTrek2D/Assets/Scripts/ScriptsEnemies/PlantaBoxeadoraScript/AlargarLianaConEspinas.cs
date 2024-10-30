using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlargarLianaConEspinas : MonoBehaviour
{
    [SerializeField] private float tiempoEspera = 1.0f; // Tiempo de espera para que la liana se alargue
    [SerializeField] private float duracionAlargue = 0.5f; // Duración del alargue
    private Vector3 escalaOriginal;
    private Vector3 posicionOriginal;
    private Vector3 escalaObjetivo = new Vector3(1, 1, 1); // El objetivo de escala para alargar
    private Vector3 posicionObjetivo = new Vector3(2, 0, 0); // El objetivo de posición para alargar

    private ApuntarAlJugador apuntador;

    void Start()
    {
        escalaOriginal = transform.localScale;
        posicionOriginal = transform.localPosition;
        apuntador = GetComponentInParent<ApuntarAlJugador>();
        StartCoroutine(CicloAlargue());
    }

    IEnumerator CicloAlargue()
    {
        while (true)
        {
            // Desactivar el apuntado
            if (apuntador != null)
            {
                apuntador.ActivarApuntado(false);
            }

            // Alargar
            float tiempoTranscurrido = 0;
            while (tiempoTranscurrido < duracionAlargue)
            {
                tiempoTranscurrido += Time.deltaTime;
                transform.localScale = Vector3.Lerp(escalaOriginal, escalaObjetivo, tiempoTranscurrido / duracionAlargue);
                transform.localPosition = Vector3.Lerp(posicionOriginal, posicionObjetivo, tiempoTranscurrido / duracionAlargue);
                yield return null;
            }

            // Esperar. Lo saqué porque esperaba alargado y yo quiero que se alargue y al instante vuelva a achatarse
            //yield return new WaitForSeconds(tiempoEspera);

            // Volver al estado original
            tiempoTranscurrido = 0;
            while (tiempoTranscurrido < duracionAlargue)
            {
                tiempoTranscurrido += Time.deltaTime;
                transform.localScale = Vector3.Lerp(escalaObjetivo, escalaOriginal, tiempoTranscurrido / duracionAlargue);
                transform.localPosition = Vector3.Lerp(posicionObjetivo, posicionOriginal, tiempoTranscurrido / duracionAlargue);
                yield return null;
            }

            // Reactivar el apuntado
            if (apuntador != null)
            {
                apuntador.ActivarApuntado(true);
            }

            // Esperar antes de repetir el ciclo
            yield return new WaitForSeconds(tiempoEspera);
        }
    }
}
