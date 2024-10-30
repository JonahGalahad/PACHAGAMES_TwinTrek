using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ApplicationController : MonoBehaviour
{
    [SerializeField] private ClientSingleton clientPrefab;
    [SerializeField] private HostSingleton hostPrefab;

    private async void Start()
    {
        //Para que no desaparezca cuando cambiemos de escenas
        DontDestroyOnLoad(gameObject);

        //Determina o calcula si es que estamos usando un servidor dedicado
        //await hara que los demas codigos esperen a que este termine de procesar
        await LaunchInMode(SystemInfo.graphicsDeviceType == UnityEngine.Rendering.GraphicsDeviceType.Null);
    }


    private async Task LaunchInMode(bool isDedicatedServer)
    {
        //Si estamos usando un servidor dedicado, usara las siguientes lineas de codigo
        if (isDedicatedServer)
        {

        }
        //Si NO estamos usando un servidor dedicado, entonces usara estas
        else
        {
            //Instancia el prefab del host
            HostSingleton HostSingleton = Instantiate(hostPrefab);
            //Crea el host
            HostSingleton.CreateHost();

            //Instancia el prefab del cliente
            ClientSingleton clientSingleton = Instantiate(clientPrefab);
            //Hara que los siguientes codigos esperen a que este termine de procesar
            bool authenticated = await clientSingleton.CreateClient();

            if (authenticated)
            {
                clientSingleton.GameManager.GoToMenu();
            }
        }

    }


}

