using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GolemScript : MonoBehaviour
{
    private Rigidbody2D rigidbody2; //toma el box collider del mismo Golem
    private SpriteRenderer spriteRenderer;
    private Collider2D boxCollider2d; //toma el box collider del mismo Golem

    [SerializeField] private LayerMask platformsLayerMask; //toma el layerMask que seria el piso para que el Golem pueda saltar

    //Establecen el punto donde debe dirigirse el enemigo
    public GameObject pointA; //destino A
    public GameObject pointB; //destino B
    public Transform destino; //Punto donde debe dirigirse
    private Transform destinoPrevio; //variable que guarda el anterior destino donde se dirigia el enemigo
    //Velocidad del enemigo
    public float speed;

    private bool enemigoDetectado = false;

    private bool mirandoDerecha = true; // Si el personaje está mirando a la derecha
    private float sentidoEnX;
    private float sentidoEnY;
    private GameObject lugarLanzamiento;

    private GameObject pisoSobreGolem;
    private Collider2D pisoSobreG;

    private bool saltar = false;
    private bool estaSaltando = false;
    [SerializeField] private float jumpVelocity;

    //Codigo de lanzamiento del jugador
    //public bool jugadorYaAtrapado = false;
    //public float TiempoEsperaParaAtrapar = 3f;
    //private Transform detectarLugarLanzamiento; //Variable que detecta la posicion donde lanzar al jugador
    //public float minLaunchForce = 5f;  // Fuerza mínima de lanzamiento
    //public float maxLaunchForce = 10f; // Fuerza máxima de lanzamiento
    //public Rigidbody2D playerRigidBody;
    //private int direccionLanzamiento = 1;
    //private int jugadorAtrapado = 0;

    private GameObject[] bloques;
    [SerializeField] private float tiempoIgnorarColision; 

    void Start()
    {
        rigidbody2 = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2d = GetComponent<Collider2D>();
        lugarLanzamiento = GameObject.Find("lugarL");
        pisoSobreGolem = GameObject.Find("PisoSobreGolem");
        pisoSobreG = pisoSobreGolem.GetComponent<Collider2D>();
        destino = pointA.transform;

        // Ignora la colisión con los bloques
        bloques = GameObject.FindGameObjectsWithTag("Bloque");

        /*foreach (GameObject bloque in bloques)
        {
            Collider2D otherCollider = bloque.GetComponent<Collider2D>();
            Physics2D.IgnoreCollision(boxCollider2d, otherCollider);
            Physics2D.IgnoreCollision(pisoSobreG, otherCollider);
        }*/
    }

    void Update()
    {
        IrDestino();
        if (IsGrounded() && saltar) //Si el golem esta en el suelo y tiene para saltar, el golem saltara
        {
            rigidbody2.velocity = Vector2.up * jumpVelocity; //realiza el salto
            StartCoroutine(IgnorarColisiones());
            saltar = false;
            
        }
    }

    IEnumerator IgnorarColisiones()
    {
        foreach (GameObject bloque in bloques)
        {
            Collider2D otherCollider = bloque.GetComponent<Collider2D>();
            Physics2D.IgnoreCollision(boxCollider2d, otherCollider);
            Physics2D.IgnoreCollision(pisoSobreG, otherCollider);
        }
        yield return new WaitForSeconds(tiempoIgnorarColision);
        foreach (GameObject bloque in bloques)
        {
            Collider2D otherCollider = bloque.GetComponent<Collider2D>();
            Physics2D.IgnoreCollision(boxCollider2d, otherCollider,false);
            Physics2D.IgnoreCollision(pisoSobreG, otherCollider,false);
        }
        estaSaltando = false;
    }

    public void IrDestino()
    {
        sentidoEnX = transform.position.x - destino.position.x;
        sentidoEnY = transform.position.y - destino.position.y;
        //Debug.Log(IsGrounded());
        //transform.position = Vector2.MoveTowards(this.transform.position, destino.transform.position, speed * Time.deltaTime);
        Vector2 targetPosition = new Vector2(destino.transform.position.x, this.transform.position.y);
        transform.position = Vector2.MoveTowards(this.transform.position, targetPosition, speed * Time.deltaTime);
        //Debug.Log("sentido en X: "+sentidoEnX);
        //Debug.Log("sentido en Y: "+sentidoEnY);
        //spriteRenderer.flipX = transform.position.x > destino.position.x;
        //Cambia la direccion del objeto ataque ala izquierda o derecha
        //Debug.Log("sentido Y: "+(sentidoEnY < -10f));
        //Debug.Log("sentido X: "+(sentidoEnX < 1f || sentidoEnY > -1f));
        if (sentidoEnX < 0 && !mirandoDerecha)
        {
            CambiarDireccion();
        }
        else if (sentidoEnX > 0 && mirandoDerecha)
        {
            CambiarDireccion();
        }
        if (!estaSaltando && sentidoEnY < -10f && (sentidoEnX < 1f && sentidoEnX >= -1f))
        {
            estaSaltando = true;
            saltar = true;
        }
    }

    private bool IsGrounded()
    {
        //Permite que el objeto conozca el suelo, en este caso esta como playermask que seria "piso" Luego le devuleve un valor
        RaycastHit2D raycastHit2d = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down, 0.1f, platformsLayerMask);
        return raycastHit2d.collider != null;
    }

    void CambiarDireccion() //Funcion que cambia la direccion del objeto ataque
    {
        mirandoDerecha = !mirandoDerecha;
        Vector3 nuevaPosicion = lugarLanzamiento.transform.localPosition;
        nuevaPosicion.x = mirandoDerecha ? 1f : -1f;
        lugarLanzamiento.transform.localPosition = nuevaPosicion;
    }

    /*IEnumerator ArrojarJugador()
    {
        yield return new WaitForSeconds(3f);
        if (playerRigidBody != null)
        {
            // Calculamos una fuerza aleatoria
            float randomForceX = Random.Range(minLaunchForce, maxLaunchForce);
            float randomForceY = Random.Range(minLaunchForce, maxLaunchForce);

            // Aplicamos la fuerza al jugador
            if (destino = pointA.transform)
            {
                direccionLanzamiento = -1;
            }
            else if (destino = pointB.transform)
            {
                direccionLanzamiento = 1;
            }
            Vector2 launchForce = new Vector2(randomForceX * direccionLanzamiento, randomForceY);
            playerRigidBody.AddForce(launchForce, ForceMode2D.Impulse);
            if (jugadorAtrapado == 1)
            {
                playerRigidBody.gameObject.GetComponent<Player1>().DejarEstarAtrapado();
            }
            else if (jugadorAtrapado == 2)
            {
                playerRigidBody.gameObject.GetComponent<Player2>().DejarEstarAtrapado();
            }
            yield return new WaitForSeconds(1f);
            jugadorYaAtrapado = false;
            playerRb = null;

        }

    }*/

    /*public void Liberar()
    {
        StartCoroutine(DejarDeAtrapar());
    }*/

    /*IEnumerator DejarDeAtrapar()
    {
        yield return new WaitForSeconds(TiempoEsperaParaAtrapar);
        jugadorYaAtrapado = false;
        Debug.Log("¡El enemigo vuelve a moverse!");
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("puntoA") && !enemigoDetectado)
        {
            destino = pointB.transform;
        }
        if (collision.gameObject.CompareTag("puntoB") && !enemigoDetectado)
        {
            destino = pointA.transform;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Sam") || collision.gameObject.CompareTag("Max"))
        {
            // Obtener la posición del jugador y del enemigo
            Vector2 playerPosition = collision.transform.position;
            Vector2 enemyPosition = transform.position;
            //Calcular la diferencia en posiciones
            float differenceX = Mathf.Abs(playerPosition.x - enemyPosition.x);
            float differenceY = Mathf.Abs(playerPosition.y - enemyPosition.y);

            // Verificar si la colisión es lateral (más diferencia en X que en Y)
            if (differenceX > differenceY)
            {
                // Colisión lateral: Aplica Agarre
                Debug.Log("El enemigo agarro al jugador");
            }
            else
            {
                Debug.Log("El enemigo no agarro al jugador");
            }
        }
        /*if (collision.gameObject.CompareTag("Player"))
        {
            // Obtener la posición del jugador y del enemigo
            Vector2 playerPosition = collision.transform.position;
            Vector2 enemyPosition = transform.position;

            // Calcular la diferencia en posiciones
            float differenceX = Mathf.Abs(playerPosition.x - enemyPosition.x);
            float differenceY = Mathf.Abs(playerPosition.y - enemyPosition.y);

            // Verificar si la colisión es lateral (más diferencia en X que en Y)
            if (differenceX > differenceY)
            {
                // Colisión lateral: Aplica Agarre
                //collision.gameObject.GetComponent<PlayerScript>().QuitarVida(danioAtaque,transform);
                //StartCoroutine(Atacar());
            }
            else
            {
                //collision.gameObject.GetComponent<PlayerScript>().QuitarVida(1f, transform);
            }
        }*/
        /*if (collision.collider.CompareTag("Sam")) //collision con jugador 1 SAM
        {
            if (jugadorYaAtrapado == false) //significa que puede atrapar
            {
                playerRigidBody = collision.gameObject.GetComponent<Rigidbody2D>();

                collision.gameObject.GetComponent<Player1>().EstarAtrapado(); //Le dice al jugador 1 (Sam) que esta atrapado
                collision.gameObject.GetComponent<Transform>().position = lugarLanzamiento.transform.position; //le dice al jugador que tome su posicion.
                Debug.Log("¡El enemigo atrapó al jugador 1!");
                jugadorYaAtrapado = true;
                jugadorAtrapado = 1;
                StartCoroutine(ArrojarJugador());
            }
        }*/

        /*if (collision.collider.CompareTag("Max")) //collision con jugador 2 MAM
        {
            if (jugadorYaAtrapado == false) //significa que puede atrapar
            {
                playerRigidBody = collision.gameObject.GetComponent<Rigidbody2D>();

                collision.gameObject.GetComponent<Player2>().EstarAtrapado(); //Le dice al jugador 1 (Sam) que esta atrapado
                collision.gameObject.GetComponent<Transform>().position = lugarLanzamiento.transform.position; //le dice al jugador que tome su posicion.
                Debug.Log("¡El enemigo atrapó al jugador 2!");
                jugadorYaAtrapado = true;
                jugadorAtrapado = 2;
                StartCoroutine(ArrojarJugador());
            }
        }*/
    }
}
