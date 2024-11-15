using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Temporizador : MonoBehaviour
{
    [SerializeField] private float tiempoInicial = 300f; // 5 minutos en segundos 
    private float tiempoTotal;
    private TextMeshProUGUI textoTemporizador;
    private MySceneManager mySceneManager; // Nueva referencia al MySceneManager
    private bool tiempoAgotado = false; // Bandera para verificar si el tiempo se ha agotado

    private void Start()
    {
        // Asignar el componente TextMeshProUGUI una vez al inicio
        textoTemporizador = GetComponent<TextMeshProUGUI>();
        mySceneManager = FindObjectOfType<MySceneManager>(); // Buscar el MySceneManager en la escena
        ReiniciarTemporizador(); // Inicializar el tiempo total
    }

    private void Update()
    {
        if (tiempoTotal > 0 && !tiempoAgotado)
        {
            tiempoTotal -= Time.deltaTime;
            ActualizarTemporizador();
        }
        else if (!tiempoAgotado)
        {
            // Detener el tiempo para que no se vuelva negativo
            tiempoTotal = 0;
            tiempoAgotado = true;

            if (mySceneManager != null)
            {
                mySceneManager.MostrarDerrota(true);
            }
            else
            {
                Debug.LogError("No se encontró el MySceneManager en la escena.");
            }
        }
    }

    private void ActualizarTemporizador()
    {
        int minutos = Mathf.FloorToInt(tiempoTotal / 60);
        int segundos = Mathf.FloorToInt(tiempoTotal % 60);
        textoTemporizador.text = $"{minutos:D2}:{segundos:D2}";
    }

    public void ReiniciarTemporizador() 
    { 
        tiempoTotal = tiempoInicial; 
        tiempoAgotado = false; 
        ActualizarTemporizador();
    }
}


