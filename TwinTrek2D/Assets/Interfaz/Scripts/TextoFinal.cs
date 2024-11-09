using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextoFinal : MonoBehaviour
{
    public GameObject objectToActivate;  // El GameObject que deseas activar
    public float delay = 7f;             // Tiempo de espera en segundos (puedes ajustar este valor en el Inspector)

    void Start()
    {
        Invoke("ActivateObject", delay);
    }

    void ActivateObject()
    {
        // Activa el objeto deseado
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
        }
    }
}
