using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorScript : MonoBehaviour
{
    public GameObject colisionGolem;
    private GolemScript golemScript;

    public List<Transform> jugadoresDentro = new List<Transform>(); // Lista para almacenar las posiciones de los jugadores
    private void Start()
    {
        golemScript = colisionGolem.GetComponent<GolemScript>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Agrega el jugador a la lista si no está ya
            if (!jugadoresDentro.Contains(collision.transform))
            {
                jugadoresDentro.Add(collision.transform);
                // Cambiar el destino al último jugador detectado
                golemScript.CambiarModoAlerta(jugadoresDentro.Count, collision.transform);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Remover el jugador de la lista
            jugadoresDentro.Remove(collision.transform);

            // Si aún hay jugadores dentro, cambiar el destino al último que queda
            if (jugadoresDentro.Count > 0)
            {
                Transform ultimoJugador = jugadoresDentro[jugadoresDentro.Count - 1];
                golemScript.CambiarModoAlerta(0, ultimoJugador);
            }
            else
            {
                // Si no hay más jugadores, volver al destino previo
                golemScript.RestaurarDestinoPrevio();
            }
        }
    }
}
