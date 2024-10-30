using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeteccionJugador : MonoBehaviour
{
    [SerializeField] private ControladorDeLuz controladorDeLuz;
    private PerseguirJugadores perseguidor; // Esta variable es para asignar el script PerseguirJugadores para que cuando colisione con jugadores se llame al método Perseguir que está en el padre
    private int jugadoresEnRango = 0; // Contador de jugadores dentro del rango de detección

    void Start()
    {
        // Obtiene la referencia al componente PerseguirJugadores en el padre
        perseguidor = GetComponentInParent<PerseguirJugadores>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Inicia el control de luces cuando se detecta un jugador con el tag "Sam" o "Max"
        if (collision.CompareTag("Player"))
        {
            if (jugadoresEnRango == 0)
            {
                // Inicia la persecución
                perseguidor.IniciarPersecucion(collision.transform);
            }
            jugadoresEnRango++;
            
            controladorDeLuz.ActivarLuces(); // Llama al método para controlar las luces
            //Debug.ClearDeveloperConsole();
            Debug.Log("Jugador entró en el rango de Espiritu de Luz");
            //perseguidor.IniciarPersecucion(collision.transform);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        // Detiene el control de luces cuando el jugador sale del área de colisión
        if (collision.CompareTag("Player"))
        {
            jugadoresEnRango--;
            if (jugadoresEnRango == 0)
            {
                perseguidor.DetenerPersecucion();
                controladorDeLuz.DesactivarLuces(); // Llama al método para resetear las luces
                Debug.Log("Jugador salió del rango de Espiritu de Luz");          
            }                    
            //controladorDeLuz.DesactivarLuces(); // Llama al método para resetear las luces
            //Debug.ClearDeveloperConsole();
            //Debug.Log("Jugador salió del rango de Espiritu de Luz");

            // Detiene la persecución
            //perseguidor.DetenerPersecucion();
        }
    }
}