using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReductorMovimientoDeJugador : MonoBehaviour
{
    private Player playerMovement;

    [SerializeField] private float velocidadOriginal;
    [SerializeField] private float fuerzaSaltoOriginal;

    void Start()
    {
        playerMovement = GetComponent<Player>();

        if (playerMovement != null)
        {
            velocidadOriginal = playerMovement.moveSpeed;
            fuerzaSaltoOriginal = playerMovement.jumpVelocity;
        }
    }

    public void ReducirVelocidad(float factorReduccion)
    {
        if (playerMovement != null)
        {
            playerMovement.moveSpeed *= factorReduccion;
            playerMovement.jumpVelocity *= factorReduccion;
        }
    }

    public void RestaurarVelocidad()
    {
        if (playerMovement != null)
        {
            playerMovement.moveSpeed = velocidadOriginal;
            playerMovement.jumpVelocity = fuerzaSaltoOriginal;
        }
    }
}
