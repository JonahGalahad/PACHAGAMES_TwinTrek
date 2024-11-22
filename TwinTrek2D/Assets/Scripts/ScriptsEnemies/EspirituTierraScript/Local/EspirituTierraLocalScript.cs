using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class EspirituTierraLocalScript : MonoBehaviour
{
    private Vector2 puntoOrigen;
    private SpriteRenderer spriteRenderer;
    private int sortinOrderInicial;
    [SerializeField] private int sortinOrderFinal = -4;
    [SerializeField] private bool trampaActivada = false;
    [SerializeField] private int destino;
    [SerializeField] private bool calcular = false;

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
    public StudioEventEmitter TierraSound { get { return tierraSound; } set { tierraSound = value; } }

    private void Start()
    {
        puntoOrigen = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        sortinOrderInicial = spriteRenderer.sortingOrder;

        //moveDownEvent = RuntimeManager.CreateInstance(moveDownSound);
        //moveDownEvent.start();
        //updateMoveDownParameter(false);
    }

    private void Update()
    {
        IrDestino();
        /*if(mover) {
            updateMoveDownParameter(true);
        } else {updateMoveDownParameter(false);}*/
        if (calcular)
        {
            CalcularDistancia();
        }
    }

    public void IrDestino() //Metodo para dirigirse hacia su destino
    {
        if (mover)
        {
            transform.position = Vector3.MoveTowards(transform.position, siguienteDestino, velocidad * Time.deltaTime);
        }
    }

    public void CalcularDistancia()
    {
        float distance = Mathf.Sqrt(Mathf.Pow(transform.position.x - siguienteDestino.x, 2) + Mathf.Pow(transform.position.y - siguienteDestino.y, 2));

        if (destino == 2 && distance < 1f)
        {
            siguienteDestino = punto2.transform.position;
            destino = 3;
        }
        else if (destino == 3 && distance < 1f)
        {
            siguienteDestino = punto3.transform.position;
            destino = 0;
        }
        else if (destino == 0 && distance < 1f)
        {
            calcular = false;
            StartCoroutine(LanzarJugador());
        }
    }

    IEnumerator MoverArriba()
    {
        mover = true;
        siguienteDestino = new Vector2(puntoOrigen.x, 5f);
        yield return new WaitForSeconds(3f);
        if (jugadorCapturado)
        {
            trampaActivada = false;
            yield break;
        }
        siguienteDestino = puntoOrigen;
        yield return new WaitForSeconds(3f);
        if (jugadorCapturado)
        {
            trampaActivada = false;
            yield break;
        }
        trampaActivada = false;
        especialCollider.enabled = true;
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
            yield return new WaitForSeconds(0.5f);
            jugadorCapturado = false;
            jugadorRB = null;
            jugador = null;
            tierraSound.Stop();
        }
        mover = false;
        destino = 1;
        velocidad = 1;
        siguienteDestino = puntoOrigen;
        transform.position = puntoOrigen;
        especialCollider.enabled = true;
        spriteRenderer.sortingOrder = sortinOrderInicial;
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


        spriteRenderer.sortingOrder = sortinOrderInicial;


        mover = false;
        destino = 1;
        siguienteDestino = puntoOrigen;
        transform.position = puntoOrigen;
    }
    public void ReinicioDeValores()
    {

        transform.position = puntoOrigen;
        mover = false;
        destino = 1;
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
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!trampaActivada)
            {
                especialCollider.enabled = false;
                trampaActivada = true;
                StartCoroutine(MoverArriba());
                tierraSound.Stop();
            }
            else
            {
                //PlayerLocal player = jugador.GetComponent<PlayerLocal>();
                tierraSound.Play();
                if (!jugadorCapturado) //significa que puede atrapar
                {
                    spriteRenderer.sortingOrder = sortinOrderFinal;
                    collision.gameObject.GetComponent<PlayerLocal>().EstarAtrapado(); //Le dice al jugador que esta atrapado
                    jugadorRB = collision.gameObject.GetComponent<Rigidbody2D>();
                    jugador = collision.gameObject;
                    collision.gameObject.GetComponent<Transform>().position = this.transform.position; //le dice al jugador que tome su posicion.
                    jugador.transform.SetParent(transform);
                    //collision.transform.SetParent(transform);
                    jugadorCapturado = true;
                    //desactivar sonidos del jugador
                    jugador.GetComponent<PlayerLocal>().updateWalkParameter(false);
                    jugador.GetComponent<PlayerLocal>().updateClimbParameter(false) ;
                    velocidad = velocidadMax;
                    siguienteDestino = punto1.position;
                    destino = 2;
                    //mover = true;
                    calcular = true;
                }
            }

        }
    }

    //private void updateMoveDownParameter(bool isMovingDown) { moveDownEvent.setParameterByName("IsMove", isMovingDown ? 1f : 0f); }
}
