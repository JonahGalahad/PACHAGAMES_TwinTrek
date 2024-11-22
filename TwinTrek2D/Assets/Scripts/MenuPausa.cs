using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPausa : MonoBehaviour
{
    private MyAudioManager myAudioM;

    private bool estaPausado = false; // Variable para rastrear el estado de pausa
    [SerializeField] private GameObject menuPausa;
    [SerializeField] private GameObject player1, player2;

    // Start is called before the first frame update
    void Start()
    {
        myAudioM = FindObjectOfType<MyAudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)) // Detecta la tecla "Enter"
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
        //PlayerLocal.Instance.updateWalkParameter(false);
        //PlayerLocal.Instance.updateClimbParameter(false);
        menuPausa.SetActive(true);

        //Variable para llamar al audioManager y detener los sonidos
        myAudioM.PararSonidos();
        /*player1.GetComponent<PlayerLocal>().WalkEvent.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        player1.GetComponent<PlayerLocal>().ClimbEvent.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);

        player2.GetComponent<PlayerLocal>().WalkEvent.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        player2.GetComponent<PlayerLocal>().ClimbEvent.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);*/
    }

    public void ReanudarJuego()
    {
        estaPausado = false;
        // Lógica para reanudar el juego
        Time.timeScale = 1f; // Restaura la simulación del tiempo
        // Puedes ocultar el menú de pausa aquí si lo mostraste previamente
        menuPausa.SetActive(false);
        player1.GetComponent<PlayerLocal>().WalkEvent.start();
        player1.GetComponent<PlayerLocal>().ClimbEvent.start();

        player2.GetComponent<PlayerLocal>().WalkEvent.start();
        player2.GetComponent<PlayerLocal>().ClimbEvent.start();
    }
}
