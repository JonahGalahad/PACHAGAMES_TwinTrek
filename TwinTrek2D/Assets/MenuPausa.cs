using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPausa : MonoBehaviour
{
    private bool estaPausado = false; // Variable para rastrear el estado de pausa
    [SerializeField] private GameObject menuPausa;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) // Detecta la tecla "Enter"
        {
            // Cambia el estado de pausa
            estaPausado = !estaPausado;

            // Aplica la lógica según el estado de pausa
            if (estaPausado)
            {
                PausarJuego();
            }
            else
            {
                ReanudarJuego();
            }
        }      
    }
    public void PausarJuego()
    {
        estaPausado = true;
        // Lógica para pausar el juego
        Time.timeScale = 0f; // Detiene la simulación del tiempo
        // Puedes mostrar un menú de pausa aquí si lo deseas
        menuPausa.SetActive(true);
    }

    public void ReanudarJuego()
    {
        estaPausado = false;
        // Lógica para reanudar el juego
        Time.timeScale = 1f; // Restaura la simulación del tiempo
        // Puedes ocultar el menú de pausa aquí si lo mostraste previamente
        menuPausa.SetActive(false);
    }
}
