using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerLobby : MonoBehaviour
{
    [SerializeField] private GameObject[] players;
    private bool estanjugadores = false;

    void Start()
    {
        // Podrías hacer alguna configuración inicial aquí si es necesario
        //CheckPlayersConnected();
        players = new GameObject[0]; // Inicializa el array para evitar null
        StartCoroutine(EsperarJugadores());
    }

    IEnumerator EsperarJugadores()
    {
        // Espera hasta que se encuentren ambos jugadores.
        while (players == null || players.Length < 2)
        {
            players = GameObject.FindGameObjectsWithTag("Player");
            yield return null;
        }
        // Evita que los jugadores se destruyan al cambiar de escena
        foreach (GameObject player in players)
        {
            DontDestroyOnLoad(player);
        }
        StartGame();
    }

    // Este método simularía la conexión de los jugadores
    /*public void OnPlayerConnected(int playerID)
    {
        if (playerID == 1)
            player1Connected = true;
        else if (playerID == 2)
            player2Connected = true;

        CheckPlayersConnected();
    }*/

    // Comprueba si ambos jugadores están conectados
    /*private void CheckPlayersConnected()
    {
        if (player1Connected && player2Connected)
        {
            StartGame();
        }
    }*/

    // Método para iniciar el juego cuando ambos jugadores están conectados
    private void StartGame()
    {
        Debug.Log("Ambos jugadores conectados. Iniciando juego...");
        // Carga la primera escena del juego (puedes ajustar el nombre de la escena aquí)
        SceneManager.LoadScene("PruebasScripts");
    }
}
