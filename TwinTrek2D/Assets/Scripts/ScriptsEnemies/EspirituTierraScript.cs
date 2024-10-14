using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EspirituTierraScript : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public float distanceBetween;
    private Vector2 posicionInicial;
    private Quaternion rotacionInicial;

    [SerializeField] private bool seguirJugador = false;
    [SerializeField] private bool seguir = false;


    private float distance;

    private void Start()
    {
        player = null;
        posicionInicial = transform.position;
        rotacionInicial = transform.rotation;
    }


    // Update is called once per frame
    void Update()
    {
        if(seguirJugador)
        {
            Mover();
        }
        VerificarExisteJugador();
    }

    public void Mover()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);
    }

    public void VerificarExisteJugador() //metodo que verifica si el objeto sigue en el juego //Metodo para la mecanica de Atrapar de la FLOR
    {
        // Verificamos si la referencia sigue siendo válida
        if (player != null)
        {
            // Si el objeto se ha destruido, Unity lo reconocerá como null
            if (player == null)
            {
                Debug.Log("El objeto ha sido destruido.");
                player = null; // Aseguramos que la variable se establezca en null
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.collider.gameObject.CompareTag("Sam") || collision.collider.gameObject.CompareTag("Max"))
        {
            if(seguir == false)
            {
                StartCoroutine(SeguirJugador(collision));
                seguir = true;
                
            }
        }
    }

    IEnumerator SeguirJugador(Collision2D colision)
    {
        player = colision.gameObject;
        seguirJugador = true;
        yield return new WaitForSeconds(4f);
        seguirJugador = false;
        player = null;
        this.gameObject.transform.position = posicionInicial;
        this.gameObject.transform.rotation = rotacionInicial;
        yield return new WaitForSeconds(1f);
        seguir = false;
    }

}
