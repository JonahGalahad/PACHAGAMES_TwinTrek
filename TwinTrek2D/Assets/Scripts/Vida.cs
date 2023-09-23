using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vida : MonoBehaviour
{
    public bool juntos = false;
    public int maxVida = 10;
    [SerializeField] public int vida = 10;
    private float tiempoUltimaRestaDeVida = 0f;
    public float tiempoEntreRestas = 2f;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player2"))
        {
            juntos = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player2"))
        {
            juntos = false;
        }
    }
    void Update()
    {
        if (juntos == false && Time.time - tiempoUltimaRestaDeVida >= tiempoEntreRestas)
        {
            // Realiza la resta de vida
            TomarDanio();

            // Actualiza el tiempo de la última resta
            tiempoUltimaRestaDeVida = Time.time;     
        }

        if (juntos == true && Time.time - tiempoUltimaRestaDeVida >= tiempoEntreRestas)
        {
            // Realiza la suma de vida
            RecuperarVida();

            // Actualiza el tiempo de la última suma
            tiempoUltimaRestaDeVida = Time.time;

        }
        if(vida > maxVida)
        {
            vida = maxVida;
        }
        
    }
    private void TomarDanio()
    {
        vida -= 1;
    }

    private void RecuperarVida()
    {
        vida += 2;
    }

}
