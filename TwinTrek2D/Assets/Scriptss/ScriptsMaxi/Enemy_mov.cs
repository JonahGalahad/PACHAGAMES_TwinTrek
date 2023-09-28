using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_mov : MonoBehaviour
{
    public float velocidadMovimiento = 2.0f; // Velocidad de movimiento del enemigo.
    public float tiempoDeEspera = 3.0f; // Tiempo que el enemigo espera en cada direcci�n.
    private bool moviendoseIzquierda = true;
    private float tiempoUltimoCambio = 0.0f;

    void Start()
    {
        // Almacenamos la posici�n inicial del enemigo.
        tiempoUltimoCambio = Time.time;
    }

    public void Mover()
    {
        // Calculamos el nuevo desplazamiento.
        float desplazamiento = velocidadMovimiento * Time.deltaTime;

        // Verificamos si es hora de cambiar de direcci�n.
        if (Time.time - tiempoUltimoCambio >= tiempoDeEspera)
        {
            // Cambiamos la direcci�n del movimiento.
            moviendoseIzquierda = !moviendoseIzquierda;
            tiempoUltimoCambio = Time.time;
        }

        // Movemos el enemigo en la direcci�n correcta.
        if (moviendoseIzquierda)
        {
            transform.Translate(Vector2.left * desplazamiento);
        }
        else
        {
            transform.Translate(Vector2.right * desplazamiento);
        }
    }
}
