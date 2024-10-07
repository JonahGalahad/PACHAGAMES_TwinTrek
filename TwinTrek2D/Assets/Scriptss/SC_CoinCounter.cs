using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Asegúrate de agregar esta línea para usar TextMeshPro

public class SC_CoinCounter : MonoBehaviour
{
    TMP_Text counterText;
    int initialCoins = 0; // Agrega esta variable para almacenar el valor inicial de las monedas


    // Start is called before the first frame update
    void Start()
    {
        counterText = GetComponent<TMP_Text>();
        SC_2DCoin.totalCoins = initialCoins; // Establece el valor inicial de las monedas
        UpdateCoinText(); // Llama a esta función para actualizar el texto inicial
        //counterText.text = SC_2DCoin.totalCoins.ToString(); // Actualiza el texto del contador
    }

    // Update is called once per frame
    void Update()
    {
        //Set the current number of coins to display
        if(counterText.text != SC_2DCoin.totalCoins.ToString())
        {
            //counterText.text = SC_2DCoin.totalCoins.ToString();
            UpdateCoinText();            
        }
    }

    void UpdateCoinText()
    {
        // Formatea el valor de las monedas con 4 cifras
        string formattedCoins = SC_2DCoin.totalCoins.ToString("D4");
        counterText.text = formattedCoins;
    }    
}
