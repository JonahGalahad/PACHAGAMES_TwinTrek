using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalancaScript : MonoBehaviour
{
    [Header("Plataforma y Sprites")]
    private SpriteRenderer spriteRenderer;
    private Sprite originalSprite;
    [SerializeField] private Sprite newSprite;
    [SerializeField] private GameObject plataforma;
    [SerializeField] private List<Collider2D> jugadoresAccionando = new List<Collider2D>(); // Lista para almacenar las posiciones de los jugadores
    [SerializeField] private int asignarPlataforma; //1 es multiplayer, 2 es local

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalSprite = spriteRenderer.sprite;

        // Carga el sprite desde la carpeta "Resources"
        newSprite = Resources.Load<Sprite>("Sprites/Objects/palanca_activada"); // Ruta dentro de la carpeta Resources
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Cambiar al sprite activado
            spriteRenderer.sprite = newSprite;
            if(asignarPlataforma == 1)
            {
                plataforma.GetComponent<PlataformaScript>().MoverPlataformaPuntoB();
            }
            else if(asignarPlataforma == 2)
            {
                plataforma.GetComponent<PlataformaHLocalScript>().MoverPlataformaPuntoB();
            }
            // Agrega el jugador a la lista si no está ya
            if (!jugadoresAccionando.Contains(collision))
            {
                jugadoresAccionando.Add(collision);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Remover el jugador de la lista
            jugadoresAccionando.Remove(collision);

            // Si aún hay jugadores dentro, cambiar el destino al último que queda
            if (jugadoresAccionando.Count <= 0)
            {
                // Restaurar al sprite desactivado
                spriteRenderer.sprite = originalSprite;
                if (asignarPlataforma == 1)
                {
                    plataforma.GetComponent<PlataformaScript>().MoverPlataformaPuntoA();
                }
                else if (asignarPlataforma == 2)
                {
                    plataforma.GetComponent<PlataformaHLocalScript>().MoverPlataformaPuntoA();
                }
            }
        }
    }
}
