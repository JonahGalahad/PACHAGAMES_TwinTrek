using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerMenu : MonoBehaviour
{
    public void NuevaPartida()
    {
        SceneManager.LoadScene("Nivel1");
    }

    public void MenuPrincipal()
    {
        SceneManager.LoadScene("menu");
    }
    
    public void Salir()
    {
        Application.Quit();
    }
}
