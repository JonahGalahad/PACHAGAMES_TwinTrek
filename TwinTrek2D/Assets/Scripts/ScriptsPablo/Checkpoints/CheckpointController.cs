using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    public Transform[] checkpoints; // Array para guardar los checkpoints
    public Transform zonaDeInicio; // Referencia a la ZonaDeInicio
    private Vector3 posicionDeReinicio; // Posición de respawn
    private int indiceCheckpointActivado = -1; // Índice del último checkpoint activado
    private Checkpoint checkpointActual; // Referencia al checkpoint actualmente activado

    private void Start()
    { 
        // Inicializar la posición de respawn en la ZonaDeInicio 
        if (zonaDeInicio != null) 
        { 
            posicionDeReinicio = zonaDeInicio.position; 
        } 
        else 
        { 
            Debug.LogError("No se ha asignado una ZonaDeInicio."); 
        }
    }

    // Método para actualizar la posición de respawn cuando se activa un checkpoint
    public void ActivarCheckpoint(int indice, Checkpoint nuevoCheckpoint)
    {
        if (indice >= 0 && indice < checkpoints.Length)
        {
            // Desactivar el checkpoint actual si existe
            if (checkpointActual != null)
            {
                checkpointActual.Desactivar();
            }

            // Activar el nuevo checkpoint
            indiceCheckpointActivado = indice;
            posicionDeReinicio = checkpoints[indice].position;
            checkpointActual = nuevoCheckpoint;
            Debug.Log("Checkpoint activado: " + indice + " Posición de respawn actualizada a: " + posicionDeReinicio);
        }
    }

    // Método para reiniciar la posición de los jugadores al último checkpoint activado
    public void ReiniciarDesdeCheckpoint()
    {
        if (indiceCheckpointActivado >= 0)
        {
            GameObject[] jugadores = GameObject.FindGameObjectsWithTag("Player");
            /*foreach (GameObject jugador in jugadores)
            {
                jugador.transform.position = posicionDeReinicio;
            }*/
            jugadores[0].transform.position = new Vector3(posicionDeReinicio.x - 2f,posicionDeReinicio.y,posicionDeReinicio.z);
            jugadores[0].GetComponent<SpriteRenderer>().flipX = false;
            jugadores[1].transform.position = new Vector3(posicionDeReinicio.x + 2f, posicionDeReinicio.y, posicionDeReinicio.z);
            jugadores[1].GetComponent<SpriteRenderer>().flipX = true;
            Debug.Log("Jugadores reiniciados desde el checkpoint: " + indiceCheckpointActivado);
        }
        else
        {
            Debug.LogWarning("No hay checkpoints activados. Reiniciando desde la ZonaDeInicio."); 
            if (zonaDeInicio != null) 
            {
                GameObject[] jugadores = GameObject.FindGameObjectsWithTag("Player"); 
                foreach (GameObject jugador in jugadores) 
                { 
                    jugador.transform.position = zonaDeInicio.position; 
                } 
            Debug.Log("Jugadores reiniciados desde la ZonaDeInicio."); 
            } 
        }
    }
}
