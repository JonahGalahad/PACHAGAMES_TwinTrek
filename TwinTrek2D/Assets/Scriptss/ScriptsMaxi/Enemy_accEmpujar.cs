using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_accEmpujar : MonoBehaviour
{
    [SerializeField] private float fuerzaEmpuje = 10.0f; // Magnitud de la fuerza de empuje.
    public bool direccionIzquierda = true;
    public float tiempoDeEspera = 3.0f; // Tiempo que el enemigo espera en cada direcci�n.
    private float tiempoUltimoCambio = 0.0f;

//
    private SpriteRenderer player2SpriteRenderer;
    private SpriteRenderer player1SpriteRenderer;

    private Color originalColor2;
    private Color originalColor1;
    private bool isBlinking2 = false;
    private bool isBlinking1 = false;


    [SerializeField] private float tiempoDeBlink = 0.5f; // Duración de cada parpadeo
    private float tiempoUltimoBlink2 = 0.0f;
    private float tiempoUltimoBlink1 = 0.0f;

    private void Start()
    {
        // Almacenamos la posici�n inicial del enemigo.
        tiempoUltimoCambio = Time.time;

        //
        player2SpriteRenderer = GameObject.FindGameObjectWithTag("Max").GetComponent<SpriteRenderer>();
        originalColor2 = player2SpriteRenderer.color;
        player1SpriteRenderer = GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>();
        originalColor1 = player1SpriteRenderer.color;
        tiempoUltimoBlink2 = Time.time;
    }

    private void Update()
    {
        if (Time.time - tiempoUltimoCambio >= tiempoDeEspera)
        {
            // Cambiamos la direcci�n del movimiento.
            direccionIzquierda = !direccionIzquierda;
            tiempoUltimoCambio = Time.time;
        }
        
        //
        if (isBlinking2 && Time.time - tiempoUltimoBlink2 >= tiempoDeBlink)
        {
            // Restaura el color original después de cada parpadeo.
            player2SpriteRenderer.color = originalColor2;
            isBlinking2 = false;
        }
        
        if (isBlinking1 && Time.time - tiempoUltimoBlink1 >= tiempoDeBlink)
        {
            // Restaura el color original después de cada parpadeo.
            player1SpriteRenderer.color = originalColor1;
            isBlinking1 = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //collision.gameObject.GetComponent<Player1_mov>().empujado = true;
            collision.gameObject.GetComponent<Player1_mov>().Empujado();
            collision.gameObject.GetComponent<Player1_mov>().tiempoCongeladoPorEmpuje = 1f;

            // Calcula la direcci�n del empuje (hacia arriba).
            Vector2 direccionEmpuje1 = Vector2.up;
            direccionEmpuje1.Normalize();
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(direccionEmpuje1 * (fuerzaEmpuje - 5f), ForceMode2D.Impulse);
            if(direccionIzquierda)
            {
                // Calcula la direcci�n del empuje (hacia la izquierda).
                Vector2 direccionEmpuje = Vector2.left;
                // Normaliza la direcci�n del empuje y aplica la fuerza.
                direccionEmpuje.Normalize();
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(direccionEmpuje * fuerzaEmpuje, ForceMode2D.Impulse);
            }
            else
            {
                // Calcula la direcci�n del empuje (hacia la derecha).
                Vector2 direccionEmpuje = Vector2.right;
                // Normaliza la direcci�n del empuje y aplica la fuerza.
                direccionEmpuje.Normalize();
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(direccionEmpuje * fuerzaEmpuje, ForceMode2D.Impulse);
            }
            
            // Cambia el color del sprite a una versión desaturada.
            player1SpriteRenderer.color = new Color(originalColor1.r * 0.5f, originalColor1.g * 0.5f, originalColor1.b * 0.5f, 0.7f);
            // Inicia el parpadeo.
            isBlinking1 = true;
            tiempoUltimoBlink1 = Time.time;
        }

        if (collision.gameObject.CompareTag("Max"))
        {
            //collision.gameObject.GetComponent<Player1_mov>().empujado = true;
            collision.gameObject.GetComponent<Player2_mov>().Empujado();
            collision.gameObject.GetComponent<Player2_mov>().tiempoCongeladoPorEmpuje = 1f;

            // Calcula la direcci�n del empuje (hacia arriba).
            Vector2 direccionEmpuje1 = Vector2.up;
            direccionEmpuje1.Normalize();
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(direccionEmpuje1 * (fuerzaEmpuje - 5f), ForceMode2D.Impulse);
            if (direccionIzquierda)
            {
                // Calcula la direcci�n del empuje (hacia la izquierda).
                Vector2 direccionEmpuje = Vector2.left;
                // Normaliza la direcci�n del empuje y aplica la fuerza.
                direccionEmpuje.Normalize();
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(direccionEmpuje * fuerzaEmpuje, ForceMode2D.Impulse);
            }
            else
            {
                // Calcula la direcci�n del empuje (hacia la derecha).
                Vector2 direccionEmpuje = Vector2.right;
                // Normaliza la direcci�n del empuje y aplica la fuerza.
                direccionEmpuje.Normalize();
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(direccionEmpuje * fuerzaEmpuje, ForceMode2D.Impulse);
            }
            
            // Cambia el color del sprite a una versión desaturada.
            player2SpriteRenderer.color = new Color(originalColor2.r * 0.5f, originalColor2.g * 0.5f, originalColor2.b * 0.5f, 0.7f);
            // Inicia el parpadeo.
            isBlinking2 = true;
            tiempoUltimoBlink2 = Time.time;
        }
    }
}
