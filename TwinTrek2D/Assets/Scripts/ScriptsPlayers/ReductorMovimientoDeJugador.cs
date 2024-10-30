using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReductorMovimientoDeJugador : MonoBehaviour
{
    private Player1 player1Movement;
    private Player2 player2Movement;

    private float velocidadOriginal;
    private float fuerzaSaltoOriginal;

    void Start()
    {
        player1Movement = GetComponent<Player1>();
        player2Movement = GetComponent<Player2>();

        if (player1Movement != null)
        {
            velocidadOriginal = player1Movement.moveSpeed;
            fuerzaSaltoOriginal = player1Movement.jumpVelocity;
        }
        else if (player2Movement != null)
        {
            velocidadOriginal = player2Movement.moveSpeed;
            fuerzaSaltoOriginal = player2Movement.jumpVelocity;
        }
    }

    public void ReducirVelocidad(float factorReduccion)
    {
        if (player1Movement != null)
        {
            player1Movement.moveSpeed *= factorReduccion;
            player1Movement.jumpVelocity *= factorReduccion;
        }
        else if (player2Movement != null)
        {
            player2Movement.moveSpeed *= factorReduccion;
            player2Movement.jumpVelocity *= factorReduccion;
        }
    }

    public void RestaurarVelocidad()
    {
        if (player1Movement != null)
        {
            player1Movement.moveSpeed = velocidadOriginal;
            player1Movement.jumpVelocity = fuerzaSaltoOriginal;
        }
        else if (player2Movement != null)
        {
            player2Movement.moveSpeed = velocidadOriginal;
            player2Movement.jumpVelocity = fuerzaSaltoOriginal;
        }
    }
}
