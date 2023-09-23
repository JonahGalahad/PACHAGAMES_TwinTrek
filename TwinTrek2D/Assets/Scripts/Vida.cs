using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vida : MonoBehaviour
{
    public bool juntos = false;
    public int maxVida = 10;
    [SerializeField] public int vida = 10;
    private float tiempoUltimaRestaDeVida = 0f;
    public float tiempoEntreRestas = 2f; // Ajusta el tiempo que desees entre restas de vida.

    private void OnTriggerStay2D(Collider2D collision)
    {
        juntos = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        juntos = false;
    }
    void Update()
    {
        if (juntos == false && Time.time - tiempoUltimaRestaDeVida >= tiempoEntreRestas)
        {
            // Realiza la resta de vida o la acci�n que desees aqu�.
            TomarDanio();

            // Actualiza el tiempo de la �ltima resta.
            tiempoUltimaRestaDeVida = Time.time;     
        }

        if (juntos == true && Time.time - tiempoUltimaRestaDeVida >= tiempoEntreRestas)
        {
            // Realiza la resta de vida o la acci�n que desees aqu�.
            RecuperarVida();

            // Actualiza el tiempo de la �ltima resta.
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
