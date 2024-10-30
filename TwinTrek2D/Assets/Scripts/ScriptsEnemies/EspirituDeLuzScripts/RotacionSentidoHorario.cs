using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotacionSentidoHorario : MonoBehaviour
{
    [SerializeField] private float velocidadRotacion = 100f; // Velocidad de rotación en grados por segundo

    void Update()
    {
        RotarEnemigo();
    }

    void RotarEnemigo()
    {
        transform.Rotate(Vector3.forward, -velocidadRotacion * Time.deltaTime);
    }
}