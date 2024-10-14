using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemV1Script : MonoBehaviour
{
    //Codigo de lanzamiento del jugador
    public bool jugadorYaAtrapado = false;
    public float TiempoEsperaParaAtrapar = 3f;
    private Transform detectarLugarLanzamiento; //Variable que detecta la posicion donde lanzar al jugador
    public float minLaunchForce = 5f;  // Fuerza mínima de lanzamiento
    public float maxLaunchForce = 10f; // Fuerza máxima de lanzamiento
    public Rigidbody2D playerRb;
    private int direccionLanzamiento = 1;
    private int jugadorAtrapado = 0;

    [SerializeField] private LayerMask platformsLayerMask; //toma el layerMask que seria el piso para que el Golem pueda saltar

    private Rigidbody2D rigidbody2d; //toma el rigidbody del mismo Golem
    private Collider2D boxCollider2d; //toma el box collider del mismo Golem
    public float jumpVelocity = 5f; //para el alcance del salto
    public float moveSpeed = 5f; //para la velocidad de movimiento
    public float midAirControl = 3f; //controla el jugador en el aire, mientras mas valor tenga, el jugador podra controlar mejor su personaje en el aire

    public bool saltar = false;
    public bool moverseIzquierda = false;
    public bool moverseDerecha = false;
    private void Awake()
    {
        rigidbody2d = transform.GetComponent<Rigidbody2D>();
        boxCollider2d = transform.GetComponent<Collider2D>();
        moverseIzquierda = true;
        //audioSource = GetComponent<AudioSource>(); //AGREGADO MAXI

        detectarLugarLanzamiento = transform.Find("lugarL");
    }

    void Update()
    {

        if (IsGrounded() && saltar) //Si el golem esta en el suelo y tiene para saltar, el golem saltara
        {
            rigidbody2d.velocity = Vector2.up * jumpVelocity; //realiza el salto
            saltar = false;
        }
        if (jugadorYaAtrapado)
        {
            return;
        }
        HandleMovement();
    }
    private bool IsGrounded()
    {
        //Permite que el objeto conozca el suelo, en este caso esta como playermask que seria "piso" Luego le devuleve un valor
        RaycastHit2D raycastHit2d = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down, 0.1f, platformsLayerMask);
        return raycastHit2d.collider != null;

    }

    public void HandleMovement()
    {

        if (moverseIzquierda) //Al presionar la letra A
        {
            if (IsGrounded()) //pregunta si esta en el suelo, y si lo esta se movera
            {
                rigidbody2d.velocity = new Vector2(-moveSpeed, rigidbody2d.velocity.y);
            }
            else //si no esta en el suelo, puede moverse igual pero tendra un retraso en el aire
            {
                rigidbody2d.velocity += new Vector2(-moveSpeed * midAirControl * Time.deltaTime, 0);
                rigidbody2d.velocity = new Vector2(Mathf.Clamp(rigidbody2d.velocity.x, -moveSpeed, +moveSpeed), rigidbody2d.velocity.y);
            }

        }
        else
        {
            if (moverseDerecha) //Al presionar la letra D
            {
                if (IsGrounded()) //pregunta si esta en el suelo, y si lo esta se movera
                {
                    rigidbody2d.velocity = new Vector2(+moveSpeed, rigidbody2d.velocity.y);
                }
                else //si no esta en el suelo, puede moverse igual pero tendra un retraso en el aire
                {
                    rigidbody2d.velocity += new Vector2(+moveSpeed * midAirControl * Time.deltaTime, 0);
                    rigidbody2d.velocity = new Vector2(Mathf.Clamp(rigidbody2d.velocity.x, -moveSpeed, +moveSpeed), rigidbody2d.velocity.y);
                }
            }
            else
            {
                //si no apreta las teclas de movimiento, no se movera
                if (IsGrounded())
                {
                    rigidbody2d.velocity = new Vector2(0, rigidbody2d.velocity.y);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("puntoA"))
        {
            moverseIzquierda = false;
            moverseDerecha = true;
            this.gameObject.transform.Rotate(0, 180, 0);
        }
        if (collision.gameObject.CompareTag("puntoB"))
        {
            moverseDerecha = false;
            moverseIzquierda = true;
            this.gameObject.transform.Rotate(0, 180, 0);
        }
        if (collision.gameObject.CompareTag("puntoDeSalto"))
        {
            saltar = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Sam")) //collision con jugador 1 SAM
        {
            if (jugadorYaAtrapado == false) //significa que puede atrapar
            {
                playerRb = collision.gameObject.GetComponent<Rigidbody2D>();

                collision.gameObject.GetComponent<Player1>().EstarAtrapado(); //Le dice al jugador 1 (Sam) que esta atrapado
                collision.gameObject.GetComponent<Transform>().position = detectarLugarLanzamiento.position; //le dice al jugador que tome su posicion.
                Debug.Log("¡El enemigo atrapó al jugador 1!");
                jugadorYaAtrapado = true;
                jugadorAtrapado = 1;
                StartCoroutine(ArrojarJugador());
            }
        }

        if (collision.collider.CompareTag("Max")) //collision con jugador 2 MAM
        {
            if (jugadorYaAtrapado == false) //significa que puede atrapar
            {
                playerRb = collision.gameObject.GetComponent<Rigidbody2D>();

                collision.gameObject.GetComponent<Player2>().EstarAtrapado(); //Le dice al jugador 1 (Sam) que esta atrapado
                collision.gameObject.GetComponent<Transform>().position = detectarLugarLanzamiento.position; //le dice al jugador que tome su posicion.
                Debug.Log("¡El enemigo atrapó al jugador 2!");
                jugadorYaAtrapado = true;
                jugadorAtrapado = 2;
                StartCoroutine(ArrojarJugador());
            }
        }
    }

    IEnumerator ArrojarJugador()
    {
        yield return new WaitForSeconds(3f);
        if (playerRb != null)
        {
            // Calculamos una fuerza aleatoria
            float randomForceX = Random.Range(minLaunchForce, maxLaunchForce);
            float randomForceY = Random.Range(minLaunchForce, maxLaunchForce);

            // Aplicamos la fuerza al jugador
            if (moverseIzquierda)
            {
                direccionLanzamiento = -1;
            }
            else if (moverseDerecha)
            {
                direccionLanzamiento = 1;
            }
            Vector2 launchForce = new Vector2(randomForceX * direccionLanzamiento, randomForceY);
            playerRb.AddForce(launchForce, ForceMode2D.Impulse);
            if (jugadorAtrapado == 1)
            {
                playerRb.gameObject.GetComponent<Player1>().DejarEstarAtrapado();
            }
            else if (jugadorAtrapado == 2)
            {
                playerRb.gameObject.GetComponent<Player2>().DejarEstarAtrapado();
            }
            yield return new WaitForSeconds(1f);
            jugadorYaAtrapado = false;
            playerRb = null;

        }

    }

    public void Liberar()
    {
        StartCoroutine(DejarDeAtrapar());
    }

    IEnumerator DejarDeAtrapar()
    {
        yield return new WaitForSeconds(TiempoEsperaParaAtrapar);
        jugadorYaAtrapado = false;
        Debug.Log("¡El enemigo vuelve a moverse!");
    }


}
