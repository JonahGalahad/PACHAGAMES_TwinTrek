using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgujasAbeja : MonoBehaviour
{
    [Header("danio al jugador")]
    private GameManager gameManager;
    [SerializeField] private float danio = 5f;

    private void Start()
    {
        // Encuentra el GameManager en la escena
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Llama al método QuitarVidaEspino en el GameManager para restar vida
            gameManager.QuitarVidaXEnemigo(danio);
            Debug.Log("auch");
        }
        
        if (collision.gameObject.CompareTag("Bloque"))
        {
            Destroy(this.gameObject);
        }

    }

    private void Update()
    {
        Destroy (this.gameObject,3f);
    }

}
