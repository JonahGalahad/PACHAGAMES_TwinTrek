using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_2DCoin : MonoBehaviour
{
    //Keep track of total picked coins (Since the value is static, it can be accessed at "SC_2DCoin.totalCoins" from any script)
    public static int totalCoins = 0; 

    void Awake()
    {
        //Make Collider2D as trigger 
        GetComponent<Collider2D>().isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D c2d)
    {
        // Si el objeto con la etiqueta "Player" o "Max" entra en contacto con la moneda
        if (c2d.CompareTag("Player") || c2d.CompareTag("Max"))
        {
            // Añadir la moneda al contador
            totalCoins = totalCoins + 100;
            // Imprimir el número total de monedas (para depuración)
            Debug.Log("Tienes actualmente " + totalCoins + " monedas.");
            // Destruir la moneda
            Destroy(gameObject);
        }
    }
}
