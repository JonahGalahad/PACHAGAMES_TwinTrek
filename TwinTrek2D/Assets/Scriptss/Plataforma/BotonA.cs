using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotonA : MonoBehaviour
{
    public GameObject plataforma;
    [SerializeField] private LayerMask jugadorPlayer;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Max"))
        {
            // Cambiar al sprite activado
            spriteRenderer.sprite = Resources.Load<Sprite>("palanca_activada");

            plataforma.GetComponent<MovimientoPlataforma>().mover = true;
            plataforma.GetComponent<MovimientoPlataforma>().MoverPlataformaPuntoA();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Max"))
        {
            // Restaurar al sprite desactivado
            spriteRenderer.sprite = Resources.Load<Sprite>("palanca_desactivada");

            plataforma.GetComponent<MovimientoPlataforma>().mover = false;
        }
    }
}
