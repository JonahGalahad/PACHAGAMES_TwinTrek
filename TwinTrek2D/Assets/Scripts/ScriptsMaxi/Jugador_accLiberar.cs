using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador_accLiberar : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;

    public Material colorNeutral;
    public Material colorInicial;
    public Material colorFinal;

    private bool compaAtrapado = false;
    private bool compaAtrapado2 = false;

    private bool zonaLiberar = false;
    private bool paraLiberar = false;

    private void Update()
    {
        if(zonaLiberar)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if(compaAtrapado2)
                {
                    player2.GetComponent<Jugador_accCapturado>().Liberar();
                    zonaLiberar = false;
                    paraLiberar = true;
                }
                else
                {
                    player1.GetComponent<Jugador_accCapturado>().Liberar();
                    zonaLiberar = false;
                    paraLiberar = true;
                }
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy1"))
        {
            if (compaAtrapado)
            {
                player1.GetComponent<Renderer>().material = colorNeutral;
                Debug.Log("Apreta 'E' para liberar");
                zonaLiberar = true;
            }

            if (compaAtrapado2)
            {
                player2.GetComponent<Renderer>().material = colorNeutral;
                Debug.Log("Apreta 'E' para liberar");
                zonaLiberar = true;
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy1"))
        {
            if(paraLiberar)
            {
                collision.gameObject.GetComponent<Enemy_accAtrapar>().Liberar();
                paraLiberar = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy1"))
        {
            if (compaAtrapado)
            {
                player1.GetComponent<Renderer>().material = colorFinal;
                Debug.Log("Fuera de rango para liberar");
                zonaLiberar = false;
            }

            if (compaAtrapado2)
            {
                player2.GetComponent<Renderer>().material = colorFinal;
                Debug.Log("Fuera de rango para liberar");
                zonaLiberar = false;
            }
        }
    }

    public void Jugador1Atrapado()
    {
        compaAtrapado = true;
    }
    public void Jugador1Liberado()
    {
        compaAtrapado = false;
    }

    public void Jugador2Atrapado()
    {
        compaAtrapado2 = true;
    }
    public void Jugador2Liberado()
    {
        compaAtrapado2 = false;
    }
}
