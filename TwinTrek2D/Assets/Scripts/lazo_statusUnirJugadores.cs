using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lazo_statusUnirJugadores : MonoBehaviour
{
    public Transform jugador1; // Transform del jugador 1
    public Transform jugador2; // Transform del jugador 2

    private LineRenderer lineRenderer;
    public GameObject lazoBlanco; 
    public GameObject lazoRojo;
    public GameObject lazoVerde;


    void Start()
    {
        // Busca el componente LineRenderer que esta adjunto al GameObject que tiene este script.
        lineRenderer = GetComponent<LineRenderer>();

        // Busca el GameObject llamado "player1" en la escena y obtiene su Transform.
        jugador1 = GameObject.Find("Player1").transform; //Cambiar el nombre del GameObject al nombre que corresponda

        // Busca el GameObject llamado "Capsule" en la escena y obtiene su Transform.
        jugador2 = GameObject.Find("Capsule").transform; //Cambiar el nombre del GameObject al nombre que corresponda
        
        // Busca el objeto llamado "LazoBlanco" en la escena y lo asigna a la variable.
        lazoBlanco = GameObject.Find("LazoBlanco");

        // Busca el objeto llamado "LazoRojo" en la escena y lo asigna a la variable.
        lazoRojo = GameObject.Find("LazoRojo");

        // Busca el objeto llamado "LazoVerde" en la escena y lo asigna a la variable.
        lazoVerde = GameObject.Find("LazoVerde");
    }

    void Update()
    {
        if (jugador1 != null && jugador2 != null)
        {
            lineRenderer.SetPosition(0, jugador1.position);
            lineRenderer.SetPosition(1, jugador2.position);
        }
    }

    public void CambiarAColorBlanco()
    {
        lazoBlanco.SetActive(true);
        lazoRojo.SetActive(false);
        lazoVerde.SetActive(false);
    }
    public void CambiarAColorRojo()
    {
        lazoBlanco.SetActive(false);
        lazoRojo.SetActive(true);
        lazoVerde.SetActive(false);
    }

    public void CambiarAColorVerde()
    {
        lazoBlanco.SetActive(false);
        lazoRojo.SetActive(false);
        lazoVerde.SetActive(true);
    }
}
