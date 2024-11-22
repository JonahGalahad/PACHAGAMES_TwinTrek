using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DejarBaba : MonoBehaviour
{
    [SerializeField] private GameObject slimeByDraggingPrefab; // Referencia al prefab SlimeByDragging
    private Transform puntoDeGeneracionDeBaba; // Punto desde el cual se genera la baba
    public bool inicializado = false; // Para verificar si la inicialización está completa

    void Start()
    {
        // Encuentra el punto de generación como hijo del Slime
        puntoDeGeneracionDeBaba = transform.Find("PuntoDeGeneracionDeBaba");

        if (puntoDeGeneracionDeBaba == null)
        {
            Debug.LogError("PuntoDeGeneracionDeBaba no encontrado. Asegúrate de que el objeto está correctamente nombrado y posicionado en la jerarquía.");
        }

        if (slimeByDraggingPrefab == null)
        {
            Debug.LogError("slimeByDraggingPrefab no asignado. Asegúrate de que el prefab está asignado en el inspector.");
        }

        // Marcar como inicializado si todo está en orden
        if (puntoDeGeneracionDeBaba != null && slimeByDraggingPrefab != null)
        {
            inicializado = true;
        }
    }

    public void DejarSlime()
    {
        if (inicializado)
        {
            Instantiate(slimeByDraggingPrefab, puntoDeGeneracionDeBaba.position, Quaternion.identity);
        }
        else
        {
            Debug.LogError("No se puede dejar slime porque los componentes necesarios no están asignados o la inicialización no ha terminado.");
        }
    }
}
