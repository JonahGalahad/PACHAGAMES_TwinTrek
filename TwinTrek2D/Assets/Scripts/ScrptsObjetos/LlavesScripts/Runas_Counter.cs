using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Runas_Counter : MonoBehaviour
{
    TextMeshProUGUI counterText;
    [SerializeField] private int cantidad;
    //int initialCollectables = 0; // Agrega esta variable para almacenar el valor inicial de las monedas


    // Start is called before the first frame update
    void Start()
    {
        if (counterText == null) // Si no se asigna desde el Inspector, intenta buscarlo automáticamente
        {
            counterText = GetComponent<TextMeshProUGUI>();
        }

        //SC_2DCollectable.totalCollectables = initialCollectables; // Establece el valor inicial de los coleccionables
        //counterText.text = SC_2DCollectable.totalCollectables.ToString(); // Actualiza el texto del contador
    }

    private void Update()
    {
        counterText.text = cantidad.ToString("F0"); // Formato sin decimales
    }

    public void SumarRunasObtenidas(int puntosEntrada)
    {
        cantidad += puntosEntrada;
    }
    public void RestarRunasObtenidas(int puntosEntrada)
    {
        cantidad -= puntosEntrada;
    }
}
