using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EspirituDeFuegoScript : MonoBehaviour
{
    [Header("Quitar vida")]
    private GameManager gameManager;
    [SerializeField] private float danio = 5f;
    [SerializeField] private float tiempo = 0f;
    [SerializeField] private float tiempoEntreRestas = 1f;
    [SerializeField] private bool quemar = false;
    [Header("Destino de Movimiento")]
    //Establecen el punto donde debe dirigirse el enemigo
    [SerializeField] private GameObject pointA; //destino A
    [SerializeField] private GameObject pointB; //destino B
    [SerializeField] private Transform destino; //Punto donde debe dirigirse
    [SerializeField] private float moveSpeed = 5f; //para la velocidad de movimiento
    [SerializeField] private LayerMask platformsLayerMask;
    private Rigidbody2D rigidbody2; //toma el rigidbody del mismo Espino
    private SpriteRenderer spriteRenderer; //toma el spriteRenderer para el flip

    [Header("Lanzamiento de Fuego")]
    [SerializeField] private Vector2 ultimaPosicion;
    [SerializeField] private float ajusteAltura = 0.1f; // Ajusta este valor en el Inspector de Unity o directamente aquí
    [SerializeField] private GameObject prefabFuego; // Prefab del sistema de partículas de fuego
    [SerializeField] private float tiempoDeEspera = 1.5f; // Tiempo entre cada llamarada
    [SerializeField] private float distancia = 0.5f; // Distancia detrás del espíritu donde aparecerá la llamarada
    [SerializeField] private float tiempoDeUltimaInstancia = 0f;

    private void Start()
    {
        // Encuentra el GameManager en la escena
        gameManager = FindObjectOfType<GameManager>();
        rigidbody2 = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        destino = pointA.transform;
    }

    private void Update()
    {
        IrDestino();
        tiempoDeUltimaInstancia += Time.deltaTime;
        if (tiempoDeUltimaInstancia >= tiempoDeEspera)
        {
            CreateFireTrail();
            tiempoDeUltimaInstancia = 0f;
        }
        if (quemar && (Time.time - tiempo) >= tiempoEntreRestas)
        {
            // Llama al método QuitarVidaEspino en el GameManager para restar vida
            gameManager.QuitarVidaXEnemigo(danio);
            Debug.Log("auch");
            tiempo = Time.time;
        }
    }

    public void IrDestino() //Metodo para dirigirse hacia su destino
    {
        Vector2 targetPosition = new Vector2(destino.transform.position.x, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        spriteRenderer.flipX = transform.position.x < destino.position.x;
        if(transform.position.x < destino.position.x)
        {
            ultimaPosicion = Vector2.left;
        }
        else
        {
            ultimaPosicion = Vector2.right;
        }
    }

    void CreateFireTrail()
    {
        // La posición inicial de la llamarada será detrás del espíritu, según la última dirección de movimiento
        Vector3 posicionFuego = transform.position - (Vector3)ultimaPosicion * distancia;

        // Raycast hacia abajo para detectar el suelo
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, platformsLayerMask);

        if (hit.collider != null)
        {
            // Si el Raycast detecta el suelo, ajusta la posición 'y' de la llamarada al punto de impacto
            posicionFuego.y = hit.point.y + ajusteAltura; // Sube un poco la posición de la llamarada
        }

        // Instancia el prefab de fuego en la posición ajustada
        GameObject instanciarFuego = Instantiate(prefabFuego, posicionFuego, Quaternion.identity);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("puntoA"))
        {
            destino = pointB.transform;
        }

        if (collision.gameObject.CompareTag("puntoB"))
        {
            destino = pointA.transform;
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            quemar = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            quemar = false;
        }
    }
}
