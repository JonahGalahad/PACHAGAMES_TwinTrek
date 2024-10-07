using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_accEmpujar2 : MonoBehaviour
{
    [SerializeField] private float fuerzaEmpuje = 10.0f; // Magnitud de la fuerza de empuje.

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player1_mov>().Empujado();
            collision.gameObject.GetComponent<Player1_mov>().tiempoCongeladoPorEmpuje = 0f;

            // Calcula la direcci�n del empuje (hacia arriba).
            Vector2 direccionEmpuje1 = Vector2.up;
            direccionEmpuje1.Normalize();
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(direccionEmpuje1 * fuerzaEmpuje, ForceMode2D.Impulse);
        }

        if (collision.gameObject.CompareTag("Max"))
        {
            //collision.gameObject.GetComponent<Player1_mov>().empujado = true;
            collision.gameObject.GetComponent<Player2_mov>().Empujado();
            collision.gameObject.GetComponent<Player2_mov>().tiempoCongeladoPorEmpuje = 0f;

            // Calcula la direcci�n del empuje (hacia arriba).
            Vector2 direccionEmpuje1 = Vector2.up;
            direccionEmpuje1.Normalize();
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(direccionEmpuje1 * fuerzaEmpuje, ForceMode2D.Impulse);
        }
    }
}
