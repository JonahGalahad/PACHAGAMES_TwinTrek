using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador_accCapturado : MonoBehaviour
{
    public Player1_mov player1_Mov;
    public Player2_mov player2_Mov;

    public GameObject player1;
    public GameObject player2;

    private Rigidbody2D rib;
    private Collider2D player1Collider;

    public int jugadorAtrapado = 0;
    void Start()
    {
        player1_Mov = GetComponent<Player1_mov>();
        player2_Mov = GetComponent<Player2_mov>();
        rib = GetComponent<Rigidbody2D>();
        player1Collider = GetComponent<Collider2D>();
    }

    public void Atrapado() //metodo que activa para que el jugador quede atrapado
    {
        if(jugadorAtrapado == 1)
        {
            player1_Mov.atrapado = true;
            player2.GetComponent<Jugador_accLiberar>().Jugador1Atrapado();
            rib.constraints = RigidbodyConstraints2D.FreezePositionY;
            player1Collider.isTrigger = true;
            this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
        if (jugadorAtrapado == 2)
        {
            player2_Mov.atrapado = true;
            player1.GetComponent<Jugador_accLiberar>().Jugador2Atrapado();
            rib.constraints = RigidbodyConstraints2D.FreezePositionY;
            player1Collider.isTrigger = true;
            this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    public void Liberar()
    {
        if(jugadorAtrapado == 1)
        {
            player1_Mov.atrapado = false;
            player2.GetComponent<Jugador_accLiberar>().Jugador1Liberado();
            rib.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
            rib.constraints = RigidbodyConstraints2D.FreezeRotation;
            player1Collider.isTrigger = false;
            jugadorAtrapado = 0;
        }

        if (jugadorAtrapado == 2)
        {
            player2_Mov.atrapado = false;
            player1.GetComponent<Jugador_accLiberar>().Jugador2Liberado();
            rib.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
            rib.constraints = RigidbodyConstraints2D.FreezeRotation;
            player1Collider.isTrigger = false;
            jugadorAtrapado = 0;
        }
    }
}
