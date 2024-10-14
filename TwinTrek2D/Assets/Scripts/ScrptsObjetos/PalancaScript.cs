using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalancaScript : MonoBehaviour
{
    public GameObject plataforma;
    private SpriteRenderer spriteRenderer;
    private Sprite originalSprite;
    public Sprite newSprite;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalSprite = spriteRenderer.sprite;

        // Carga el sprite desde la carpeta "Resources"
        newSprite = Resources.Load<Sprite>("Sprites/Objects/palanca_activada"); // Ruta dentro de la carpeta Resources

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Sam") || collision.gameObject.CompareTag("Max"))
        {
            // Cambiar al sprite activado
            spriteRenderer.sprite = newSprite;
            plataforma.GetComponent<PlataformaScript>().MoverPlataformaPuntoB();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Sam") || collision.gameObject.CompareTag("Max"))
        {
            // Restaurar al sprite desactivado
            spriteRenderer.sprite = originalSprite;
            plataforma.GetComponent<PlataformaScript>().MoverPlataformaPuntoA();
        }
    }
}
