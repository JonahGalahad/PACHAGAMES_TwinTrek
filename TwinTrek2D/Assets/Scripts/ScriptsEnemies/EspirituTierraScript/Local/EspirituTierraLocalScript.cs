using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class EspirituTierraLocalScript : MonoBehaviour
{
    [SerializeField] private Vector3 puntoOrigen;
    [SerializeField] private float puntoDestinoY;

    private SpriteRenderer spriteRenderer;
    private int sortinOrderInicial;
    [SerializeField] private int sortinOrderFinal = -4;
    [SerializeField] private bool trampaActivada = false;

    //[SerializeField] private Transform jugador;
    [SerializeField] private Rigidbody2D jugadorRB;
    [SerializeField] private GameObject jugador;
    [SerializeField] private BoxCollider2D especialCollider;

    [SerializeField] private bool jugadorCapturado = false;
    [SerializeField] private float minLaunchForce = 5f;  // Fuerza m�nima de lanzamiento
    [SerializeField] private float maxLaunchForce = 10f; // Fuerza m�xima de lanzamiento

    [SerializeField] private Transform punto1; // Punto 1 donde debe dirigirse
    [SerializeField] private Transform punto2; // Punto 2 donde debe dirigirse
    [SerializeField] private Transform punto3; // Punto 3 donde debe dirigirse
    [SerializeField] private float velocidad = 2.0f; // Velocidad con la que se mueve la plataforma
    [SerializeField] private float velocidadMax = 6.0f; // Velocidad con la que se mueve la plataforma
    [SerializeField] private bool mover = false; // Declaraci�n de la variable mover

    [SerializeField] private Vector3 siguienteDestino; // Representa el destino donde debe dirigirse la plataforma

    //Variables para el instanciacion y control de sonido FMOD mediante Emiter
    [SerializeField] private StudioEventEmitter tierraSound;

    [SerializeField] Transform destinoFlip; //Punto donde debe mirar
    public StudioEventEmitter TierraSound { get { return tierraSound; } set { tierraSound = value; } }
    public bool JugadorCapturado { get { return jugadorCapturado; } set { jugadorCapturado = value; } }

    private void Start()
    {
        puntoOrigen = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        sortinOrderInicial = spriteRenderer.sortingOrder;
        siguienteDestino = puntoOrigen;

        //moveDownEvent = RuntimeManager.CreateInstance(moveDownSound);
        //moveDownEvent.start();
        //updateMoveDownParameter(false);
    }

    private void Update()
    {
        if ((destinoFlip != null))
        {
            spriteRenderer.flipX = transform.position.x < destinoFlip.position.x;
        }
        IrDestino();
    }

    public void IrDestino() //Metodo para dirigirse hacia su destino
    {
        if (mover)
        {
            transform.position = Vector3.MoveTowards(transform.position, siguienteDestino, velocidad * Time.deltaTime);
        }
    }

    IEnumerator MoverArriba()
    {
        mover = true;
        siguienteDestino = new Vector3(transform.position.x, puntoDestinoY,transform.position.z);
        yield return null;
        while (transform.position != siguienteDestino)
        {
            yield return null;
            if(jugadorCapturado)
            {
                yield break;
            }
        }
        StartCoroutine(MoverAbajo());
    }

    IEnumerator MoverAbajo()
    {
        siguienteDestino = puntoOrigen;
        yield return null;
        while (transform.position != siguienteDestino)
        {
            yield return null;
            if (jugadorCapturado)
            {
                yield break;
            }
        }
        especialCollider.enabled = true;
        trampaActivada = false;
    }

    IEnumerator DirigirseADestinos()
    {
        yield return null;
        siguienteDestino = punto1.transform.position;
        yield return null;
        while (transform.position != siguienteDestino)
        {
            yield return null;
        }
        siguienteDestino = punto2.transform.position;
        yield return null;
        while (transform.position != siguienteDestino)
        {
            yield return null;
        }
        siguienteDestino = punto3.transform.position;
        yield return null;
        while (transform.position != siguienteDestino)
        {
            yield return null;
        }
        StartCoroutine(LanzarJugador());
    }

    IEnumerator LanzarJugador()
    {
        yield return new WaitForSeconds(0.3f);
        jugador.transform.SetParent(null);
        if (jugadorRB != null)
        {
            // Calculamos una fuerza aleatoria
            float randomForceY = Random.Range(minLaunchForce, maxLaunchForce);

            Vector2 launchForce = new Vector2(0, randomForceY);

            jugadorRB.AddForce(launchForce, ForceMode2D.Impulse);
            jugadorRB.gameObject.GetComponent<PlayerLocal>().DejarEstarAtrapado();
            yield return new WaitForSeconds(1f);
            
            jugadorRB = null;
            jugador = null;
            tierraSound.Stop();
        }
        mover = false;
        velocidad = 1;
        siguienteDestino = puntoOrigen;
        transform.position = puntoOrigen;
        spriteRenderer.sortingOrder = sortinOrderInicial;
        yield return new WaitForSeconds(1f);
        jugadorCapturado = false;
        trampaActivada = false;
        especialCollider.enabled = true;
    }

    public void ReiniciarTodo()
    {
        // Resetear al jugador
        if (jugador != null)
        {
            jugador.transform.SetParent(null);
            jugadorRB.velocity = Vector2.zero;
            jugadorCapturado = false;
            jugador = null;
            jugadorRB = null;
        }

        ReinicioDeValores();


        especialCollider.enabled = true;
        StopAllCoroutines();

        spriteRenderer.sortingOrder = sortinOrderInicial;


        mover = false;
        siguienteDestino = puntoOrigen;
        transform.position = puntoOrigen;
    }
    public void ReinicioDeValores()
    {

        transform.position = puntoOrigen;
        mover = false;
        siguienteDestino = puntoOrigen;
        velocidad = 1;
        jugadorCapturado = false;
        jugadorRB = null;
        jugador = null;
        trampaActivada = false;
        spriteRenderer.sortingOrder = sortinOrderInicial;
        especialCollider.enabled = true;
        tierraSound.Stop();

    }

    public void ReiniciarEstadoJugador()
    {
        jugadorCapturado = false;
        jugadorRB = null;
        jugador = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !jugadorCapturado)
        {
            if (!trampaActivada)
            {
                especialCollider.enabled = false;
                trampaActivada = true;
                StartCoroutine(MoverArriba());
                tierraSound.Stop();
                destinoFlip = collision.transform;
            }
            else
            {
                StopAllCoroutines();
                jugadorCapturado = true;
                tierraSound.Play();
                spriteRenderer.sortingOrder = sortinOrderFinal;
                collision.gameObject.GetComponent<PlayerLocal>().EstarAtrapado(); //Le dice al jugador que esta atrapado
                jugadorRB = collision.gameObject.GetComponent<Rigidbody2D>();
                jugador = collision.gameObject;
                collision.gameObject.GetComponent<Transform>().position = this.transform.position; //le dice al jugador que tome su posicion.
                jugador.transform.SetParent(transform);
                //collision.transform.SetParent(transform);
                //desactivar sonidos del jugador
                //jugador.GetComponent<PlayerLocal>().WalkEvent.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                //jugador.GetComponent<PlayerLocal>().ClimbEvent.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                jugador.GetComponent<PlayerLocal>().updateWalkParameter(false);
                jugador.GetComponent<PlayerLocal>().updateClimbParameter(false);
                velocidad = velocidadMax;
                StartCoroutine(DirigirseADestinos());
            }

        }
    }
}
