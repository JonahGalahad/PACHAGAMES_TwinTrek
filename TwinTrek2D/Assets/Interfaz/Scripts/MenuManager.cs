using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private static string nivelAnterior;
    [SerializeField] private string nivelInicial;
    [SerializeField] private string netbootstrap;

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
        SceneManager.LoadScene(nivelInicial);
    }

    public void MenuMultiplayer()
    {
        //carga de nivel1
        SceneManager.LoadScene(netbootstrap);
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
