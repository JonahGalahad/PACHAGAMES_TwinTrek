using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class ZonaDeMuerte : MonoBehaviour
{
    private MySceneManager sceneManager;
    [SerializeField] GameObject player1,player2;

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
                player1.GetComponent<PlayerLocal>().WalkEvent.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                player1.GetComponent<PlayerLocal>().ClimbEvent.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                //player1.GetComponent<PlayerLocal>().WalkEvent.release();
                player2.GetComponent<PlayerLocal>().WalkEvent.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                player2.GetComponent<PlayerLocal>().ClimbEvent.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                //player2.GetComponent<PlayerLocal>().WalkEvent.release();
                //player1.GetComponent<PlayerLocal>().updateWalkParameter(false);
                //player1.GetComponent<PlayerLocal>().updateClimbParameter(false);
                //player2.GetComponent<PlayerLocal>().updateWalkParameter(false);
                //player2.GetComponent<PlayerLocal>().updateClimbParameter(false);
                sceneManager.MostrarDerrota(false); // El parametro false indica que no es derrota por tiempo
            }
        }
    }
    /*private MySceneManager sceneManager;

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
    }*/
}

