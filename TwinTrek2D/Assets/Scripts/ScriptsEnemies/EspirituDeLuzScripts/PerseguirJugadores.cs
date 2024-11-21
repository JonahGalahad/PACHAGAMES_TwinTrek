using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerseguirJugadores : MonoBehaviour
{
    [SerializeField] private float velocidadPersecucion = 0.2f;
    private Transform jugador;
    [SerializeField] private bool persiguiendo;
    private MovimientoOndaTriangular movimientoOndaTriangular;
    private RotacionSentidoHorario rotacionScript; // Referencia al script de rotación
    private int ultimaDireccion; // Variable para almacenar la última dirección
    [SerializeField] private float desplazamientoY = 2f; // Desplazamiento en Y para que el enemigo no esté directamente sobre el jugador sino un poco más arriba

    void Start()
    {
        movimientoOndaTriangular = GetComponent<MovimientoOndaTriangular>();
        // Obtener la referencia al script de rotación
        rotacionScript = transform.Find("EspirituDeLuzSprite").GetComponent<RotacionSentidoHorario>();
    }
    void Update()
    {
        if (persiguiendo)
        {
            Vector3 direccion = (jugador.position - transform.position).normalized;

            // Cambiar dirección si es necesario
            if ((direccion.x > 0 && movimientoOndaTriangular.direccion < 0) || (direccion.x < 0 && movimientoOndaTriangular.direccion > 0))
            {
                movimientoOndaTriangular.CambiarDireccion();
            }

            //transform.position += direccion * velocidadPersecucion * Time.deltaTime;

            // Ajustar la posición del enemigo para que sobrevuele por encima del jugador
            Vector3 nuevaPosicion = new Vector3(jugador.position.x, jugador.position.y + desplazamientoY, jugador.position.z);
            transform.position = Vector3.MoveTowards(transform.position, nuevaPosicion, velocidadPersecucion * Time.deltaTime);

            // Almacenar la última dirección en la que se movía el jugador
            ultimaDireccion = direccion.x > 0 ? 1 : -1;

            // Actualizar la dirección de rotación 
            rotacionScript.moviendoDerecha = ultimaDireccion == 1;
        }
    }

    public void IniciarPersecucion(Transform objetivo)
    {
        jugador = objetivo;
        persiguiendo = true;
        // Mostrar por consola a quién está persiguiendo
        /*if (jugador.CompareTag("Player"))
        {
            Debug.Log("Persiguiendo a Sam");
        }
        else if (jugador.CompareTag("Max"))
        {
            Debug.Log("Persiguiendo a Max");
        }*/
    }

    public void DetenerPersecucion()
    {
        persiguiendo = false;

        // Establecer la dirección del movimiento a la última dirección conocida del jugador
        movimientoOndaTriangular.EstablecerDireccion(ultimaDireccion);
    }
}
