using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlorControllerScript : MonoBehaviour
{
    // Lista de flores atrapadoras
    [SerializeField] private GameManager gameManager;
    [SerializeField] private List<FlorLocalScript> flores;
    // Referencias a los jugadores
    [SerializeField] private GameObject[] player;

    [SerializeField] private float danio = 20f;

    private void Start()
    {
        StartCoroutine(EsperarJugadores()); // Inicia la corutina para buscar a los jugadores
    }


    private void Update()
    {
        // Comprobar si ambos jugadores están atrapados
        bool ambosAtrapados = player[0].GetComponent<PlayerLocal>().atrapado && player[1].GetComponent<PlayerLocal>().atrapado;
        //bool ambosAtrapados = jugador1.GetComponent<Flortrampa>().EstaAtrapado() && jugador2.GetComponent<Flortrampa>().EstaAtrapado();

        // Si ambos jugadores están atrapados, liberarlos
        if (ambosAtrapados)
        {
            LiberarJugadores();
            //StartCoroutine(LiberarJugadores());
        }
    }

    public void AgregarFlor(GameObject flor)
    {
        // Obtenemos el componente FlorLocalScript del objeto flor
        FlorLocalScript florScript = flor.GetComponent<FlorLocalScript>();

        // Asegurarnos de que la flor tiene el script antes de añadirla
        if (florScript != null)
        {
            flores.Add(florScript);
            Debug.Log("Flor agregada a la lista.");
        }
        else
        {
            Debug.LogWarning("La flor no tiene el script FlorLocalScript.");
        }
    }

    // Método para eliminar una flor de la lista
    public void EliminarFlor(FlorLocalScript flor)
    {
        if (flores.Contains(flor))
        {
            flores.Remove(flor);
            Debug.Log("Flor eliminada de la lista.");
        }
        else
        {
            Debug.LogWarning("La flor no está en la lista.");
        }
    }

    void LiberarJugadores()
    {
        // Iterar sobre las flores y liberar a los jugadores atrapados
        foreach (FlorLocalScript flor in flores)
        {
            flor.Liberar();
        }
        gameManager.QuitarVidaXEnemigo(danio);
    }



    /*IEnumerator LiberarJugadores()
    {
        yield return new WaitForSeconds(2f);
        // Iterar sobre las flores y liberar a los jugadores atrapados
        foreach (FlorLocalScript flor in flores)
        {
            flor.Liberar();
        }

    }*/

    IEnumerator EsperarJugadores()
    {
        // Espera hasta que se encuentren ambos jugadores.
        while (player.Length < 2)
        {
            player = GameObject.FindGameObjectsWithTag("Player");
            yield return null;
        }
        //estanjugadores = true; // Actualiza la bandera cuando ambos jugadores est�n presentes.
    }
}
