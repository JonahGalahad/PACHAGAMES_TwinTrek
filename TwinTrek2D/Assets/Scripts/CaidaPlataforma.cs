using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaidaPlataforma : MonoBehaviour
{
    private int velocidadY = -50; // Ajusta la velocidad Y según tus necesidades
    private int velocidadY2 = 50;
    [SerializeField] private bool parado = false;
    [SerializeField] private bool subiendo = false;
    [SerializeField] private GameObject plataforma;
    [SerializeField] private Transform pos;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Player2"))
        {
            parado = true;
            velocidadY = -70;
            velocidadY2 = 100;
        }
      
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            subiendo = true;
        }

     
    }

    private void FixedUpdate()
    {
        if (parado == true)
        {
            StartCoroutine(Caida());
        }

        if (subiendo == true)
        {
            StartCoroutine(Subida());
        }
    }
   

    private void Caer()
    {
        float movimientoY = velocidadY * Time.deltaTime;
        transform.Translate(0, movimientoY, 0);

    }

    private void Subir()
    {
        float movimientoY = velocidadY2 * Time.deltaTime;
        transform.Translate(0, movimientoY, 0);

    }

    IEnumerator Caida()
    {
        yield return new WaitForSeconds(2f);
        Caer();
        yield return new WaitForSeconds(1f);
        parado = false;
    }

    IEnumerator Subida()
    {
        yield return new WaitForSeconds(2f);
        /* Subir();
         yield return new WaitForSeconds(1f);*/
        plataforma.transform.position = pos.transform.position;
        subiendo = false;

    }
}

/*private int velocidadY = -10; // Ajusta la velocidad Y según tus necesidades
    private int velocidadY2 = 10;
    [SerializeField] private bool parado = false;
    [SerializeField] private bool subiendo = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Player2"))
        {
            parado = true;
        }
      
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            subiendo = true;
        }
    }

    private void Update()
    {
        if (parado == true)
        {
            StartCoroutine(Caida());
        }

        if (subiendo == true)
        {
            StartCoroutine(Subida());
        }
    }
   

    private void Caer()
    {
        float movimientoY = velocidadY * Time.deltaTime;
        transform.Translate(0, movimientoY, 0);

    }

    private void Subir()
    {
        float movimientoY = velocidadY2 * Time.deltaTime;
        transform.Translate(0, movimientoY, 0);

    }

    IEnumerator Caida()
    {
        yield return new WaitForSeconds(2f);
        Caer();
        yield return new WaitForSeconds(1f);
        parado = false;
    }

    IEnumerator Subida()
    {
        yield return new WaitForSeconds(2f);
        Subir();
        yield return new WaitForSeconds(1f);
        subiendo = false;
    }*/
