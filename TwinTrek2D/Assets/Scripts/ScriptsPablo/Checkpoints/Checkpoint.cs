using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] GameObject hogueraPrendida;
    private CheckpointController controladorDeCheckpoints;
    private int jugadoresEnCheckpoint = 0;
    public Sprite spriteActivado; // Sprite que se usará cuando el checkpoint se active
    public Sprite spriteDesactivado; // Sprite que se usará cuando el checkpoint se desactive
    private SpriteRenderer spriteRenderer;
    //[SerializeField] private StudioEventEmitter checkFlame;

    private bool noActivarMas = false;

    private void Start()
    {
        // Encontrar el CheckpointController en la escena
        controladorDeCheckpoints = FindObjectOfType<CheckpointController>();

        if (controladorDeCheckpoints == null)
        {
            Debug.LogError("No se encontró el CheckpointController en la escena.");
        }

        // Obtener el SpriteRenderer del checkpoint
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            Debug.LogError("No se encontró un SpriteRenderer en el GameObject del checkpoint.");
        }

        // Establecer el sprite inicial como desactivado
        if (spriteRenderer != null && spriteDesactivado != null)
        {
            spriteRenderer.sprite = spriteDesactivado;
        }
    }

    private void OnTriggerEnter2D(Collider2D colision)
    {
        if (colision.CompareTag("Player"))
        {
            if (noActivarMas)
            {
                return;
            }
            jugadoresEnCheckpoint++;
            if (jugadoresEnCheckpoint == 2) // Ambos jugadores han llegado al checkpoint
            {
                int indiceCheckpoint = System.Array.IndexOf(controladorDeCheckpoints.checkpoints, transform); // Obtener el índice del checkpoint
                if (indiceCheckpoint != -1)
                {
                    controladorDeCheckpoints.ActivarCheckpoint(indiceCheckpoint, this);
                    // Cambiar el sprite del checkpoint al sprite activado
                    /*if (spriteRenderer != null && spriteActivado != null)
                    {
                        spriteRenderer.sprite = spriteActivado;
                        Debug.Log("Checkpoint activado: Cambiando sprite.");
                    }*/
                    hogueraPrendida.SetActive(true);
                    noActivarMas = true;
                    Debug.Log("Checkpoint activado: Cambiando sprite.");
                    //checkFlame.Play();
                }
                else
                {
                    Debug.LogError("El índice del checkpoint no se encontró en el array del CheckpointController.");
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D colision)
    {
        if (colision.CompareTag("Player"))
        {
            jugadoresEnCheckpoint--;
        }
    }

    // Método para desactivar el checkpoint y cambiar el sprite
    public void Desactivar()
    {
        /*if (spriteRenderer != null && spriteDesactivado != null)
        {
            spriteRenderer.sprite = spriteDesactivado;
            Debug.Log("Checkpoint desactivado: Cambiando sprite.");
        }*/
        hogueraPrendida.SetActive(false);
        Debug.Log("Checkpoint desactivado: Cambiando sprite.");
    }
    /*private CheckpointController controladorDeCheckpoints;
    [SerializeField] GameObject hogueraPrendida;
    private int jugadoresEnCheckpoint = 0;
    public Sprite spriteActivado; // Sprite que se usará cuando el checkpoint se active
    public Sprite spriteDesactivado; // Sprite que se usará cuando el checkpoint se desactive
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        // Encontrar el CheckpointController en la escena
        controladorDeCheckpoints = FindObjectOfType<CheckpointController>();

        if (controladorDeCheckpoints == null)
        {
            Debug.LogError("No se encontró el CheckpointController en la escena.");
        }

        // Obtener el SpriteRenderer del checkpoint
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            Debug.LogError("No se encontró un SpriteRenderer en el GameObject del checkpoint.");
        }

        // Establecer el sprite inicial como desactivado
        if (spriteRenderer != null && spriteDesactivado != null)
        {
            spriteRenderer.sprite = spriteDesactivado;
        }
    }

    private void OnTriggerEnter2D(Collider2D colision)
    {
        if (colision.CompareTag("Player"))
        {
            jugadoresEnCheckpoint++;
            if (jugadoresEnCheckpoint == 2) // Ambos jugadores han llegado al checkpoint
            {
                int indiceCheckpoint = System.Array.IndexOf(controladorDeCheckpoints.checkpoints, transform); // Obtener el índice del checkpoint
                if (indiceCheckpoint != -1)
                {
                    controladorDeCheckpoints.ActivarCheckpoint(indiceCheckpoint, this);
                    // Cambiar el sprite del checkpoint al sprite activado
                    /*if (spriteRenderer != null && spriteActivado != null)
                    {
                        hogueraPrendida.SetActive(true);
                        //spriteRenderer.sprite = spriteActivado;
                        Debug.Log("Checkpoint activado: Cambiando sprite.");
                    }*/
    /*hogueraPrendida.SetActive(true);
    //spriteRenderer.sprite = spriteActivado;
    Debug.Log("Checkpoint activado: Prendiendo hoguera.");
}
else
{
    Debug.LogError("El índice del checkpoint no se encontró en el array del CheckpointController.");
}
}
}
}

private void OnTriggerExit2D(Collider2D colision)
{
if (colision.CompareTag("Player"))
{
jugadoresEnCheckpoint--;
}
}

// Método para desactivar el checkpoint y cambiar el sprite
public void Desactivar()
{
/*if (spriteRenderer != null && spriteDesactivado != null)
{
hogueraPrendida.SetActive(false);
//spriteRenderer.sprite = spriteDesactivado;
Debug.Log("Checkpoint desactivado: Cambiando sprite.");
}*/
    /*hogueraPrendida.SetActive(false);
    //spriteRenderer.sprite = spriteDesactivado;
    Debug.Log("Checkpoint desactivado: Prendiendo hoguera.");
}*/
}

