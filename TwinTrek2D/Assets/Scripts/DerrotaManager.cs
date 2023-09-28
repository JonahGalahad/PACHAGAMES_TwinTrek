using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DerrotaManager : MonoBehaviour
{
    private void Update()
    {
        if (Vida.vida <= 0)
        {
            Derrota();
        }
        if (Player_SceneVictoria.dentro == true)
        {
            GanarPartida();
        }
    }

    public void Derrota()
    {

        SceneManager.LoadScene("Derrotaa", LoadSceneMode.Single);

    }

    public void GanarPartida()
    {
        
        SceneManager.LoadScene("Creditos");

    }
}
