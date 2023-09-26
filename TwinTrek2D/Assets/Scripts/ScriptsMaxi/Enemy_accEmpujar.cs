using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_accEmpujar : MonoBehaviour
{
    [SerializeField] private float fuerzaEmpuje = 10.0f; // Magnitud de la fuerza de empuje.
    public bool direccionIzquierda = true;
    public float tiempoDeEspera = 3.0f; // Tiempo que el enemigo espera en cada dirección.
    private float tiempoUltimoCambio = 0.0f;

    private void Start()
    {
        // Almacenamos la posición inicial del enemigo.
        tiempoUltimoCambio = Time.time;
    }

    private void Update()
    {
        if (Time.time - tiempoUltimoCambio >= tiempoDeEspera)
        {
            // Cambiamos la dirección del movimiento.
            direccionIzquierda = !direccionIzquierda;
            tiempoUltimoCambio = Time.time;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //collision.gameObject.GetComponent<Player1_mov>().empujado = true;
            collision.gameObject.GetComponent<Player1_mov>().Empujado();

            // Calcula la dirección del empuje (hacia arriba).
            Vector2 direccionEmpuje1 = Vector2.up;
            direccionEmpuje1.Normalize();
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(direccionEmpuje1 * (fuerzaEmpuje - 5f), ForceMode2D.Impulse);
            if(direccionIzquierda)
            {
                // Calcula la dirección del empuje (hacia la izquierda).
                Vector2 direccionEmpuje = Vector2.left;
                // Normaliza la dirección del empuje y aplica la fuerza.
                direccionEmpuje.Normalize();
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(direccionEmpuje * fuerzaEmpuje, ForceMode2D.Impulse);
            }
            else
            {
                // Calcula la dirección del empuje (hacia la derecha).
                Vector2 direccionEmpuje = Vector2.right;
                // Normaliza la dirección del empuje y aplica la fuerza.
                direccionEmpuje.Normalize();
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(direccionEmpuje * fuerzaEmpuje, ForceMode2D.Impulse);
            }
        }

        if (collision.gameObject.CompareTag("Max"))
        {
            //collision.gameObject.GetComponent<Player1_mov>().empujado = true;
            collision.gameObject.GetComponent<Player2_mov>().Empujado();

            // Calcula la dirección del empuje (hacia arriba).
            Vector2 direccionEmpuje1 = Vector2.up;
            direccionEmpuje1.Normalize();
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(direccionEmpuje1 * (fuerzaEmpuje - 5f), ForceMode2D.Impulse);
            if (direccionIzquierda)
            {
                // Calcula la dirección del empuje (hacia la izquierda).
                Vector2 direccionEmpuje = Vector2.left;
                // Normaliza la dirección del empuje y aplica la fuerza.
                direccionEmpuje.Normalize();
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(direccionEmpuje * fuerzaEmpuje, ForceMode2D.Impulse);
            }
            else
            {
                // Calcula la dirección del empuje (hacia la derecha).
                Vector2 direccionEmpuje = Vector2.right;
                // Normaliza la dirección del empuje y aplica la fuerza.
                direccionEmpuje.Normalize();
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(direccionEmpuje * fuerzaEmpuje, ForceMode2D.Impulse);
            }
        }
    }
}
