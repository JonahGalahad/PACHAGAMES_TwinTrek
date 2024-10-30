using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApuntarAlJugador : MonoBehaviour
{
    [SerializeField] private GameObject[] player;
    private bool estanjugadores = false;
    //private GameObject jugadorMax;
    //private GameObject jugadorSam;
    private GameObject objetivoActual;
    private bool puedeApuntar = true;

    void Start()
    {
        // Encontrar a los jugadores con los tags "Max" y "Sam"
        StartCoroutine(EsperarJugadores());
        //jugadorMax = GameObject.FindGameObjectWithTag("Max");
        //jugadorSam = GameObject.FindGameObjectWithTag("Sam");
    }

    void Update()
    {
        if (!puedeApuntar) return;

        if (estanjugadores)
        {
            // Calcular las distancias a ambos jugadores
            float distancia1 = Vector3.Distance(transform.position, player[0].transform.position);
            float distancia2 = Vector3.Distance(transform.position, player[1].transform.position);

            // Determinar cuál es el jugador más cercano
            if (distancia1 <= distancia2)
            {
                objetivoActual = player[0];
            }
            else
            {
                objetivoActual = player[1];
            }

            // Apuntar hacia el jugador más cercano
            Vector3 direccionObjetivo = objetivoActual.transform.position - transform.position;
            float angulo = Mathf.Atan2(direccionObjetivo.y, direccionObjetivo.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angulo));
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
        estanjugadores = true; // Actualiza la bandera cuando ambos jugadores están presentes.
        //jugadorMax = GameObject.FindGameObjectWithTag("Max");
        //jugadorSam = GameObject.FindGameObjectWithTag("Sam");
    }

    public void ActivarApuntado(bool estado)
    {
        puedeApuntar = estado;
    }
}
