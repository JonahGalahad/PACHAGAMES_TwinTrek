using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlorLocalScript : MonoBehaviour
{
    public bool jugadorYaAtrapado = false;
    public GameObject playerAtrapado;
    [SerializeField] private bool paraAtrapar = true;
    private int jugadoresEnFlor = 0;

    private void Start()
    {
        playerAtrapado = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) //collision con cualquier jugador
        {
            jugadoresEnFlor++;
            if (paraAtrapar) //significa que puede atrapar
            {
                playerAtrapado = collision.gameObject;
                collision.gameObject.GetComponent<PlayerLocal>().EstarAtrapado(); //Le dice al jugador 1 (Sam) que esta atrapado
                collision.gameObject.GetComponent<Transform>().position = this.gameObject.transform.position; //le dice al jugador que tome su posicion.
                paraAtrapar = false;
                jugadorYaAtrapado = true;
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            jugadoresEnFlor--;
            if (jugadoresEnFlor <= 0)
            {
                paraAtrapar = true;
                Debug.Log("La Flor puede volver a atrapar!");
            }
        }
    }

    public void Liberar()
    {
        StartCoroutine(DejarDeAtrapar());
    }

    IEnumerator DejarDeAtrapar()
    {
        Debug.Log("Libera a mi compa!");
        playerAtrapado.GetComponent<PlayerLocal>().DejarEstarAtrapado();
        playerAtrapado = null;
        jugadorYaAtrapado = false;
        yield return null;
    }
}
