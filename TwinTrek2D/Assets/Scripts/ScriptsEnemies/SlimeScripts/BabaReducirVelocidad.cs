using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabaReducirVelocidad : MonoBehaviour
{
    [SerializeField] private float factorReduccionVelocidad = 0.5f; // Factor de reducción para la velocidad. Mientras más bajo mayor reducción, pero no poner en negativo.
    private Rigidbody2D babaRB;

    private void Start()
    {
        babaRB = GetComponent<Rigidbody2D>();
        if (babaRB != null)
        {
            // Inicialmente, configuramos el Rigidbody2D como Dynamic para que responda a la gravedad
            babaRB.bodyType = RigidbodyType2D.Dynamic;
        }
    }

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

        // Detectar colisión con el suelo o un bloque
        if (collision.gameObject.layer == LayerMask.NameToLayer("Piso") || collision.CompareTag("Bloque"))
        {
            if (babaRB != null)
            {
                // Cambiar el Rigidbody2D a Kinematic para que la baba se quede en el lugar
                babaRB.bodyType = RigidbodyType2D.Kinematic;
                // Congelar la posición para que la baba no se mueva más
                babaRB.constraints = RigidbodyConstraints2D.FreezeAll;
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
