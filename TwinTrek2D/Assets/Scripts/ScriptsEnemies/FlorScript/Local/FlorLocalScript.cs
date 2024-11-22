using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlorLocalScript : MonoBehaviour
{
    [SerializeField] private GameObject[] player;
    [SerializeField] private bool jugadorYaAtrapado = false;
    [SerializeField] private GameObject playerAtrapado;
    [SerializeField] private bool paraAtrapar = true;
    private int jugadoresEnFlor = 0;

    [SerializeField] private FlorControllerScript controlador;

    private Sprite originalSprite;
    [SerializeField] private Sprite newSprite;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerAtrapado = null;
        originalSprite = spriteRenderer.sprite;
        //newSprite = Resources.Load<Sprite>("Sprites/Enemies/planta1"); // Ruta dentro de la carpeta Resources
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) //collision con cualquier jugador
        {
            jugadoresEnFlor++;
            if (paraAtrapar) //significa que puede atrapar
            {
                spriteRenderer.sprite = newSprite;
                playerAtrapado = collision.gameObject;
                collision.gameObject.GetComponent<PlayerLocal>().EstarAtrapado(); //Le dice al jugador 1 (Sam) que esta atrapado
                collision.gameObject.GetComponent<Transform>().position = this.gameObject.transform.position; //le dice al jugador que tome su posicion.
                paraAtrapar = false;
                jugadorYaAtrapado = true;
                //para que cuando este atrapado no se reproduzca el sfx
                PlayerLocal player = playerAtrapado.GetComponent<PlayerLocal>();
                player.updateWalkParameter(false);

                controlador.AgregarFlor(gameObject);
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            jugadoresEnFlor--;
            if (jugadoresEnFlor <= 0)
            {
                paraAtrapar = true;
                Debug.Log("La Flor puede volver a atrapar!");
                controlador.EliminarFlor(gameObject.GetComponent<FlorLocalScript>());
            }
        }
    }

    public void Liberar()
    {
        
        StartCoroutine(DejarDeAtrapar());
    }

    /*public bool JugadorYaAtrapado()
    {
       return jugadorYaAtrapado;
    }*/

    public bool JugadorYaAtrapado
    {
        get {  return jugadorYaAtrapado; }
    }

    public GameObject JugadorAtrapado
    {
        get { return playerAtrapado; }
    }
    IEnumerator DejarDeAtrapar()
    {
        Debug.Log("Libera a mi compa!");
        spriteRenderer.sprite = originalSprite;
        playerAtrapado.GetComponent<PlayerLocal>().DejarEstarAtrapado();
        playerAtrapado = null;
        jugadorYaAtrapado = false;
        yield return null;
    }

    public void RestaurarValores()
    {
        spriteRenderer.sprite = originalSprite;
        paraAtrapar = true;            // Restablece el estado de la flor para poder atrapar
        jugadorYaAtrapado = false;     // Restablece el estado de que un jugador no est� atrapado
        playerAtrapado = null;         // Limpia la referencia al jugador atrapado
        jugadoresEnFlor = 0;           // Restablece el contador de jugadores en la flor
    }
}
