using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    [Header("Variables de Vida del Jugador")]
    [SerializeField] private GameObject[] player;
    [SerializeField] private Slider barraVida;
    [SerializeField] private bool juntos = false;
    [SerializeField] private int maxVida = 100;
    [SerializeField] private float vida = 100;
    [SerializeField] private float distanceMax;
    [SerializeField] private Lazo_UnirJugadoresScript unirJugadores; //AGREGADO
    [SerializeField] private float tiempoUltimaRestaDeVida = 0f;
    [SerializeField] private float tiempoEntreRestas = 2f;

    private bool estanjugadores = false;

    private void Start()
    {
        //player = GameObject.FindGameObjectsWithTag("Player");
        unirJugadores = GameObject.FindObjectOfType<Lazo_UnirJugadoresScript>(); //AGREGADO
        // Inicia la corutina para buscar a los jugadores.
        StartCoroutine(EsperarJugadores());
    }

    private void Update()
    {
        //player = GameObject.FindGameObjectsWithTag("Player");
        if(estanjugadores)
        {
            CalcularVida();
            if (player[0].GetComponent<Player>().atrapado == true || player[1].GetComponent<Player>().atrapado == true)
            {
                juntos = false;
                return;
            }
            CalcularDistancia();
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
    }

    public void CalcularDistancia()
    {
        float distance = Mathf.Sqrt(Mathf.Pow(player[0].transform.position.x - player[1].transform.position.x, 2) + Mathf.Pow(player[0].transform.position.y - player[1].transform.position.y, 2));
        if (distance > distanceMax)
        {
            //Debug.Log("Pierde vida");
            juntos = false;
        }
        else
        {
            //Debug.Log("Gana vida");
            juntos = true;
        }
    }

    public void CalcularVida()
    {
        if (juntos == true && (Time.time - tiempoUltimaRestaDeVida) >= tiempoEntreRestas)
        {
            // Realiza la suma de vida
            RecuperarVida();

            // Actualiza el tiempo de la ultima suma
            tiempoUltimaRestaDeVida = Time.time;

        }
        else if (juntos == false && (Time.time - tiempoUltimaRestaDeVida) >= tiempoEntreRestas)
        {
            // Realiza la resta de vida
            TomarDanio();

            // Actualiza el tiempo de la ultima resta
            tiempoUltimaRestaDeVida = Time.time;
        }

        if (vida > maxVida)
        {
            vida = maxVida;
            unirJugadores.CambiarAColorBlanco(); // //AGREGADO Cambiar color a blanco cuando no se está tomando daño ni recuperando vida.
        }
        else if (vida <= 0)
        {
            //Debug.Log("MUERTOOOO");
        }
        barraVida.value = vida;
    }

    private void TomarDanio()
    {
        vida -= 2;
        unirJugadores.CambiarAColorRojo(); // //AGREGADO Cambiar color a rojo cuando se toma daño.
    }

    private void RecuperarVida()
    {
        vida += 2;
        unirJugadores.CambiarAColorVerde(); // //AGREGADO Cambiar color a verde cuando se recupera vida.
    }

    public void QuitarVidaXEnemigo(float danio)
    {
        vida -= danio;
    }
}
