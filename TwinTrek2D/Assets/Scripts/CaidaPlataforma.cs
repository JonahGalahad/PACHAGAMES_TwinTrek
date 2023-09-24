using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaidaPlataforma : MonoBehaviour
{
    private int velocidadY = -10; // Ajusta la velocidad Y según tus necesidades
    [SerializeField] private bool parado = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Player2"))
        {
            parado = true;
        }
    }

    private void Update()
    {
        if (parado == true)
        {
            StartCoroutine(Caida());
        }
    }
   

    private void Caer()
    {
        float movimientoY = velocidadY * Time.deltaTime;
        transform.Translate(0, movimientoY, 0);

    }

    IEnumerator Caida()
    {
        yield return new WaitForSeconds(2f);
        Caer();
    }
}

