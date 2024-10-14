using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlataformaScript : MonoBehaviour
{
    public Transform puntoA; // Punto A donde debe dirigirse
    public Transform puntoB; // Punto B donde debe dirigirse
    public float velocidad = 2.0f; // Velocidad con la que se mueve la plataforma
    public bool mover = true; // Declaración de la variable mover

    private Vector3 siguienteDestino; // Representa el destino donde debe dirigirse la plataforma

    private void Start()
    {
        siguienteDestino = puntoA.position; // Inicia con un destino al principio
    }

    /*private void Update()
    {

        if (!mover)
        {
            transform.position = Vector3.MoveTowards(transform.position, puntoB.position, velocidad * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, siguienteDestino, velocidad * Time.deltaTime);
        }
    }*/

    private void Update()
    {
        if(mover)
        {
            transform.position = Vector3.MoveTowards(transform.position, siguienteDestino, velocidad * Time.deltaTime);
        }
    }

    public void MoverPlataformaPuntoA()
    {
        siguienteDestino = puntoA.position;
    }

    public void MoverPlataformaPuntoB()
    {
        siguienteDestino = puntoB.position;
    }
}
