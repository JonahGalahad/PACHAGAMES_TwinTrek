using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlataformaVLocalScript : MonoBehaviour
{
    [SerializeField] private Transform punto; //Marca el punto donde debe dirigirse
    private Vector2 posicionInicial;
    [SerializeField] private float speed;
    private Vector3 siguienteDestino; // Representa el destino donde debe dirigirse la plataforma
    private int jugadoresArribaPlataforma = 0;

    private SpriteRenderer spriteRenderer;
    private Color colorInicial;

    private void Start()
    {
        posicionInicial = transform.position;
        siguienteDestino = posicionInicial;
        spriteRenderer = GetComponent<SpriteRenderer>();
        colorInicial = spriteRenderer.color;
    }


    // Update is called once per frame
    void Update()
    {
        Mover();
    }

    public void Mover()
    {
        transform.position = Vector3.MoveTowards(transform.position, siguienteDestino, speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
            siguienteDestino = punto.transform.position;
            jugadoresArribaPlataforma++;
            spriteRenderer.color = Color.green;
            //Debug.Log("funciona");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
            jugadoresArribaPlataforma--;
            if (jugadoresArribaPlataforma <= 0)
            {
                siguienteDestino = posicionInicial;
                spriteRenderer.color = colorInicial;
            }
        }
    }
}
