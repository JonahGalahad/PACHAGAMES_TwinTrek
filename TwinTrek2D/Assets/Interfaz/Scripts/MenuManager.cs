using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private static string nivelAnterior;

    public void PantallaDerrota()
    {
        nivelAnterior = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("PantallaDerrota");
    }

    public void ReintentarNivel()
    {
        if (!string.IsNullOrEmpty(nivelAnterior))
        {
            SceneManager.LoadScene(nivelAnterior);
        }
        else
        {
            Debug.LogWarning("No hay un nivel guardado para reintentar.");
        }
    }

    public void IniciarJuego()
    {
        //carga de nivel1
        // SceneManager.LoadScene(nivelInicial);
    }
    public void MenuPrincipal()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }

    public void CerrarJuego()
    {
        Application.Quit();
    }
}
