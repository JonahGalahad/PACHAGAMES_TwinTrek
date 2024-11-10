using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuegoApagar : MonoBehaviour
{
    [SerializeField] private float tiempoDeVida = 3f; // Tiempo de vida del fuego en segundos
    [SerializeField] private float tiempoParaApagar = 2f; // Tiempo en segundos para apagar el fuego
    private Vector3 escalaInicial;
    private Vector3 posicionInicial;

    private void Start()
    {
        escalaInicial = transform.localScale; // Guardar la escala inicial del fuego
    }

    public void ActualizarPosicionInicial(Vector3 nuevaPosicion)
    {
        posicionInicial = nuevaPosicion; // Actualizar la posición inicial
    }

    public void IniciarApagado()
    {
        Debug.Log("IniciarApagado llamado"); // Agregado
        StartCoroutine(GestionarFuego());
    }

    private IEnumerator GestionarFuego()
    {
        // Espera durante el tiempo de vida del fuego
        yield return new WaitForSeconds(tiempoDeVida);

        // Inicia la rutina de apagado con achique
        yield return StartCoroutine(ApagarFuegoConAchique());
    }

    private IEnumerator ApagarFuegoConAchique()
    {
        float tiempoTranscurrido = 0f;

        while (tiempoTranscurrido < tiempoParaApagar)
        {
            // Calcular el factor de achique basado en el tiempo transcurrido
            float factor = 1 - (tiempoTranscurrido / tiempoParaApagar);
            // Aplicar la escala descendente solo en el eje Y y ajustar la posición para mantener la base en el suelo
            transform.localScale = new Vector3(escalaInicial.x, escalaInicial.y * factor, escalaInicial.z);
            transform.position = new Vector3(posicionInicial.x, posicionInicial.y - (escalaInicial.y * (1 - factor) / 2), posicionInicial.z);

            tiempoTranscurrido += Time.deltaTime;
            yield return null; // Espera el siguiente frame
        }

        // Destruye el GameObject después de completar el achique
        Destruir();
    }

    private void Destruir()
    {
        // Destruye el GameObject
        Destroy(gameObject);
        Debug.Log("El fuego ha sido destruido.");
    }
}
