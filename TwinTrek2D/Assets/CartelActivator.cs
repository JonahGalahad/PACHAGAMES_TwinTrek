using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Asegúrate de agregar esta línea para usar TextMeshPro

public class CartelActivator : MonoBehaviour
{
    public GameObject cartelTextMesh; // Asigna el objeto TextMesh Pro UI del cartel en el Inspector
    public GameObject cartelTrepar; // Asigna el objeto "CartelTrepar" en el Inspector
    public GameObject cartelFondo; // Asigna el objeto "CartelTrepar" en el Inspector
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Max"))
        {
            // Activa el cartel
            cartelTextMesh.SetActive(true);
            cartelFondo.SetActive(true);
            Debug.Log("OnTriggerEnter2D called Cartel");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Max"))
        {
            // Desactiva el cartel y su hijo
            cartelTrepar.SetActive(false);
        }
    }
}


