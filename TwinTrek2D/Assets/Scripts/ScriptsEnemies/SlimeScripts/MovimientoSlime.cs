using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class MovimientoSlime : MonoBehaviour
{
    [Header("Destino de Movimiento")]
    [SerializeField] private GameObject pointA; // Destino A
    [SerializeField] private GameObject pointB; // Destino B
    [SerializeField] private Transform destino; // Punto donde debe dirigirse

    [Header("Parámetros de Movimiento")]
    [SerializeField] private float velocidadInicial = 1f; // Velocidad inicial del desplazamiento
    [SerializeField] private float velocidadMaxima = 2.5f; // Velocidad máxima del desplazamiento
    [SerializeField] private float distanciaPaso = 1f; // Distancia por cada paso
    [SerializeField] private float tiempoPausa = 1f; // Tiempo de pausa entre pasos
    [SerializeField] private int pasosParaSaltar = 3; // Cantidad de pasos antes de hacer un salto
    [SerializeField] private float fuerzaSaltoVertical = 7f; // Fuerza del salto vertical
    [SerializeField] private float fuerzaSaltoHorizontal = 2f; // Fuerza del salto horizontal
    [SerializeField] private float tiempoPausaPostSalto = 1f; // Tiempo de pausa después del salto
    [SerializeField] private float factorReduccionVelocidad = 0.5f; // Factor de reducción para la velocidad

    private bool mirandoDerecha = false; // El Slime está inicialmente mirando a la izquierda
    private bool debeCambiarDestino = false; // Indica si debe cambiar de destino después del paso actual
    private int contadorDePasos = 0; // Cuenta los pasos dados
    private Rigidbody2D rb2D; // Referencia al Rigidbody2D del Slime
    private SpriteRenderer spriteRenderer; // Referencia al SpriteRenderer del Slime
    private bool puedeMoverse = true; // Controla cuándo el Slime puede moverse
    private bool estaEnSuelo = true; // Verifica si el Slime está en el suelo
    private Transform groundCheck; // Para verificar si el Slime está en el suelo
    [SerializeField] private LayerMask groundLayer; // Capa del suelo
    private DejarBaba dejarBaba; // Referencia al script DejarBaba

    private void Start()
    {
        // Extrae datos del Slime
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb2D = GetComponent<Rigidbody2D>();
        dejarBaba = GetComponent<DejarBaba>();

        // Inicializa el destino como pointA
        destino = pointA.transform;

        // Inicializa groundCheck
        groundCheck = transform.Find("GroundCheck");
    }

    private void Update()
    {
        // Verificar si el Slime está en contacto con el suelo
        estaEnSuelo = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

        if (puedeMoverse)
        {
            StartCoroutine(Mover());
        }
    }

    private IEnumerator Mover()
    {
        puedeMoverse = false;

        // Instancia la baba antes de moverse si está en el suelo
        if (estaEnSuelo)
        {
            dejarBaba.DejarSlime();
        }

        // Calcula la dirección del paso
        Vector3 direccion = (destino.position - transform.position).normalized;
        Vector3 paso = direccion * distanciaPaso;

        // Dinámica de aceleración suave
        float velocidad = velocidadInicial;
        while (velocidad < velocidadMaxima)
        {
            velocidad += (velocidadMaxima - velocidadInicial) * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, transform.position + paso, velocidad * Time.deltaTime);
            yield return null;
        }

        // Cambia la dirección del Slime si es necesario y está en el suelo
        if (debeCambiarDestino && estaEnSuelo)
        {
            CambiarDestino();
            CambiarDireccion();
            debeCambiarDestino = false;
        }

        // Incrementa el contador de pasos
        contadorDePasos++;

        // Si ha dado los pasos necesarios, realiza un salto
        if (contadorDePasos >= pasosParaSaltar)
        {
            // Instancia la baba antes de saltar si está en el suelo
            if (estaEnSuelo)
            {
                dejarBaba.DejarSlime();
            }
            StartCoroutine(Saltar());
            contadorDePasos = 0; // Reinicia el contador de pasos
        }

        // Pausa entre pasos para hacer visible el movimiento
        yield return new WaitForSeconds(tiempoPausa);

        puedeMoverse = true;
    }

    private IEnumerator Saltar()
    {
        // Aplica la fuerza de salto
        float direccionHorizontal = mirandoDerecha ? 1 : -1;
        Vector2 fuerzaSalto = new Vector2(fuerzaSaltoHorizontal * direccionHorizontal, fuerzaSaltoVertical);
        rb2D.AddForce(fuerzaSalto, ForceMode2D.Impulse);

        // Espera a que el Slime aterrice
        while (!estaEnSuelo)
        {
            yield return null;
        }

        // Asegura que el Slime cambia de dirección al aterrizar si debe hacerlo
        if (debeCambiarDestino)
        {
            CambiarDestino();
            CambiarDireccion();
            debeCambiarDestino = false;
        }

        // Pausa después del salto
        yield return new WaitForSeconds(tiempoPausaPostSalto);
    }

    private void CambiarDestino()
    {
        // Verifica si el destino es el punto A o B y cambia al opuesto
        if (destino == pointA.transform)
        {
            destino = pointB.transform;
        }
        else if (destino == pointB.transform)
        {
            destino = pointA.transform;
        }
    }

    private void CambiarDireccion()
    {
        mirandoDerecha = !mirandoDerecha;
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verificar colisión con los puntos A y B para cambiar de destino
        if ((collision.gameObject == pointA && destino == pointA.transform) || (collision.gameObject == pointB && destino == pointB.transform))
        {
            debeCambiarDestino = true;
        }

        // Reducir la velocidad del jugador si colisiona con el Slime
        if (collision.CompareTag("Player"))
        {
            ReductorMovimientoDeJugador reductorMovimientoDeJugador = collision.GetComponent<ReductorMovimientoDeJugador>();
            
            if (reductorMovimientoDeJugador != null)
            {
                reductorMovimientoDeJugador.ReducirVelocidad(factorReduccionVelocidad);
            }
        }

        // Detectar colisión con el suelo o un bloque y ajustar la posición del Slime
        if (collision.gameObject.layer == LayerMask.NameToLayer("Piso") || collision.CompareTag("Bloque"))
        {
            if (rb2D != null)
            {
                // Asegurar que el Slime esté en el suelo o bloque
                rb2D.bodyType = RigidbodyType2D.Kinematic;
                rb2D.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Restaurar la velocidad del jugador al salir del Slime
        if (collision.CompareTag("Player"))
        {
            ReductorMovimientoDeJugador reductorMovimientoDeJugador = collision.GetComponent<ReductorMovimientoDeJugador>();
            if (reductorMovimientoDeJugador != null)
            {
                reductorMovimientoDeJugador.RestaurarVelocidad();
            }
        }
    }
}
