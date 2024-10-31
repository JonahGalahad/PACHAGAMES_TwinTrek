using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgujasAbeja : MonoBehaviour
{
    //Daño de ataque
  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("choco contra jugador");
            //Vida.vida -= 3;
        }
        
        if (collision.gameObject.CompareTag("Bloque"))
        {
            Destroy(this.gameObject);
        }

    }

    private void Update()
    {
        Destroy (this.gameObject,3f);
    }

}
