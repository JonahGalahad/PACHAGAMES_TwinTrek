using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador_accLiberar : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;

    public GameObject imagenLiberar1;
    public GameObject imagenLiberar2;

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
                    imagenLiberar2.SetActive(false);
                }
                else
                {
                    player1.GetComponent<Jugador_accCapturado>().Liberar();
                    zonaLiberar = false;
                    paraLiberar = true;
                    imagenLiberar1.SetActive(false);
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
                Debug.Log("Apreta 'E' para liberar");
                zonaLiberar = true;
                imagenLiberar1.SetActive(true);
            }

            if (compaAtrapado2)
            {
                Debug.Log("Apreta 'E' para liberar");
                zonaLiberar = true;
                imagenLiberar2.SetActive(true);
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
                Debug.Log("Fuera de rango para liberar");
                zonaLiberar = false;
                imagenLiberar1.SetActive(false);
            }

            if (compaAtrapado2)
            {
                Debug.Log("Fuera de rango para liberar");
                zonaLiberar = false;
                imagenLiberar2.SetActive(false);
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
