using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using Unity.VisualScripting;
using UnityEngine;

public class FuegoScript : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] private float danio = 5f;
    [SerializeField] private float tiempo = 0f;
    [SerializeField] private float tiempoEntreRestas = 1f;
    [SerializeField] private bool quemar = false;
    private BoxCollider2D fuegoCollider;
    private Rigidbody2D fuegoRB;
    //[SerializeField] private StudioEventEmitter fireSound;

    private void Start()
    {
        // Encuentra el GameManager en la escena
        gameManager = FindObjectOfType<GameManager>();
        if(this.gameObject.GetComponent<BoxCollider2D>() != null )
        {
            fuegoCollider = GetComponent<BoxCollider2D>();
        }
        if (this.gameObject.GetComponent<Rigidbody2D>() != null)
        {
            fuegoRB = GetComponent<Rigidbody2D>();
        }
        //fireSound.Play();
    }

    private void Update()
    {
        if (quemar && (Time.time - tiempo) >= tiempoEntreRestas)
        {
            // Llama al m�todo QuitarVidaEspino en el GameManager para restar vida
            gameManager.QuitarVidaXEnemigo(danio);
            Debug.Log("auch");
            tiempo = Time.time;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            quemar = true;
        }
        if (collision.gameObject.CompareTag("Bloque"))
        {
            // Cambiar el Rigidbody2D a Kinematic
            if (fuegoRB != null)
            {
                fuegoRB.bodyType = RigidbodyType2D.Static;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            quemar = false;
        }
    }
}
