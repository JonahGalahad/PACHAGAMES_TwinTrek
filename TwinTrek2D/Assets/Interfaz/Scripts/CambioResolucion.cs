using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CambioResolucion : MonoBehaviour
{
    // Referencia al componente TextMeshProUGUI
    public TextMeshProUGUI resolutionText;

    private int width;
    private int height;
    public bool fullScreen;
    public GameObject panel;

    private int newResolution;

    public void NextResolution()
    {
        newResolution++;
        Resolutions();
    }

    public void BackResolution()
    {
        newResolution--;
        Resolutions();
    }

    public void ToggleFullScreen()
    {
        fullScreen = !fullScreen;
        if(fullScreen)
        {
            panel.SetActive(true);
        }else{
            panel.SetActive(false);
        }
        
        Screen.SetResolution(width, height, fullScreen);
    }

    private void Resolutions()
    {
        // Limitar el rango de las resoluciones
        newResolution = Mathf.Clamp(newResolution, 0, 3);

        // Asignar el ancho y alto dependiendo de la resolución seleccionada
        switch (newResolution)
        {
            case 0:
                width = 1024;
                height = 576;
                break;
            case 1:
                width = 1280;
                height = 720;
                break;
            case 2:
                width = 1366;
                height = 768;
                break;
            case 3:
                width = 1920;
                height = 1080;
                break;
        }

        // Actualizar el texto del componente TextMeshPro con la resolución actual
        resolutionText.text = width.ToString() + " x " + height.ToString();

        // Aplica la resolución inmediatamente
        Screen.SetResolution(width, height, fullScreen);
    }
}
