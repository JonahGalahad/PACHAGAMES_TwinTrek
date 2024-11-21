using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotacionSentidoHorario : MonoBehaviour
{
    [SerializeField] private float velocidadRotacion = 100f; // Velocidad de rotación en grados por segundo
    public bool moviendoDerecha = true; // Indica si se está moviendo a la derecha

    void Update()
    {
        RotarEnemigo();
    }

    void RotarEnemigo()
    {
        float direccionRotacion = moviendoDerecha ? -velocidadRotacion : velocidadRotacion;
        transform.Rotate(Vector3.forward, direccionRotacion * Time.deltaTime);
    }
}