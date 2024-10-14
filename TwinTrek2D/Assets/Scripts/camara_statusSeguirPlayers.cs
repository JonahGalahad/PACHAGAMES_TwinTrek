using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camara_statusSeguirPlayers : MonoBehaviour
{
    public Transform jugador1;
    public Transform jugador2;
    public Camera camara;
    public float zoomMinimo = 5f; // Ajustar el valor según lo que se necesite
    public float zoomMaximo = 10f; // Ajustar el valor según lo que se necesite

    void Start()
    {
        // Busca el GameObject llamado "player1" en la escena y obtiene su Transform.
        jugador1 = GameObject.Find("Player1").transform; //Cambiar el nombre del GameObject al nombre que corresponda

        // Busca el GameObject llamado "Capsule" en la escena y obtiene su Transform.
        jugador2 = GameObject.Find("Player2").transform; //Cambiar el nombre del GameObject al nombre que corresponda

        camara = Camera.main; // Asigna automáticamente la cámara principal si no se ha asignado en el Inspector.
    }

    void Update()
    {
        // Obtener posiciones de los jugadores
        Vector3 posicionJugador1 = jugador1.position;
        Vector3 posicionJugador2 = jugador2.position;

        // Calcular la posición media
        Vector3 posicionMedia = (posicionJugador1 + posicionJugador2) / 2f;

        // Ajustar la posición de la cámara
        camara.transform.position = new Vector3(posicionMedia.x, posicionMedia.y, camara.transform.position.z);

        // Calcular la distancia entre los jugadores
        float distanciaEntreJugadores = Vector3.Distance(posicionJugador1, posicionJugador2);

        // Calcular el tamaño del zoom
        float tamañoZoom = Mathf.Clamp(distanciaEntreJugadores / 2f, zoomMinimo, zoomMaximo);

        // Ajustar el zoom de la cámara ortográfica
        camara.orthographicSize = tamañoZoom;
    }
}
