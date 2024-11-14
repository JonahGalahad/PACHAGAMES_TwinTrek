using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonaDeVictoria : MonoBehaviour
{
    private int jugadoresEnZona = 0;
    private MySceneManager sceneManager;

    private void Start()
    {
        // Asignar la referencia al SceneManager al inicio
        sceneManager = FindObjectOfType<MySceneManager>();
        
        if (sceneManager == null)
        {
            Debug.LogError("No se encontró el MySceneManager en la escena.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jugadoresEnZona++;
            if (jugadoresEnZona == 2) // Ambos jugadores han llegado a la zona de victoria
            {
                if (sceneManager != null)
                {
                    sceneManager.MostrarCanvasVictoriaDeNivel();
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jugadoresEnZona--;
        }
    }
}

