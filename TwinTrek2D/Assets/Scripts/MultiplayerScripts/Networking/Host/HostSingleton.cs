using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostSingleton : MonoBehaviour
{
    private static HostSingleton instance;

    public HostGameManager GameManager { get; private set; }

    //Comprueba si la instancia  es nula o no
    public static HostSingleton Instance

    {
        get
        {
            //Si la instancia no es nula, devuelve instancia
            if (instance != null) { return instance; }

            //Buscara a un cliente en la escena
            instance = FindObjectOfType<HostSingleton>();

            //Si la instancia es nula
            if (instance == null)
            {
                //Mandara un error a la consola
                Debug.LogError("No HostSingleton en la escena");
                return null;
            }

            //Si al final logra encontrarlo entonces se hara uso de esta linea
            return instance;
        }
    }

    private void Start()
    {
        //Para que no desaparezca cuando cambiemos de escenas
        DontDestroyOnLoad(gameObject);
    }

    //Creacion del cliente
    public void CreateHost()
    {
        GameManager = new HostGameManager();

        //Para que el codigo despues de este espere a que este se termine de procesar
        //await GameManager.InitAsync();
    }
}