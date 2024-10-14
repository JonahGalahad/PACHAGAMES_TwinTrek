using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_2DCollectable : MonoBehaviour
{
    //Keep track of total picked coins (Since the value is static, it can be accessed at "SC_2DCollectable.totalCoins" from any script)
    public static int totalCollectables = 0; 

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
            totalCollectables++;
            // Imprimir el número total de monedas (para depuración)
            Debug.Log("Tienes actualmente " + totalCollectables + " coleccionables.");
            // Destruir la moneda
            Destroy(gameObject);
        }
    }
}
