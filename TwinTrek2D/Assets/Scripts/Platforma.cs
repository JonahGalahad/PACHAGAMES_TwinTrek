using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platforma : MonoBehaviour
{
    public Transform puntoA; //el punto A donde debe dirigirse
    public Transform puntoB; //el punto B donde debe dirigirse
    [SerializeField] private LayerMask jugadorPlayer;
    public float velocidad = 2.0f; //la velocidad con la que se mueve la plataforma
    private Vector3 siguienteDestino; //representa al destino donde debe dirigirse la plataforma
    private bool mover = false; //bandera que permite el movimiento o no

    private void Start()
    {
        siguienteDestino = puntoB.position; //inicia con un denstino al principio
    }

    private void Update()
    {
        if (mover == true) //pregunta si debe moverse al siguiente destino
        {
            MoverPlataforma();
        }
        else //vuelve a donde se encontraba
        {
            MoverPlataformaInverso();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Max")) 
        {
            siguienteDestino = puntoB.position;
            mover = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Max"))
        {
            siguienteDestino = puntoA.position;
            mover = false;
        }
    }
    private void MoverPlataforma() //mueve la plataform al destino
    {
        transform.position = Vector3.MoveTowards(transform.position, siguienteDestino, velocidad * Time.deltaTime);
    }

    private void MoverPlataformaInverso() //mueve la plataforma al comienzo
    {
        siguienteDestino = puntoA.position;
        transform.position = Vector3.MoveTowards(transform.position, siguienteDestino, velocidad * Time.deltaTime);
    }
}
