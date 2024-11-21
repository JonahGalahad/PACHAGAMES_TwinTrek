using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camara_statusSeguirPlayers : MonoBehaviour
{
    [SerializeField] private GameObject[] jugadores; // Array para almacenar los jugadores
    [SerializeField] private bool estanJugadores = false; // Indica si los jugadores están presentes
    [SerializeField] private Camera camara; // Referencia a la cámara principal
    [SerializeField] private float zoomMinimo = 10f; // Zoom mínimo de la cámara
    [SerializeField] private float zoomMaximo = 30f; // Zoom máximo de la cámara
    [SerializeField] private float desplazamientoVertical = 2f; // Desplazamiento vertical de la cámara
    [SerializeField] private GameObject limiteIzquierda; // Referencia al GameObject que marca el límite izquierdo
    [SerializeField] private GameObject limiteDerecha; // Referencia al GameObject que marca el límite derecho
    [SerializeField] private GameObject limiteSuperior; // Referencia al GameObject que marca el límite superior
    [SerializeField] private GameObject limiteInferior; // Referencia al GameObject que marca el límite inferior

    void Start()
    {
        StartCoroutine(EsperarJugadores());
        camara = Camera.main; // Asigna automáticamente la cámara principal si no se ha asignado en el Inspector.
    }

    void Update()
    {
        if (!estanJugadores || camara == null)
        {
            return;
        }

        // Obtener posiciones de los jugadores
        Vector3 posicionJugador1 = jugadores[0].transform.position;
        Vector3 posicionJugador2 = jugadores[1].transform.position;

        // Calcular la posición media
        Vector3 posicionMedia = (posicionJugador1 + posicionJugador2) / 2f;

        // Calcular la distancia entre los jugadores
        float distanciaEntreJugadores = Vector3.Distance(posicionJugador1, posicionJugador2);

        // Calcular el tamaño del zoom
        float tamanoZoom = Mathf.Clamp(distanciaEntreJugadores / 2f, zoomMinimo, zoomMaximo);

        // Ajustar el zoom de la cámara ortográfica
        camara.orthographicSize = tamanoZoom;

        // Calcular los límites de la cámara teniendo en cuenta el zoom
        float limiteIzquierdaX = limiteIzquierda.transform.position.x + camara.orthographicSize * camara.aspect;
        float limiteDerechaX = limiteDerecha.transform.position.x - camara.orthographicSize * camara.aspect;
        float limiteSuperiorY = limiteSuperior.transform.position.y - camara.orthographicSize;
        float limiteInferiorY = limiteInferior.transform.position.y + camara.orthographicSize;

        // Calcular la nueva posición de la cámara
        float nuevaPosicionX = Mathf.Clamp(posicionMedia.x, limiteIzquierdaX, limiteDerechaX);
        float nuevaPosicionY = Mathf.Clamp(posicionMedia.y + desplazamientoVertical, limiteInferiorY, limiteSuperiorY);

        // Ajustar la posición de la cámara
        camara.transform.position = new Vector3(nuevaPosicionX, nuevaPosicionY, camara.transform.position.z);
    }

    IEnumerator EsperarJugadores()
    {
        // Espera hasta que se encuentren ambos jugadores.
        while (jugadores.Length < 2)
        {
            jugadores = GameObject.FindGameObjectsWithTag("Player");
            yield return null;
        }
        estanJugadores = true; // Actualiza la bandera cuando ambos jugadores están presentes.
    }

    void OnDisable()
    {
        // Detener todas las corutinas cuando el script se deshabilite
        StopAllCoroutines();
    }
}
