using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlorScript : MonoBehaviour
{
    public bool jugadorYaAtrapado = false;
    public float TiempoEsperaParaAtrapar = 3f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) //collision con cualquier jugador
        {
            if (jugadorYaAtrapado == false) //significa que puede atrapar
            {
                collision.gameObject.GetComponent<Player>().EstarAtrapado(); //Le dice al jugador 1 (Sam) que esta atrapado
                collision.gameObject.GetComponent<Transform>().position = this.gameObject.transform.position; //le dice al jugador que tome su posicion.
                if(collision.gameObject.GetComponent <Player>().asignarJugador == 1)
                {
                    Debug.Log("¡El enemigo atrapó al jugador 1!");
                }
                else if(collision.gameObject.GetComponent<Player>().asignarJugador == 2)
                {
                    Debug.Log("¡El enemigo atrapó al jugador 2!");
                }
                jugadorYaAtrapado = true;
            }

        }
    }

    public void Liberar()
    {
        StartCoroutine(DejarDeAtrapar());
    }

    IEnumerator DejarDeAtrapar()
    {
        yield return new WaitForSeconds(TiempoEsperaParaAtrapar);
        jugadorYaAtrapado = false;
        Debug.Log("¡El enemigo vuelve a moverse!");
    }
}
