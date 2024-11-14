using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    [Header("Referencias de Canvas")]
    public GameObject canvasUI; // Referencia al CanvasUI
    public GameObject canvasVictoriaDeNivel; // Referencia al CanvasVictoriaDeNivel
    public GameObject canvasDerrota; // Referencia al CanvasDerrota
    
    private void Start()
    {
        // Asegurarse de que el Canvas de victoria está deshabilitado al inicio
        if (canvasVictoriaDeNivel != null)
        {
            canvasVictoriaDeNivel.SetActive(false);
        }
        if (canvasDerrota != null) 
        { 
            canvasDerrota.SetActive(false); 
        }
        Time.timeScale = 1; // Asegura de que el juego esté en marcha
    }

    // Método para cargar una escena específica por nombre
    public void CargarEscena(string nombreEscena)
    {
        Debug.Log("Cargando escena: " + nombreEscena);
        SceneManager.LoadScene(nombreEscena);
        StartCoroutine(EsperarYVerificarCarga(nombreEscena));
    }

    // Método para cargar la siguiente escena
    public void CargarSiguienteEscena()
    {
        string nombreEscenaActual = SceneManager.GetActiveScene().name;
        Debug.Log("Escena actual: " + nombreEscenaActual);

        string siguienteEscena = "";
        switch (nombreEscenaActual)
        {
            case "Level1":
                siguienteEscena = "Level2";
                break;
            /* EDITAR ESTO DEPENDIENDO DE CUÁNTOS NIVELES TENGAMOS
            case "Level2": 
                siguienteEscena = "Level3";
                break; 
            case "Level3": 
                siguienteEscena = "LevelFinal"; 
                break; 
            case "LevelFinal": 
                siguienteEscena = "MenuPrincipal";
                break;*/
            default:
                Debug.LogWarning("La escena actual no está configurada para continuar.");
                return;
        }
        Debug.Log("Intentando cargar " + siguienteEscena + "..."); 
        SceneManager.LoadScene(siguienteEscena); 
        StartCoroutine(EsperarYVerificarCarga(siguienteEscena));
    }

    private IEnumerator EsperarYVerificarCarga(string nombreEscena) 
    { 
        yield return null; // Espera un frame para permitir que la escena cargue
        
        Scene escena = SceneManager.GetActiveScene(); 
        Debug.Log("Escena cargada: " + escena.name);
        
        
        canvasUI = GameObject.Find("CanvasUI"); 
        Debug.Log("CanvasUI encontrado: " + (canvasUI != null));

        canvasVictoriaDeNivel = GameObject.Find("CanvasVictoriaDeNivel"); 
        Debug.Log("CanvasVictoriaDeNivel encontrado: " + (canvasVictoriaDeNivel != null)); 
        
        if (canvasVictoriaDeNivel != null) 
        { 
            canvasVictoriaDeNivel.SetActive(false); 
        }

        canvasDerrota = GameObject.Find("CanvasDerrota"); 
        Debug.Log("CanvasDerrota encontrado: " + (canvasDerrota != null)); 
        if (canvasDerrota != null) 
        { 
            canvasDerrota.SetActive(false); 
        }

        // Asegurarse de que el juego esté en marcha 
        Time.timeScale = 1;
    }

    public void MostrarCanvasVictoriaDeNivel()
    {
        if (canvasUI != null)
        {
            canvasUI.SetActive(false);
        }

        if (canvasVictoriaDeNivel != null)
        {
            canvasVictoriaDeNivel.SetActive(true);
        }

        Time.timeScale = 0;
    }

    public void MostrarDerrota() 
    { 
        if (canvasUI != null) 
        { 
            canvasUI.SetActive(false); 
        } 
        if (canvasDerrota != null) 
        { 
            canvasDerrota.SetActive(true); 
        } 
    Time.timeScale = 0;
    }

    // Método para cargar la escena MenuPrincipal 
    public void CargarMenuPrincipal() 
    { 
        Debug.Log("Cargando MenuPrincipal..."); 
        SceneManager.LoadScene("MenuPrincipal"); 
    } 
    
    // Método para reiniciar el nivel actual 
    public void ReiniciarNivel()
    {
        // Ocultar el CanvasDerrota
        if (canvasDerrota != null)
        {
            canvasDerrota.SetActive(false);
        }

        // Activar el CanvasUI 
        if (canvasUI != null) 
        { 
            canvasUI.SetActive(true); 
        }
        
        CheckpointController controladorDeCheckpoint = FindObjectOfType<CheckpointController>();
        if (controladorDeCheckpoint != null)
        {
            controladorDeCheckpoint.ReiniciarDesdeCheckpoint();
        }
        else
        {
            string nombreEscenaActual = SceneManager.GetActiveScene().name;
            Debug.Log("Reiniciando escena: " + nombreEscenaActual);
            SceneManager.LoadScene(nombreEscenaActual);
        }

        //Asegurar que el juego se reanude
        Time.timeScale = 1;
    }
}
