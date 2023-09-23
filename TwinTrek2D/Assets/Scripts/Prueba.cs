using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prueba : MonoBehaviour
{
    
    public float salud = 100f;
    public bool estaQuemado = false;
    public float duracionQuemado = 10f;
    public float danoQuemadoPorTick = 5f;
    public float tiempoEntreTicks = 1f;
    private float tiempoUltimoTick;

    private void Update()
    {
        if (estaQuemado)
        {
            if (Time.time - tiempoUltimoTick >= tiempoEntreTicks)
            {
                AplicarDanoQuemado();
                tiempoUltimoTick = Time.time;
            }

            duracionQuemado -= Time.deltaTime;

            if (duracionQuemado <= 0)
            {
                QuitarQuemado();
            }
        }
    }

    private void AplicarDanoQuemado()
    {
        salud -= danoQuemadoPorTick;
        /*ActualizarUI();

        if (salud <= 0)
        {
            Morir();
        }*/
    }

    /*private void ActualizarUI()
    {
        // Implementa la actualización de la interfaz de usuario aquí
    }

    private void Morir()
    {
        // Implementa la lógica de muerte del personaje aquí
    }*/

    public void ActivarQuemado()
    {
        estaQuemado = true;
        tiempoUltimoTick = Time.time;
    }

    public void QuitarQuemado()
    {
        
        estaQuemado = false;
        duracionQuemado = 0f;
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        estaQuemado = false;
        duracionQuemado = 0f;
    }*/
}
