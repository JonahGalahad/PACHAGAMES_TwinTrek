using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Asegúrate de agregar esta línea para usar TextMeshPro

public class SC_CollectableCounter : MonoBehaviour
{
    TMP_Text counterText;
    int initialCollectables = 0; // Agrega esta variable para almacenar el valor inicial de las monedas


    // Start is called before the first frame update
    void Start()
    {
        counterText = GetComponent<TMP_Text>();
        SC_2DCollectable.totalCollectables = initialCollectables; // Establece el valor inicial de los coleccionables
        counterText.text = SC_2DCollectable.totalCollectables.ToString(); // Actualiza el texto del contador
    }

    // Update is called once per frame
    void Update()
    {
        //Set the current number of coins to display
        if(counterText.text != SC_2DCollectable.totalCollectables.ToString())
        {
            counterText.text = SC_2DCollectable.totalCollectables.ToString();
        }
    }
}
