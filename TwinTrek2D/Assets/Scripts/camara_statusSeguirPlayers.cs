using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camara_statusSeguirPlayers : MonoBehaviour
{
    [SerializeField] private GameObject[] player;
    [SerializeField] private bool estanjugadores = false;
    [SerializeField] private Camera camara;
    [SerializeField] private float zoomMinimo = 5f; // Ajustar el valor según lo que se necesite
    [SerializeField] private float zoomMaximo = 10f; // Ajustar el valor según lo que se necesite
    [SerializeField] private float algo;

    void Start()
    {
        StartCoroutine(EsperarJugadores());
        // Busca el GameObject llamado "player1" en la escena y obtiene su Transform.
        //jugador1 = GameObject.Find("Player").transform; //Cambiar el nombre del GameObject al nombre que corresponda

        // Busca el GameObject llamado "Capsule" en la escena y obtiene su Transform.
        //jugador2 = GameObject.Find("Player").transform; //Cambiar el nombre del GameObject al nombre que corresponda

        camara = Camera.main; // Asigna automáticamente la cámara principal si no se ha asignado en el Inspector.
    }

    void Update()
    {
        if(!estanjugadores)
        {
            return;
        }
        // Obtener posiciones de los jugadores
        Vector3 posicionJugador1 = player[0].transform.position;
        Vector3 posicionJugador2 = player[1].transform.position;

        // Calcular la posición media
        Vector3 posicionMedia = (posicionJugador1 + posicionJugador2) / 2f;

        // Ajustar la posición de la cámara
        camara.transform.position = new Vector3(posicionMedia.x, posicionMedia.y+algo, camara.transform.position.z);

        // Calcular la distancia entre los jugadores
        float distanciaEntreJugadores = Vector3.Distance(posicionJugador1, posicionJugador2);

        // Calcular el tamaño del zoom
        float tamañoZoom = Mathf.Clamp(distanciaEntreJugadores / 2f, zoomMinimo, zoomMaximo);

        // Ajustar el zoom de la cámara ortográfica
        camara.orthographicSize = tamañoZoom;
    }

    IEnumerator EsperarJugadores()
    {
        // Espera hasta que se encuentren ambos jugadores.
        while (player.Length < 2)
        {
            player = GameObject.FindGameObjectsWithTag("Player");
            yield return null;
        }
        estanjugadores = true; // Actualiza la bandera cuando ambos jugadores están presentes.
    }
}
