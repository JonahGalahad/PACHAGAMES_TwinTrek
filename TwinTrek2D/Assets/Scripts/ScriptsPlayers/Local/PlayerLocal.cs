using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocal : MonoBehaviour
{
    //Variables para la animacion
    private Animator animator;
    private SpriteRenderer direccion;
    //Variables que sirven para moverse con la plataforma
    //--------------------------------------
    //private float distanciaX;
    //private float distanciaY;
    //private bool sobrePlataforma;
    //--------------------------------------
    //Variables que sirven para la mecanica de quedar atrapado de la Flor
    //--------------------------------------
    public bool atrapado = false; //variable de si estas atrapado o no
    [SerializeField] private bool atrapadoPorGolem = false;
    [SerializeField] private bool zonaLiberar = false;
    [SerializeField] private bool paraLiberar = false;
    //private GameObject partner; //Variable que sirven cuando el jugador reconozco a su compañero cuando es atrapado por la planta
    //--------------------------------------
    //public bool empujado = false; //AGREGADO MAXI
    //public float tiempoCongeladoPorEmpuje = 1f; //AGREGADO MAXI

    //private AudioSource audioSource; //AGREGADO MAXI

    //Variables para el movimiento del Jugador
    //-------------------------------------------
    [SerializeField] private LayerMask platformsLayerMask; //toma el layerMask que seria el piso para que el jugador pueda saltar
    //[SerializeField] private LayerMask platformsLayerMask2;  //toma el layerMask que seria el piso para que el jugador pueda saltar
    private Rigidbody2D rigidbody2d; //toma el rigidbody del mismo jugador
    private BoxCollider2D boxCollider2d; //toma el box collider del mismo jugador
    [SerializeField] private float jumpVelocity = 5f; //para el alcance del salto
    [SerializeField] private float moveSpeed = 5f; //para la velocidad de movimiento
    [SerializeField] private float midAirControl = 3f; //controla el jugador en el aire, mientras mas valor tenga, el jugador podra controlar mejor su personaje en el aire
    //-------------------------------------------

    //private bool estaPausado = false; // Variable para rastrear el estado de pausa

    //[SerializeField] private GameObject menuPausa;

    //Variables para los odigos de trepar
    //-------------------------------------------
    private bool estaEnEnredadera = false;
    private bool estaEnParedLateral = false;
    [SerializeField] private float moveSpeedTrepar = 2f; //Velocidad con la que el player trepará
    //-------------------------------------------
    private KeyCode BotonSalto;
    private KeyCode BotonAccion;
    // Agregada una variable para identificar el jugador
    private string playerVerticalAxis; // Asigna el nombre del eje vertical en el Inspector
    private string playerHorizontalAxis; // Asigna el nombre del eje horizontal en el Inspector

    [SerializeField] private int asignarJugador;

    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        boxCollider2d = GetComponent<BoxCollider2D>();
        direccion = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        //audioSource = GetComponent<AudioSource>(); //AGREGADO MAXI
    }

    private void Start()
    {
        if (asignarJugador == 1)
        {
            playerVerticalAxis = "Vertical1";
            playerHorizontalAxis = "Horizontal1";
            BotonSalto = KeyCode.Space;
            BotonAccion = KeyCode.E;
        }
        if (asignarJugador == 2)
        {
            playerVerticalAxis = "Vertical2";
            playerHorizontalAxis = "Horizontal2";
            BotonSalto = KeyCode.UpArrow;
            BotonAccion = KeyCode.RightControl;
        }
    }

    void Update()
    {
        if (atrapado || atrapadoPorGolem) //Pregunta si el jugador esta atrapado
        {
            return;
        }

        if (IsGrounded() && Input.GetKeyDown(BotonSalto)) //Si el jugador esta en el suelo, con space salta
        {
            rigidbody2d.velocity = Vector2.up * jumpVelocity; //realiza el salto
        }
        HandleMovement();
        MoverEnParedLateral();
        MoverEnEnredadera();

        if (zonaLiberar) //Mecanica de Atrapado de la FLOR
        {
            LiberarCompa();
        }
        //VerificarExisteJugador();
    }

    /*public void PausarJuego()
    {
        // Lógica para pausar el juego
        Time.timeScale = 0; // Detiene la simulación del tiempo
        // Puedes mostrar un menú de pausa aquí si lo deseas
        menuPausa.SetActive(true);
    }

    public void ReanudarJuego()
    {
        // Lógica para reanudar el juego
        Time.timeScale = 1; // Restaura la simulación del tiempo
        // Puedes ocultar el menú de pausa aquí si lo mostraste previamente
        menuPausa.SetActive(false);
    }*/

    private void SetRigidbodyToDynamic()
    {
        rigidbody2d.bodyType = RigidbodyType2D.Dynamic;
        rigidbody2d.interpolation = RigidbodyInterpolation2D.Interpolate;  // Movimiento más fluido
        rigidbody2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;  // Evita problemas de colisiones
    }

    private bool IsGrounded()
    {
        //Permite que el objeto conozca el suelo, en este caso esta como playermask que seria "piso" Luego le devuleve un valor
        RaycastHit2D raycastHit2d = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down, 0.1f, platformsLayerMask);
        return raycastHit2d.collider != null;

    }

    public void HandleMovement()
    {
        float moveInput = Input.GetAxis(playerHorizontalAxis); // Obtiene el valor del eje Horizontal (-1 a 1)

        if(animator != null)
        {
            animator.SetFloat("Horizontal", Mathf.Abs(moveInput)); //para la animacion
        }

        if (animator != null)
        {
            if (estaEnEnredadera || estaEnParedLateral)
            {
                // Código para la animación de escalada
                //animator.SetBool("isClimbing", true);
                animator.SetFloat("Horizontal", 0); // Detiene animación horizontal
            }
            else
            {
                // Código para la animación de movimiento normal
                //animator.SetBool("isClimbing", false);
                animator.SetFloat("Horizontal", Mathf.Abs(moveInput));
            }
        }
        if (moveInput != 0) // Si se está presionando A (-1) o D (+1)
        {
            if (IsGrounded()) // Pregunta si está en el suelo
            {
                rigidbody2d.velocity = new Vector2(moveInput * moveSpeed, rigidbody2d.velocity.y);

                /*if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                }*/
            }
            else
            {
                if (estaEnEnredadera)
                {
                    // Mover hacia los lados cuando esta en enredadera
                    rigidbody2d.velocity = new Vector2(moveInput * moveSpeedTrepar, rigidbody2d.velocity.y);
                }
                else
                {
                    rigidbody2d.velocity += new Vector2(moveInput * moveSpeed * midAirControl * Time.deltaTime, 0);
                    rigidbody2d.velocity = new Vector2(Mathf.Clamp(rigidbody2d.velocity.x, -moveSpeed, +moveSpeed), rigidbody2d.velocity.y);
                }

                /*if (audioSource.isPlaying)
                {
                    audioSource.Stop();
                }*/
            }

            // Configura la dirección del sprite
            direccion.flipX = (moveInput < 0);
        }

        else
        {
            //si no apreta las teclas de movimiento, no se movera
            if (IsGrounded())
            {
                rigidbody2d.velocity = new Vector2(0, rigidbody2d.velocity.y);

                /*if(audioSource.isPlaying)
                {
                    audioSource.Stop();
                }*/

            }
            if (estaEnEnredadera)
            {
                // Dejar de Mover hacia los lados cuando esta en enredadera
                rigidbody2d.velocity = new Vector2(0, rigidbody2d.velocity.y);
            }
        }
    }

    public void MoverEnParedLateral()
    {
        if (estaEnParedLateral)
        {
            float moveInput = Input.GetAxis(playerVerticalAxis); // Obtiene el valor del eje Vertical (-1 a 1)
            if (moveInput != 0) // Si se está presionando S (-1) o W (+1)
            {
                // Mover hacia arriba
                rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, moveInput * moveSpeedTrepar);
            }
            else
            {
                // Si no se presiona hacia arriba, dejar de moverse verticalmente
                rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, 0);
            }
        }
    }

    public void MoverEnEnredadera()
    {
        if (estaEnEnredadera)
        {
            float moveInput = Input.GetAxis(playerVerticalAxis); // Obtiene el valor del eje Vertical (-1 a 1)
            if (moveInput < 0) // Si se está presionando S (-1)
            {
                rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, moveInput * moveSpeedTrepar);
            }
        }
    }

    public void EstarAtrapado() //Metodo para la mecanica de Atrapar de la FLOR
    {
        atrapado = true;
        rigidbody2d.constraints = RigidbodyConstraints2D.FreezePositionY;
        boxCollider2d.isTrigger = true;
        this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        if(asignarJugador == 1)
        {
            Debug.Log("¡El enemigo atrapó al jugador 1!");
        }
        else if (asignarJugador == 2)
        {
            Debug.Log("¡El enemigo atrapó al jugador 2!");
        }
    }

    public void DejarEstarAtrapado() //Metodo para la mecanica de Atrapar de la FLOR
    {
        Debug.Log("Me ha liberado!");
        rigidbody2d.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
        rigidbody2d.constraints = RigidbodyConstraints2D.FreezeRotation;
        boxCollider2d.isTrigger = false;
        atrapado = false;
    }

    public void EstarAtrapadoPorGolem() //Metodo para la mecanica de Atrapar del Golem
    {
        atrapadoPorGolem = true;
        rigidbody2d.constraints = RigidbodyConstraints2D.FreezePositionY;
        boxCollider2d.isTrigger = true;
        this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    public void DejarEstarAtrapadoPorGolem() //Metodo para la mecanica de Atrapar de la FLOR
    {
        StartCoroutine(LiberarmeGolem());
    }

    IEnumerator LiberarmeGolem()
    {
        rigidbody2d.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
        rigidbody2d.constraints = RigidbodyConstraints2D.FreezeRotation;
        boxCollider2d.isTrigger = false;
        yield return new WaitForSeconds(1f);
        atrapadoPorGolem = false;
    }

    public void LiberarCompa() //Metodo para la mecanica de Atrapar de la FLOR
    {
        if (Input.GetKeyDown(BotonAccion))
        {
            //partner.gameObject.GetComponent<Player>().DejarEstarAtrapado();
            zonaLiberar = false;
            paraLiberar = true;
            //imagenLiberar2.SetActive(false);
        }
    }

    /*public void VerificarExisteJugador() //metodo que verifica si el objeto sigue en el juego  //Metodo para la mecanica de Atrapar de la FLOR
    {
        // Verificamos si la referencia sigue siendo válida
        if (partner != null)
        {
            // Si el objeto se ha destruido, Unity lo reconocerá como null
            if (partner == null)
            {
                Debug.Log("El objeto ha sido destruido.");
                partner = null; // Aseguramos que la variable se establezca en null
            }
        }
    }*/

    public void ChocarEspino()
    {
        StartCoroutine(Moverse());
    }

    IEnumerator Moverse()
    {
        atrapado = true;
        yield return new WaitForSeconds(2f);
        atrapado = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enredadera"))
        {
            estaEnEnredadera = true;
            rigidbody2d.gravityScale = 0f; // Desactivar gravedad mientras está en el techo
        }
        if (collision.gameObject.CompareTag("ParedLateral"))
        {
            estaEnParedLateral = true;
            rigidbody2d.gravityScale = 0f; // Desactivar gravedad mientras está en el techo
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enredadera"))
        {
            estaEnEnredadera = false;
            rigidbody2d.gravityScale = 1f; // Restaurar la gravedad cuando sale del techo
        }
        if (collision.gameObject.CompareTag("ParedLateral"))
        {
            estaEnParedLateral = false;
            rigidbody2d.gravityScale = 1f; // Restaurar la gravedad cuando sale del techo
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("FlorEnemy"))
        {
            if (collision.GetComponent<FlorLocalScript>().jugadorYaAtrapado == true && gameObject != collision.GetComponent<FlorLocalScript>().playerAtrapado)
            {
                zonaLiberar = true;
            }
        }

        /*if (collision.gameObject.CompareTag("Plataforma"))
        {
            // Calcular distancia inicial en X y Y
            distanciaX = transform.position.x - collision.transform.position.x;
            distanciaY = transform.position.y - collision.transform.position.y;
        }*/

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("FlorEnemy"))
        {
            if (collision.GetComponent<FlorLocalScript>().jugadorYaAtrapado == true && gameObject != collision.GetComponent<FlorLocalScript>().playerAtrapado)
            {
                zonaLiberar = false;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("FlorEnemy"))
        {
            if (paraLiberar)  //Metodo para la mecanica de Atrapar de la FLOR
            {
                collision.gameObject.GetComponent<FlorLocalScript>().Liberar();
                paraLiberar = false;
            }
        }

        /*if (collision.gameObject.CompareTag("Plataforma"))
        {
            if (sobrePlataforma)
            {
                // Ajustar posición en ambos ejes
                transform.position = new Vector2(
                    collision.transform.position.x + distanciaX,
                    collision.transform.position.y + distanciaY
                );
            }
            else
            {
                // Calcular distancia inicial en X y Y
                distanciaX = transform.position.x - collision.transform.position.x;
                distanciaY = transform.position.y - collision.transform.position.y;
            }
        }*/

        /*if (collision.gameObject.CompareTag("EspirituTierra"))
        {
            if (atrapado)
            {
                transform.position = new Vector2(
                    collision.transform.position.x,
                    collision.transform.position.y
                );
            }
        }*/
    }
}
