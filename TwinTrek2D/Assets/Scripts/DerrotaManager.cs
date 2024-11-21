using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DerrotaManager : MonoBehaviour
{
    private void Update()
    {
        /*if (Vida.vida <= 0)
        {
            Derrota();
        }*/
        if (Player_SceneVictoria.dentro == true)
        {
            GanarPartida();
        }
    }

    public void Derrota()
    {
        SceneAnimationController.Instance.FadeIn();
        SceneManager.LoadScene("Derrotaa", LoadSceneMode.Single);

    }

    public void GanarPartida()
    {
        SceneAnimationController.Instance.FadeIn();
        SceneManager.LoadScene("Creditos");

    }
}
