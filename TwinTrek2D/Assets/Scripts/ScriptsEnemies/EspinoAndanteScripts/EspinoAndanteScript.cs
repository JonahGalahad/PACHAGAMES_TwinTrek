using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EspinoAndanteScript : MonoBehaviour
{
    [Header("danio al jugador")]
    private GameManager gameManager;
    [SerializeField] private float danio = 10f;
    [Header("Destino de Movimiento")]
    //Establecen el punto donde debe dirigirse el enemigo
    [SerializeField] private GameObject pointA; //destino A
    [SerializeField] private GameObject pointB; //destino B
    [SerializeField] private Transform destino; //Punto donde debe dirigirse
    [SerializeField] private Transform destinoPrevio; //variable que guarda el anterior destino donde se dirigia el enemigo
    private float sentidoEnX; //Establece la distancia en X entre el Espino y su destino
    private bool mirandoDerecha = true; // Si el Espino está mirando a la derecha
    [SerializeField] private float moveSpeed = 5f; //para la velocidad de movimiento
    [SerializeField] private float maxSpeed = 10f; //maxima velocidad de movimiento (modo diablo)

    private GameObject padreEspino; //busca el objeto padre y lo guarda en la variable
    private Rigidbody2D rigidbody2; //toma el rigidbody del mismo Espino
    private SpriteRenderer spriteRenderer; //toma el spriteRenderer para el flip
    private GameObject detectarLugarLanzamiento;
    private GameObject detectarLugarLanzamiento2;
    private GameObject vision;
    [SerializeField] private bool modoDiablo = false;

    [Header("Codigo de lanzamiento")]
    //Codigo de lanzamiento del jugador
    [SerializeField] private Transform p2;
    [SerializeField] private Vector3 tamanio = new Vector3(2, 1, 2);
    [SerializeField] private Vector3 tamanioOr = new Vector3(2, 2, 2);
    [SerializeField] private int direccionLanzamiento = 1;
    [SerializeField] private float launchForceX = 15;
    [SerializeField] private float launchForceY = 8;

    private RotacionSentidoHorario rotacionScript; // Referencia al script de rotación

    private void Awake()
    {
        detectarLugarLanzamiento = GameObject.Find("PuntodeTiro");
        detectarLugarLanzamiento2 = GameObject.Find("PuntodeTiro2");
        vision = GameObject.Find("Vision");
    }

    private void Start()
    {
        // Encuentra el GameManager en la escena
        gameManager = FindObjectOfType<GameManager>();

        padreEspino = transform.parent.gameObject;
        //Extraen datos del Golem
        rigidbody2 = padreEspino.GetComponent<Rigidbody2D>();
        //spriteRenderer = padreEspino.GetComponent<SpriteRenderer>();
        spriteRenderer = padreEspino.transform.Find("Espino/EspinoSprite").GetComponent<SpriteRenderer>();

        // Obtener la referencia al script de rotación
        rotacionScript = padreEspino.transform.Find("Espino/EspinoSprite").GetComponent<RotacionSentidoHorario>();
        destino = pointA.transform;
    }

    void Update()
    {
        IrDestino();
    }

    public void IrDestino() //Metodo para dirigirse hacia su destino
    {
        Vector2 targetPosition = new Vector2(destino.transform.position.x, padreEspino.transform.position.y);
        padreEspino.transform.position = Vector2.MoveTowards(padreEspino.transform.position, targetPosition, moveSpeed * Time.deltaTime);

        spriteRenderer.flipX = transform.position.x < destino.position.x;
        sentidoEnX = padreEspino.transform.position.x - destino.position.x;
        
        // Actualizar la dirección de rotación
        rotacionScript.moviendoDerecha = sentidoEnX < 0;

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

    void CambiarDireccion() //Funcion que cambia la direccion del objeto de lanzamiento
    {
        mirandoDerecha = !mirandoDerecha;
        Vector3 nuevaPosicion = detectarLugarLanzamiento.transform.localPosition;
        Vector3 nuevaPosicion2 = detectarLugarLanzamiento2.transform.localPosition;
        Vector3 nuevaPosicionDetector = vision.transform.localPosition;
        nuevaPosicion.x = mirandoDerecha ? 1f : -1f;
        nuevaPosicion2.x = mirandoDerecha ? -0.95f : 0.95f;
        nuevaPosicionDetector.x = mirandoDerecha ? 6.70f : -2f;
        detectarLugarLanzamiento.transform.localPosition = nuevaPosicion;
        detectarLugarLanzamiento2.transform.localPosition = nuevaPosicion2;
        vision.transform.localPosition = nuevaPosicionDetector;
    }

    public void CambiarModoDiablo()
    {
        modoDiablo = true;
        p2.localScale = tamanio;
        moveSpeed = maxSpeed;
        spriteRenderer.color = Color.red; // Cambiar el color del sprite a rojo
    }

    IEnumerator ChocarJugador()
    {
        //colliderHijo.enabled = false;
        moveSpeed = 0;
        modoDiablo = false;
        spriteRenderer.color = Color.white; // Cambiar el color del sprite a normal
        yield return new WaitForSeconds(1f);
        p2.localScale = tamanioOr;
        moveSpeed = 0;
        yield return new WaitForSeconds(3f);
        //colliderHijo.enabled = true;
        moveSpeed = 5;
        vision.GetComponent<EspinoDetectorScript>().DejarDetectarJugador();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("puntoA"))
        {
            destino = pointB.transform;
            p2.localScale = tamanioOr;
            moveSpeed = 5;
            modoDiablo = false;
            spriteRenderer.color = Color.white; // Cambiar el color del sprite a normal
            vision.GetComponent<EspinoDetectorScript>().DejarDetectarJugador();
        }

        if (collision.gameObject.CompareTag("puntoB"))
        {
            destino = pointA.transform;
            p2.localScale = tamanioOr;
            moveSpeed = 5;
            modoDiablo = false;
            spriteRenderer.color = Color.white; // Cambiar el color del sprite a normal
            vision.GetComponent<EspinoDetectorScript>().DejarDetectarJugador();
        }


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (!mirandoDerecha)
            {
                direccionLanzamiento = -1;
            }
            else
            {
                direccionLanzamiento = 1;
            }
            if (!modoDiablo)
            {
                gameManager.QuitarVidaXEnemigo(danio);
                Vector2 launchForce = new Vector2(launchForceX * -direccionLanzamiento, launchForceY);
                playerRb.AddForce(launchForce, ForceMode2D.Impulse);
                collision.gameObject.GetComponent<Transform>().position = detectarLugarLanzamiento2.transform.position;
                collision.gameObject.GetComponent<PlayerLocal>().ChocarEspino();
                StartCoroutine(ChocarJugador());
            }
            else
            {
                gameManager.QuitarVidaXEnemigo(danio/2);
                Vector2 launchForce = new Vector2(launchForceX * direccionLanzamiento, launchForceY);
                playerRb.AddForce(launchForce, ForceMode2D.Impulse);
                collision.gameObject.GetComponent<Transform>().position = detectarLugarLanzamiento.transform.position;
                collision.gameObject.GetComponent<PlayerLocal>().ChocarEspino();
                StartCoroutine(ChocarJugador());
            }
        }
    }
}
