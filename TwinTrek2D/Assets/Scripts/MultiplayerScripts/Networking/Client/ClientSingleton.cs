using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ClientSingleton : MonoBehaviour
{
    private static ClientSingleton instance;

    public ClientGameManager GameManager { get; private set; }

    //Comprueba si la instancia  es nula o no
    public static ClientSingleton Instance

    {
        get
        {
            //Si la instancia no es nula, devuelve instancia
            if (instance != null) { return instance; }

            //Buscara a un cliente en la escena
            instance = FindObjectOfType<ClientSingleton>();

            //Si la instancia es nula
            if (instance == null)
            {
                //Mandara un error a la consola
                Debug.LogError("No ClientSingleton en la escena");
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
    public async Task<bool> CreateClient()
    {
        GameManager = new ClientGameManager();

        //Para que el codigo despues de este espere a que este se termine de procesar
      return await GameManager.InitAsync();
    }

}

