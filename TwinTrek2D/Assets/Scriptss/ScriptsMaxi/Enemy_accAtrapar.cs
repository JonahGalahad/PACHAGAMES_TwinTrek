using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_accAtrapar : MonoBehaviour
{
    public Enemy enemy;
    private bool jugadorAtrapado2 = false;
    public float TiempoEsperaParaAtrapar = 3f;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) //collision con jugador 1
        {
            if (jugadorAtrapado2 == false) //significa que puede atrapar
            {
                // Si el enemigo detecta al jugador, detendra su movimiento.
                enemy.DejarMoverse();
                collision.gameObject.GetComponent<Jugador_accCapturado>().jugadorAtrapado = 1;
                collision.gameObject.GetComponent<Jugador_accCapturado>().Atrapado(); //le dice al jugador que esta atrapado.
                collision.gameObject.GetComponent<Transform>().position = this.gameObject.transform.position; //le dice al jugador que tome su posicion.
                Debug.Log("¡El enemigo atrapó al jugador 1!");
                jugadorAtrapado2 = true;
            }
        }
        if (collision.CompareTag("Max")) //collision con jugador 1
        {
            if (jugadorAtrapado2 == false) //significa que puede atrapar
            {
                // Si el enemigo detecta al jugador 2, detendra su movimiento.
                enemy.DejarMoverse();
                collision.gameObject.GetComponent<Jugador_accCapturado>().jugadorAtrapado = 2;
                collision.gameObject.GetComponent<Jugador_accCapturado>().Atrapado(); //le dice al jugador que esta atrapado.
                collision.gameObject.GetComponent<Transform>().position = this.gameObject.transform.position; //le dice al jugador que tome su posicion.
                Debug.Log("¡El enemigo atrapó al jugador 2!");
                jugadorAtrapado2 = true;
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
        enemy.jugadorAtrapado = false;
        jugadorAtrapado2 = false;
        enemy.PermitirMoverse();
        Debug.Log("¡El enemigo vuelve a moverse!");
    }
}
