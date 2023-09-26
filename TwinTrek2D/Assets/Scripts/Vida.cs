using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Vida : MonoBehaviour
{
    public bool juntos = false;
    public int maxVida = 10;
    [SerializeField] public int vida = 10;
    private float tiempoUltimaRestaDeVida = 0f;
    public float tiempoEntreRestas = 2f;
    public Slider barra;

    // private lazo_statusUnirJugadores unirJugadores; //AGREGADO
    void Start()
    {
   //     unirJugadores = GameObject.FindObjectOfType<lazo_statusUnirJugadores>(); //AGREGADO
    }

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

            // Actualiza el tiempo de la �ltima resta
            tiempoUltimaRestaDeVida = Time.time;     
        }

        if (juntos == true && Time.time - tiempoUltimaRestaDeVida >= tiempoEntreRestas)
        {
            // Realiza la suma de vida
            RecuperarVida();

            // Actualiza el tiempo de la �ltima suma
            tiempoUltimaRestaDeVida = Time.time;

        }
        if(vida > maxVida)
        {
            vida = maxVida;
            // unirJugadores.CambiarAColorBlanco(); // //AGREGADO Cambiar color a blanco cuando no se está tomando daño ni recuperando vida.
        }
        barra.value = vida;

    }
    private void TomarDanio()
    {
        vida -= 1;
    //    unirJugadores.CambiarAColorRojo(); // //AGREGADO Cambiar color a rojo cuando se toma daño.
    }

    private void RecuperarVida()
    {
        vida += 2;
   //     unirJugadores.CambiarAColorVerde(); // //AGREGADO Cambiar color a verde cuando se recupera vida.
    }

}
