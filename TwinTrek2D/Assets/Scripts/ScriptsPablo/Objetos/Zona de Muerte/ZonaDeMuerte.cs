using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonaDeMuerte : MonoBehaviour
{
    private MySceneManager sceneManager;

    private void Start()
    {
        // Asignar la referencia al MySceneManager al inicio
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
            Debug.Log("Jugador ha caído en la zona de muerte.");
            if (sceneManager != null)
            {
                sceneManager.MostrarDerrota();
            }
        }
    }
}

