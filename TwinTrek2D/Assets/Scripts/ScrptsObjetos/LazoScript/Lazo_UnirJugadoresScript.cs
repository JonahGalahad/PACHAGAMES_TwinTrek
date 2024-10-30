using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Lazo_UnirJugadoresScript : MonoBehaviour
{
    [SerializeField] private GameObject[] player;
    [SerializeField] private Transform jugador1;
    [SerializeField] private Transform jugador2;

    private LineRenderer lineRenderer;
    private GameObject lazoBlanco;
    private GameObject lazoRojo;
    private GameObject lazoVerde;

    private bool estanjugadores = false;


    void Start()
    {
        /*player = GameObject.FindGameObjectsWithTag("Player");
        

        // Busca el GameObject de los jugadores en la escena y obtiene su Transform.
        jugador1 = player[0].transform;
        jugador2 = player[1].transform;*/

        // Busca el componente LineRenderer que esta adjunto al GameObject que tiene este script.
        lineRenderer = GetComponent<LineRenderer>();
        // Busca el objeto llamado "LazoBlanco" en la escena y lo asigna a la variable.
        lazoBlanco = GameObject.Find("LazoBlanco");

        // Busca el objeto llamado "LazoRojo" en la escena y lo asigna a la variable.
        lazoRojo = GameObject.Find("LazoRojo");

        // Busca el objeto llamado "LazoVerde" en la escena y lo asigna a la variable.
        lazoVerde = GameObject.Find("LazoVerde");
        // Inicia la corutina para buscar a los jugadores.
        StartCoroutine(EsperarJugadores());
    }

    void Update()
    {

        /*if (jugador1 != null && jugador2 != null)
        {
            lineRenderer.SetPosition(0, jugador1.position);
            lineRenderer.SetPosition(1, jugador2.position);
        }*/

        // Solo ejecuta el código si ambos jugadores existen.
        if (estanjugadores)
        {
            lineRenderer.SetPosition(0, jugador1.position);
            lineRenderer.SetPosition(1, jugador2.position);
        }
    }

    IEnumerator EsperarJugadores()
    {
        // Espera hasta que se encuentren ambos jugadores.
        while (player.Length < 2)
        {
            player = GameObject.FindGameObjectsWithTag("Player");
            yield return null;
        }

        // Asigna los Transform de los jugadores una vez que se encuentran.
        jugador1 = player[0].transform;
        jugador2 = player[1].transform;
        estanjugadores = true; // Actualiza la bandera cuando ambos jugadores están presentes.
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
