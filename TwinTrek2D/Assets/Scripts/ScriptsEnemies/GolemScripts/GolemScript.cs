using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class GolemScript : MonoBehaviour
{
    private GameObject padreGolem;
    private Rigidbody2D rigidbody2; //toma el rigidbody del mismo Golem

    private SpriteRenderer spriteRenderer; //toma el spriteRenderer para el flip
    private Collider2D boxCollider2d; //toma el box collider del mismo Golem

    private float sentidoEnX; //Establece la distancia en X entre el Golem y su destino
    private float sentidoEnY; //Establece la distancia en Y entre el Golem y su destino
    private bool mirandoDerecha = true; // Si el personaje est� mirando a la derecha
    private GameObject lugarLanzamiento; //toma el objeto del lugar de lanzamiento
    private GameObject detector; //toma el objeto de la deteccion de los jugadores
    private Collider2D pisoSobreGolem; //toma el collider del objeto piso sobre golem para que los jugadores puedan caminar sobre el

    [SerializeField] private GameObject[] bloques;

    [Header("Movimiento y Salto")]
    public float speed; //Velocidad del enemigo
    [SerializeField] private LayerMask platformsLayerMask; //toma el layerMask que seria el piso para que el Golem pueda saltar

    [Header("Destino de Movimiento")]
    //Establecen el punto donde debe dirigirse el enemigo
    public GameObject pointA; //destino A
    public GameObject pointB; //destino B
    [SerializeField] private Transform destino; //Punto donde debe dirigirse
    private Transform destinoPrevio; //variable que guarda el anterior destino donde se dirigia el enemigo

    [Header("Salto")]
    private bool saltar = false;
    private bool estaSaltando = false;
    [SerializeField] private float jumpVelocity;
    [SerializeField] private float limiteSentidoEnX; //establecen el limite en x para que el golem salte
    [SerializeField] private float limiteSentidoEnY; //establece el limite en y para que el golem salte
    [SerializeField] private float tiempoIgnorarColision; //tiempo para ignorar colisiones cuando el enemigo salta

    [Header("Colisiones de los jugadores")]
    [SerializeField] private GameObject[] jugadores;
    [SerializeField] private float diferencia = 0f;
    //[SerializeField] private Collider2D sam;
    //[SerializeField] private Collider2D max;

    [Header("Mecanica Lanzamiento")]
    [SerializeField] private float minLaunchForce = 5f;  // Fuerza m�nima de lanzamiento
    [SerializeField] private float maxLaunchForce = 10f; // Fuerza m�xima de lanzamiento
    [SerializeField] private int direccionLanzamiento;

    [Header("Datos adicionales")]
    [SerializeField] private bool modoPatrullaje;
    [SerializeField] private bool jugadorYaAtrapado = false;

    //Sonido
    [SerializeField] private StudioEventEmitter lanzarSound;


    void Start()
    {
        padreGolem = transform.parent.gameObject;
        //Extraen datos del Golem
        rigidbody2 = padreGolem.GetComponent<Rigidbody2D>();
        spriteRenderer = padreGolem.GetComponent<SpriteRenderer>();
        boxCollider2d = GetComponent<Collider2D>();
        //Buscan objetos dentro del Golem
        lugarLanzamiento = GameObject.Find("lugarL");
        detector = GameObject.Find("Detector");
        pisoSobreGolem = GameObject.Find("PisoSobreGolem").GetComponent<Collider2D>();
        //establece el primer destino
        destino = pointA.transform;

        //Busca objetos fuero del Golem
        bloques = GameObject.FindGameObjectsWithTag("Bloque");
        jugadores = GameObject.FindGameObjectsWithTag("Player");

        //establece modo patrullaje true al principio
        modoPatrullaje = true;
    }

    void Update()
    {
        if (jugadorYaAtrapado)
        {
            return;
        }
        IrDestino();
        if (modoPatrullaje)
        {
            Saltar();
        }
    }

    public void IrDestino() //Metodo para dirigirse hacia su destino
    {
        Vector2 targetPosition = new Vector2(destino.transform.position.x, padreGolem.transform.position.y);
        padreGolem.transform.position = Vector2.MoveTowards(padreGolem.transform.position, targetPosition, speed * Time.deltaTime);

        spriteRenderer.flipX = transform.position.x < destino.position.x;

        sentidoEnX = padreGolem.transform.position.x - destino.position.x;
        //Cambia la direccion del objeto ataque ala izquierda o derecha
        if (sentidoEnX < 0 && !mirandoDerecha)
        {
            CambiarDireccion();
        }
        else if (sentidoEnX > 0 && mirandoDerecha)
        {
            CambiarDireccion();
        }
    }

    public void Saltar() //Metodo para el Salto del Golem
    {
        sentidoEnY = padreGolem.transform.position.y - destino.position.y;

        if (!estaSaltando && sentidoEnY < limiteSentidoEnY && (sentidoEnX < limiteSentidoEnX && sentidoEnX >= -limiteSentidoEnX)) //condiciones para hacer el salto del golem
        {
            estaSaltando = true;
            saltar = true;
        }

        if (IsGrounded() && saltar) //Si el golem esta en el suelo y tiene para saltar, el golem saltara
        {
            rigidbody2.velocity = Vector2.up * jumpVelocity; //realiza el salto
            StartCoroutine(IgnorarColisiones());
            detector.SetActive(false);
            saltar = false;
        }
    }

    private bool IsGrounded() //Bandera para confirmar si el Golem esta en el suelo con el layer mask "Piso"
    {
        //Permite que el objeto conozca el suelo, en este caso esta como playermask que seria "piso" Luego le devuleve un valor
        RaycastHit2D raycastHit2d = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down, 0.1f, platformsLayerMask);
        return raycastHit2d.collider != null;
    }

    void CambiarDireccion() //Funcion que cambia la direccion del objeto de lanzamiento
    {
        mirandoDerecha = !mirandoDerecha;
        Vector3 nuevaPosicion = lugarLanzamiento.transform.localPosition;
        Vector3 nuevaPosicionDetector = detector.transform.localPosition;
        nuevaPosicion.x = mirandoDerecha ? 2f : -2f;
        nuevaPosicionDetector.x = mirandoDerecha ? 3.5f : -3.5f;
        lugarLanzamiento.transform.localPosition = nuevaPosicion;
        detector.transform.localPosition = nuevaPosicionDetector;
    }

    public void CambiarModoAlerta(int cantidadJugadores, Transform targetsPosition) //Metodo que cambia el target del Golem (el destino a donde debe dirigirse)
    {
        modoPatrullaje = false;
        // Guardar el destino previo si es la primera vez que cambia
        if (cantidadJugadores == 1)
        {
            destinoPrevio = destino;
        }
        // Cambiar el destino a la posici�n del �ltimo jugador detectado
        destino = targetsPosition;
    }

    public void RestaurarDestinoPrevio() // M�todo para restaurar el destino previo cuando ya no hay jugadores
    {
        destino = destinoPrevio;
        modoPatrullaje = true;
    }

    IEnumerator IgnorarColisiones() //Metodo para ignorar las colisiones con los bloques y los jugadores cuando el Golem Salta
    {
        foreach (GameObject bloque in bloques)
        {
            Collider2D otherCollider = bloque.GetComponent<Collider2D>();
            Physics2D.IgnoreCollision(boxCollider2d, otherCollider);
            Physics2D.IgnoreCollision(pisoSobreGolem, otherCollider);
        }

        foreach (GameObject jugador in jugadores)
        {
            Collider2D otherCollider = jugador.GetComponent<Collider2D>();
            Physics2D.IgnoreCollision(boxCollider2d, otherCollider);
            Physics2D.IgnoreCollision(pisoSobreGolem, otherCollider);
        }
        yield return new WaitForSeconds(tiempoIgnorarColision);
        foreach (GameObject bloque in bloques)
        {
            Collider2D otherCollider = bloque.GetComponent<Collider2D>();
            Physics2D.IgnoreCollision(boxCollider2d, otherCollider, false);
            Physics2D.IgnoreCollision(pisoSobreGolem, otherCollider, false);
        }

        foreach (GameObject jugador in jugadores)
        {
            Collider2D otherCollider = jugador.GetComponent<Collider2D>();
            Physics2D.IgnoreCollision(boxCollider2d, otherCollider, false);
            Physics2D.IgnoreCollision(pisoSobreGolem, otherCollider, false);
        }
        estaSaltando = false;
        detector.SetActive(true);
    }

    IEnumerator ArrojarJugador(Rigidbody2D playerRB) //Funcion que sirve para arrojar a los jugadores
    {
        yield return new WaitForSeconds(2f);
        lanzarSound.Play();
        if (playerRB != null)
        {
            // Calculamos una fuerza aleatoria
            float randomForceX = Random.Range(minLaunchForce, maxLaunchForce);
            float randomForceY = Random.Range(minLaunchForce, maxLaunchForce);

            // Aplicamos la fuerza al jugador
            if (!mirandoDerecha)
            {
                direccionLanzamiento = -1;
            }
            else
            {
                direccionLanzamiento = 1;
            }
            Vector2 launchForce = new Vector2(randomForceX * direccionLanzamiento, randomForceY);

            playerRB.AddForce(launchForce, ForceMode2D.Impulse);
            playerRB.gameObject.GetComponent<PlayerLocal>().DejarEstarAtrapadoPorGolem();
            yield return new WaitForSeconds(1f);
            jugadorYaAtrapado = false;
            playerRB = null;

        }

    }

    public void RestaurarValoresIniciales()
    {
        // Restaurar destino inicial
        destino = pointA.transform;
        destinoPrevio = null;

        // Resetear velocidad y posici�n del Golem
        rigidbody2.velocity = Vector2.zero;
        padreGolem.transform.position = pointA.transform.position;

        // Resetear estados de salto y patrullaje
        estaSaltando = false;
        saltar = false;
        modoPatrullaje = true;

        // Habilitar colisiones
        detector.SetActive(true);
        jugadorYaAtrapado = false;

        // Restaurar flip de sprite a direcci�n predeterminada
        spriteRenderer.flipX = false;
        mirandoDerecha = true;

        // Resetear posiciones relativas
        lugarLanzamiento.transform.localPosition = new Vector3(2f, lugarLanzamiento.transform.localPosition.y, lugarLanzamiento.transform.localPosition.z);
        detector.transform.localPosition = new Vector3(3.5f, detector.transform.localPosition.y, detector.transform.localPosition.z);

        // Detener velocidades residuales y liberar jugadores atrapados
        foreach (GameObject jugador in jugadores)
        {
            Rigidbody2D playerRb = jugador.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                playerRb.velocity = Vector2.zero;
                playerRb.angularVelocity = 0f;
            }

            // Liberar jugadores atrapados
            PlayerLocal playerScript = jugador.GetComponent<PlayerLocal>();
            if (playerScript != null)
            {
                // Usar los m�todos existentes en PlayerLocal para liberar
                playerScript.DejarEstarAtrapadoPorGolem();


                // Opcional: Restablecer posici�n inicial si es necesario
                jugador.transform.position = playerScript.transform.position;
            }
        }

        Debug.Log("Valores iniciales del Golem y jugadores restaurados.");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("puntoA") && modoPatrullaje)
        {
            destino = pointB.transform;
        }
        if (collision.gameObject.CompareTag("puntoB") && modoPatrullaje)
        {
            destino = pointA.transform;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) //Colisiona con un jugador
        {
            // Obtener la posici�n del jugador y del enemigo
            Vector2 playerPosition = collision.transform.position;
            Vector2 enemyPosition = padreGolem.transform.position;
            //Calcular la diferencia en posiciones
            float differenceX = Mathf.Abs(playerPosition.x - enemyPosition.x);
            float differenceY = Mathf.Abs(playerPosition.y - enemyPosition.y);

            // Verificar si la colisi�n es lateral (m�s diferencia en X que en Y)
            if ((differenceX + diferencia) > differenceY)
            {
                // Colisi�n lateral: Aplica Agarre
                Debug.Log("El enemigo agarro al jugador");
                if (jugadorYaAtrapado == false) //significa que puede atrapar
                {
                    Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();

                    collision.gameObject.GetComponent<PlayerLocal>().EstarAtrapadoPorGolem(); //Le dice al jugador 2 (Max) que esta atrapado
                    collision.gameObject.GetComponent<Transform>().position = lugarLanzamiento.transform.position; //le dice al jugador que tome su posicion.
                    jugadorYaAtrapado = true;
                    StartCoroutine(ArrojarJugador(playerRb));
                }
            }
            else
            {
                Debug.Log("El enemigo no agarro al jugador");
            }
        }
    }
}
