using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlataformaScript : MonoBehaviour
{
    [SerializeField] private Transform puntoA; // Punto A donde debe dirigirse
    [SerializeField] private Transform puntoB; // Punto B donde debe dirigirse
    [SerializeField] private float velocidad = 2.0f; // Velocidad con la que se mueve la plataforma
    [SerializeField] private bool mover = true; // Declaración de la variable mover
    [SerializeField] private BoxCollider2D plataformaTrigger;


    private Vector3 siguienteDestino; // Representa el destino donde debe dirigirse la plataforma

    private void Start()
    {
        if(puntoA != null || puntoB != null)
        {
            siguienteDestino = puntoA.position; // Inicia con un destino al principio
        }
    }

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.CompareTag("Player"))
        {
            plataformaTrigger.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            plataformaTrigger.enabled = false;
        }
    }
}
