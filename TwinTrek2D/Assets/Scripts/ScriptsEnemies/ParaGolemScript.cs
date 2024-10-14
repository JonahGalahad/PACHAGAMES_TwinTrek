using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;

public class ParaGolemScript : MonoBehaviour
{
    [Header("Direccion y Movimiento")]
    //Establecen el punto donde debe dirigirse el enemigo
    public GameObject pointA; //destino A
    public GameObject pointB; //destino B
    public Transform destino; //Punto donde debe dirigirse
    private Transform destinoPrevio; //variable que guarda el anterior destino donde se dirigia el enemigo
    private bool enemigoDetectado = false;
    //Velocidad del enemigo
    public float speed;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidbody2;
    private Color colorOriginal;

    [Header("Hacer danio")]
    //Daño de ataque
    [SerializeField] private float danioAtaque;

    [Header("Vida enemigo")]
    //La vida del enemigo
    //[SerializeField] private float vida;
    public float vida;

    // Fuerza del salto cuando es golpeado
    [SerializeField] private float fuerzaSalto = 5f;

    //Variables para la animacion
    private Animator animator;
    private bool isCoroutineAtacarOn = false; //pregunta si la coroutine "Atacar" esta encendida

    void Start()
    {
        // Ignora la colisión entre compañeros de equipo
        Collider2D myCollider = GetComponent<Collider2D>();
        GameObject[] enemigos = GameObject.FindGameObjectsWithTag("TopoMalo");

        foreach (GameObject enemigo in enemigos)
        {
            if (enemigo != gameObject) // Asegurarte de no ignorar la colisión contigo mismo
            {
                Collider2D otherCollider = enemigo.GetComponent<Collider2D>();
                Physics2D.IgnoreCollision(myCollider, otherCollider);
            }
        }

        spriteRenderer = GetComponent<SpriteRenderer>();
        colorOriginal = spriteRenderer.color;
        destino = pointA.transform;

        rigidbody2 = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        IrDestino();
    }

    
    public void IrDestino()
    {
        transform.position = Vector2.MoveTowards(this.transform.position, destino.transform.position, speed * Time.deltaTime);
        spriteRenderer.flipX = transform.position.x > destino.position.x;
    }

    public void TomarDanio(float danio, Transform atacante)
    {
        StartCoroutine(QuitarVida(danio,atacante));
        // Calcular la dirección del golpe y aplicar la fuerza de rebote
        Vector2 direccionRebote = (transform.position - atacante.position).normalized; // Dirección opuesta al atacante
        rigidbody2.velocity = Vector2.zero; // Reiniciar la velocidad antes de aplicar la fuerza
        rigidbody2.AddForce(new Vector2(direccionRebote.x * fuerzaSalto, fuerzaSalto), ForceMode2D.Impulse);
    }

    IEnumerator QuitarVida(float danio, Transform atacante)
    {
        vida -= danio;
        spriteRenderer.color = Color.red;
        if (vida <= 0)
        {
            if (atacante.gameObject.CompareTag("Player") || atacante.gameObject.CompareTag("PuntoAtaque"))
            {
                yield return new WaitForSeconds(0.2f);
                //atacante.GetComponent<PlayerScript>().DesaparecerUIEnemigo();
            }
            //puntaje.SumarPuntos(cantidadPuntos);
            Destroy(gameObject);
        }
        else
        {
            if (atacante.gameObject.CompareTag("Player") || atacante.gameObject.CompareTag("PuntoAtaque"))
            {
                //atacante.GetComponent<PlayerScript>().AparecerUIEnemigo();
            }
            yield return new WaitForSeconds(0.5f);
            rigidbody2.velocity = Vector2.zero; // Reiniciar la velocidad antes de aplicar la fuerza
            spriteRenderer.color = colorOriginal;
        }
    }

    IEnumerator Atacar()
    {
        isCoroutineAtacarOn = true;
        animator.SetBool("atacando", true);
        yield return new WaitForSeconds(0.1f);
        animator.SetBool("atacando", false);
        isCoroutineAtacarOn = false;
    }

    public void CambiarTarget(Transform player)
    {
        if(!enemigoDetectado)
        {
            enemigoDetectado = true;
            destinoPrevio = destino;
            destino = player.transform;
        }
        else
        {
            enemigoDetectado = false;
            destino = destinoPrevio;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("PuntoA") && !enemigoDetectado)
        {
            //spriteRenderer.flipX = false;
            destino = pointB.transform;
        }
        if (collision.gameObject.CompareTag("PuntoB") && !enemigoDetectado)
        {
            destino = pointA.transform;
            //spriteRenderer.flipX = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(!isCoroutineAtacarOn)
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
                    // Colisión lateral: aplicar el daño
                    //collision.gameObject.GetComponent<PlayerScript>().QuitarVida(danioAtaque,transform);
                    StartCoroutine(Atacar());
                }
                else
                {
                    //collision.gameObject.GetComponent<PlayerScript>().QuitarVida(1f, transform);
                }
            }
        }
    }

}
