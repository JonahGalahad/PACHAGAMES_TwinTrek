using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeReducirVelocidad : MonoBehaviour
{
    [SerializeField] private float factorReduccionVelocidad = 0.5f; // Factor de reducción para la velocidad. Mientras más bajo mayor reducción, pero no poner en negativo.

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ReductorMovimientoDeJugador reductorMovimientoDeJugador = collision.GetComponent<ReductorMovimientoDeJugador>();
            if (reductorMovimientoDeJugador != null)
            {
                reductorMovimientoDeJugador.ReducirVelocidad(factorReduccionVelocidad);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ReductorMovimientoDeJugador reductorMovimientoDeJugador = collision.GetComponent<ReductorMovimientoDeJugador>();
            if (reductorMovimientoDeJugador != null)
            {
                reductorMovimientoDeJugador.RestaurarVelocidad();
            }
        }
    }
}
