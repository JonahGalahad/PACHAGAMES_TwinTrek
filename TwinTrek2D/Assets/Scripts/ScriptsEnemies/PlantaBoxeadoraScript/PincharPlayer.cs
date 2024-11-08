using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PincharPlayer : MonoBehaviour
{
    [Header("danio al jugador")]
    private GameManager gameManager;
    [SerializeField] private float danio = 5f;

    [Header("fuerza de empuje al jugador")]
    [SerializeField] private float fuerzaRebote = 5.0f; // Fuerza del rebote al tocar al player

    private void Start()
    {
        // Encuentra el GameManager en la escena
        gameManager = FindObjectOfType<GameManager>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Aplica una fuerza de rebote al player
            Rigidbody2D rbPlayer = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rbPlayer != null)
            {
                Vector2 direccionRebote = (collision.transform.position - transform.position).normalized;
                rbPlayer.AddForce(direccionRebote * fuerzaRebote, ForceMode2D.Impulse);
            }
            // Llama al método QuitarVidaXEnemigo en el GameManager para restar vida
            gameManager.QuitarVidaXEnemigo(danio);
            Debug.Log("auch");
        }
    }
}
