using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using FMODUnity;
using FMOD.Studio;

public class AbejaV2 : MonoBehaviour
{
    [SerializeField] private GameObject[] players;
    [SerializeField] private Transform player1;
    [SerializeField] private Transform player2;
    private SpriteRenderer spriteRenderer; //toma el spriteRenderer para el flip

    [Header("Destino de Movimiento y velocidad")]
    //Establecen el punto donde debe dirigirse el enemigo
    [SerializeField] private GameObject pointA; //destino A
    [SerializeField] private GameObject pointB; //destino B
    [SerializeField] private Transform destino; //Punto donde debe dirigirse
    [SerializeField] private float speed;
    [SerializeField] private float longitud = 1f;

    [Header("Posicion de Disparo y Prefab aguijon")]
    [SerializeField] private Transform shootPosition;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed = 20f;
    [SerializeField] private float minDistance = 5f;
    [SerializeField] private float maxDistance = 30f;
    private bool playerInRange;
    private bool player2InRange;
    private float angle = 0.0f;

    //Variables para el instanciacion y control de sonido FMOD
    [SerializeField] private StudioEventEmitter abejaShotSound;

    public StudioEventEmitter AbejaShotSound { get { return abejaShotSound; } set { abejaShotSound = value; } }
    //[SerializeField] private EventReferece abejaFireSound;
    //private EventInstance abejaFireEvent;

    void Start()
    {
        StartCoroutine(EsperarJugadores());
        //establece el primer destino
        destino = pointA.transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
        InvokeRepeating("Shoot", 0.5f, 2f);
        //UpdatePlayers();
        //Sonido
        //abejaFireEvent = RuntimeManager.CreateInstance(abejaFireSound);
        //abejaFireEvent.start();
        
    }

    void Update()
    {
        IrDestino();
        ActualizarDistanciaJugador();
    }



    IEnumerator EsperarJugadores()
    {
        // Espera hasta que se encuentren ambos jugadores.
        while (players.Length < 2)
        {
            players = GameObject.FindGameObjectsWithTag("Player");
            yield return null;
        }
        player1 = players[0].transform;
        player2 = players[1].transform;
    }

    private void ActualizarDistanciaJugador()
    {
        // Actualizar el estado de cada jugador dentro del rango
        playerInRange = player1 != null && Vector2.Distance(player1.position, transform.position) < maxDistance && Vector2.Distance(player1.position, transform.position) > minDistance;
        player2InRange = player2 != null && Vector2.Distance(player2.position, transform.position) < maxDistance && Vector2.Distance(player2.position, transform.position) > minDistance;
    }
    private void IrDestino() //Metodo para dirigirse hacia su destino
    {
        Vector2 targetPosition = new Vector2(destino.transform.position.x, transform.position.y);
        //Vector2 targetPosition = new Vector2(destino.transform.position.x, Mathf.Sin(angle) * Time.deltaTime * longitud);
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        // Movimiento en el eje X con oscilaci�n
        transform.Translate(0f, Mathf.Sin(angle) * Time.deltaTime * longitud, 0f);
        angle += 0.01f;

        spriteRenderer.flipX = transform.position.x < destino.position.x;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("puntoA"))
        {
            destino = pointB.transform;
            Debug.Log("Faaaack");
        }
        if (collision.gameObject.CompareTag("puntoB"))
        {
            destino = pointA.transform;
            Debug.Log("Faaaack");
        }
    }

    private void Shoot()
    {
        // Verificar si al menos un jugador est� en rango
        if (playerInRange || player2InRange)
        {
            // Crear la bala
            GameObject bullet = Instantiate(bulletPrefab, shootPosition.position, Quaternion.identity);
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            abejaShotSound.Play();

            // Elegir aleatoriamente entre el jugador 1 y el jugador 2 si ambos est�n en rango, o disparar al �nico en rango
            Transform targetPlayer;
            if (playerInRange && player2InRange)
            {
                targetPlayer = (Random.Range(0, 2) == 0) ? player1 : player2;
            }
            else if (playerInRange)
            {
                targetPlayer = player1;
            }
            else
            {
                targetPlayer = player2;
            }

            // Calcular la direcci�n hacia el jugador elegido
            Vector2 direction = (targetPlayer.position - transform.position).normalized;

            // Establecer la velocidad de la bala en direcci�n al jugador elegido
            bulletRb.velocity = direction * bulletSpeed;

            // Calcular el �ngulo entre la bala y el jugador para rotarla en la direcci�n correcta
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.Euler(0, 0, angle - 180); // Ajusta la rotaci�n seg�n el sprite
        }
    }
    /*private void updateShotParameter(bool isShoting) {
        abejaFireEvent.setParameterByName("IsShot", isShoting ? 1f:0f);
    }*/
}