using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerMenu : MonoBehaviour
{
  /*  private void Update()
    {
        if (Vida.vida <= 0)
        {
            Derrota();
        }
        
    }*/
   
    public void NuevaPartida()
    {
        SceneManager.LoadScene("Sceness/OnboardingStory");
        Vida.vida = 10;
    }

    public void MenuPrincipal()
    {
        SceneManager.LoadScene("menu");
    }

   /* public void Derrota()
    {
       
            SceneManager.LoadScene("Derrotaa", LoadSceneMode.Single);
        
    }*/

    public void Reintentar()
    {
        SceneManager.LoadScene("Sceness/Nivel1");
        Vida.vida = 10;
    }
    
    public void Salir()
    {
        Application.Quit();
    }
}
