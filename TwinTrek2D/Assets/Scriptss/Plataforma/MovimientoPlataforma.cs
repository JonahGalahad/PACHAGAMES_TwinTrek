using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPlataforma : MonoBehaviour
{
    public Transform puntoA; // Punto A donde debe dirigirse
    public Transform puntoB; // Punto B donde debe dirigirse
    public float velocidad = 2.0f; // Velocidad con la que se mueve la plataforma
    public bool mover = false; // Declaración de la variable mover

    private Vector3 siguienteDestino; // Representa el destino donde debe dirigirse la plataforma

    private void Start()
    {
        siguienteDestino = puntoA.position; // Inicia con un destino al principio
    }

    private void Update()
    {
        if (!mover)
        {
            transform.position = Vector3.MoveTowards(transform.position, puntoB.position, velocidad * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, siguienteDestino, velocidad * Time.deltaTime);
        }
    }

    public void MoverPlataformaPuntoA()
    {
        siguienteDestino = puntoA.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Max"))
        {
            // Hacer que el jugador sea hijo de la plataforma
            collision.transform.SetParent(transform);
            Debug.Log("Colisión con jugador: " + collision.gameObject.name);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Max"))
        {
            // Liberar al jugador de ser hijo de la plataforma
            collision.transform.SetParent(null);
            Debug.Log("Sale de la colisión con jugador: " + collision.gameObject.name);
        }
    }
}