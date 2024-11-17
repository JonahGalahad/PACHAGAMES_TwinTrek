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
    private GameManager gameManager; // Referencia al GameManager
    private EspirituTierraLocalScript espirituTierra;
    private PlayerLocal playerLocal;
    private Temporizador temporizador; // Referencia al Temporizador
    private CheckpointController controladorDeCheckpoint; // Referencia al CheckpointController
    private bool derrotaPorTiempo = false; // Bandera para verificar si la derrota fue causada por tiempo agotado

    private void Start()
    {
        // Asegurarse de que el Canvas de victoria y derrota están deshabilitados al inicio
        if (canvasVictoriaDeNivel != null)
        {
            canvasVictoriaDeNivel.SetActive(false);
        }
        if (canvasDerrota != null)
        {
            canvasDerrota.SetActive(false);
        }
        Time.timeScale = 1; // Asegura de que el juego esté en marcha

        // Buscar las referencias a otros componentes en la escena
        gameManager = FindObjectOfType<GameManager>();
        espirituTierra = FindObjectOfType<EspirituTierraLocalScript>();
        playerLocal = FindObjectOfType<PlayerLocal>();
        temporizador = FindObjectOfType<Temporizador>();
        controladorDeCheckpoint = FindObjectOfType<CheckpointController>();
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
            case "Level2":
                siguienteEscena = "Creditos";
                break;
            /* EDITAR ESTO DEPENDIENDO DE CUÁNTOS NIVELES TENGAMOS
            case "Level3": 
                siguiente escena = "LevelFinal"; 
                break; 
            case "LevelFinal": 
                siguiente escena = "MenuPrincipal";
                break;*/
            default:
                Debug.LogWarning("La escena actual no está configurada para continuar.");
                return;
        }
        Debug.Log("Intentando cargar " + siguienteEscena + "...");
        Time.timeScale = 1; // Asegura que la animación se reproduzca en la siguiente escena, ej: MenuPrincipal, Creditos
        SceneManager.LoadScene(siguienteEscena);
        StartCoroutine(EsperarYVerificarCarga(siguienteEscena));
    }

    // Coroutine para esperar y verificar la carga de la escena
    private IEnumerator EsperarYVerificarCarga(string nombreEscena)
    {
        yield return null; // Espera un frame para permitir que la escena cargue

        Scene escena = SceneManager.GetActiveScene();
        Debug.Log("Escena cargada: " + escena.name);

        // Buscar y desactivar canvas si es necesario
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
        //Time.timeScale = 1;
    }

    // Método para mostrar el canvas de victoria de nivel
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

        Time.timeScale = 0; // Pausar el juego
    }

    // Método para mostrar el canvas de derrota
    public void MostrarDerrota(bool porTiempo)
    {
        if (canvasUI != null)
        {
            canvasUI.SetActive(false);
        }
        if (canvasDerrota != null)
        {
            canvasDerrota.SetActive(true);
        }
        Time.timeScale = 0; // Pausar el juego
        derrotaPorTiempo = porTiempo; // Establecer la bandera según el tipo de derrota. Esta bandera es la que se lee en ReiniciarNivel que es el método que activa el click del botón Reintentar
    }

    // Método para cargar la escena MenuPrincipal 
    public void CargarMenuPrincipal()
    {
        Debug.Log("Cargando MenuPrincipal...");
        SceneManager.LoadScene("MenuPrincipal");
        Time.timeScale = 1; // Asegura que el juego esté en marcha
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

        // Reiniciar las variables del jugador
        if (gameManager != null)
        {
            playerLocal.DejarEstarAtrapado();
            espirituTierra.ReiniciarTodo();
            gameManager.ReiniciarVida();

        }

        // Reiniciar el temporizador
        if (temporizador != null)
        {
            temporizador.ReiniciarTemporizador();
        }

        // Reiniciar la posición de los jugadores       
        if (derrotaPorTiempo)
        {
            // Reiniciar desde la ZonaDeInicio
            if (controladorDeCheckpoint != null)
            {
                controladorDeCheckpoint.ReiniciarDesdeZonaDeInicio();
            }
            derrotaPorTiempo = false; // Resetear la bandera
        }
        else
        {
            // Reiniciar desde el Checkpoint
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
        }

        // Asegurar que el juego se reanude
        Time.timeScale = 1;
    }
    /*[Header("Referencias de Canvas")]
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
    /*default:
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
}*/
}
