using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlorScript : MonoBehaviour
{
    public bool jugadorYaAtrapado = false;
    public float TiempoEsperaParaAtrapar = 3f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Sam")) //collision con jugador 1 SAM
        {
            if (jugadorYaAtrapado == false) //significa que puede atrapar
            {
                collision.gameObject.GetComponent<Player1>().EstarAtrapado(); //Le dice al jugador 1 (Sam) que esta atrapado
                collision.gameObject.GetComponent<Transform>().position = this.gameObject.transform.position; //le dice al jugador que tome su posicion.
                Debug.Log("¡El enemigo atrapó al jugador 1!");
                jugadorYaAtrapado = true;
            }

        }
        if (collision.CompareTag("Max")) //collision con jugador 1
        {
            if (jugadorYaAtrapado == false) //significa que puede atrapar
            {
                collision.gameObject.GetComponent<Player2>().EstarAtrapado(); //Le dice al jugador 1 (Sam) que esta atrapado
                collision.gameObject.GetComponent<Transform>().position = this.gameObject.transform.position; //le dice al jugador que tome su posicion.
                Debug.Log("¡El enemigo atrapó al jugador 2!");
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
