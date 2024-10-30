using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_2DCoin : MonoBehaviour
{
    //Keep track of total picked coins (Since the value is static, it can be accessed at "SC_2DCoin.totalCoins" from any script)
    public static int totalCoins = 0;
    public int puntos;

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Si el objeto con la etiqueta "Sam" o "Max" entra en contacto con la moneda
        if (collision.CompareTag("Sam") || collision.CompareTag("Max"))
        {
            // Añadir la moneda al contador
            totalCoins = totalCoins + puntos;
            // Imprimir el número total de monedas (para depuración)
            Debug.Log("Tienes actualmente " + totalCoins + " estrellas.");
            // Destruir la moneda
            Destroy(gameObject);
        }
    }
}
