using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using Unity.VisualScripting;
using UnityEngine;
using FMOD.Studio;

public class PlayerLocal : MonoBehaviour
{
    //Variables para la animacion
    private Animator animator;
    private SpriteRenderer direccion;
    private int sortinOrderInicial;
    [SerializeField] private int sortinOrderFinal = -4;
    //--------------------------------------
    //Variables para el instanciacion y control de sonido FMOD
    //--------------------------------------
    [SerializeField] private EventReference walkSound;
    [SerializeField] private EventReference climbSound;
    //[SerializeField] private StudioEventEmitter jumpSound;
    private EventInstance walkEvent;
    private EventInstance climbEvent;
    //private EventInstance jumpEvent;
    private bool isWalk = false;
    private bool isClimb = false;
    //--------------------------------------
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
    //private GameObject partner; //Variable que sirven cuando el jugador reconozco a su compa�ero cuando es atrapado por la planta
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
    //private bool isJumping = false; //para verificar que si salto
    //-------------------------------------------

    private bool estaPausado = false; // Variable para rastrear el estado de pausa
    [SerializeField] private GameObject menuPausa;

    //[SerializeField] private GameObject menuPausa;

    //Variables para los odigos de trepar
    //-------------------------------------------
    private bool estaEnEnredadera = false;
    private bool estaEnParedLateral = false;
    [SerializeField] private float moveSpeedTrepar = 2f; //Velocidad con la que el player trepar�
    //-------------------------------------------
    private KeyCode BotonSalto;
    private KeyCode BotonAccion;
    // Agregada una variable para identificar el jugador
    private string playerVerticalAxis; // Asigna el nombre del eje vertical en el Inspector
    private string playerHorizontalAxis; // Asigna el nombre del eje horizontal en el Inspector

    [SerializeField] private int asignarJugador;

    [SerializeField] private float masaInicial;
    [SerializeField] private float masaFinal = 5f;

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
        sortinOrderInicial = direccion.sortingOrder;
        masaInicial = rigidbody2d.mass;
        walkEvent = RuntimeManager.CreateInstance(walkSound);
        climbEvent = RuntimeManager.CreateInstance(climbSound);
        //jumpEvent = RuntimeManager.CreateInstance(jumpSound);
        walkEvent.start();
        climbEvent.start();
        //jumpEvent.start();

    }

    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Return)) // Detecta la tecla "Enter"
        {
            // Cambia el estado de pausa
            estaPausado = !estaPausado;

            // Aplica la l�gica seg�n el estado de pausa
            if (estaPausado)
            {
                PausarJuego();
            }
            else
            {
                ReanudarJuego();
            }
        }*/

        if (atrapado || atrapadoPorGolem) //Pregunta si el jugador esta atrapado
        {
            return;
        }

        if (IsGrounded() && Input.GetKeyDown(BotonSalto)) //Si el jugador esta en el suelo, con space salta
        {
            //isJumping = true;
            rigidbody2d.velocity = Vector2.up * jumpVelocity; //realiza el salto
            StartCoroutine(CambiarMasa());
            /*if(!isJumping && IsGrounded()) {
                jumpSound.Play();
            }*/
            //rigidbody2d.mass = masaFinal;
        }
        HandleMovement();
        //si permanece en el suelo se reproduce el sonido
        if (!atrapado) {
            if (IsGrounded()) {
                updateWalkParameter(isWalk);
                isClimb = false;
            } else updateWalkParameter(false);
        }
        //Debug.Log(isWalk);

        updateClimbParameter(isClimb);

        MoverEnParedLateral();
        MoverEnEnredadera();

        if (zonaLiberar) //Mecanica de Atrapado de la FLOR
        {
            LiberarCompa();
        }
        //VerificarExisteJugador();
    }

    public void PausarJuego()
    {
        // L�gica para pausar el juego
        Time.timeScale = 0; // Detiene la simulaci�n del tiempo
        // Puedes mostrar un men� de pausa aqu� si lo deseas
        menuPausa.SetActive(true);
    }

    public void ReanudarJuego()
    {
        // L�gica para reanudar el juego
        Time.timeScale = 1; // Restaura la simulaci�n del tiempo
        // Puedes ocultar el men� de pausa aqu� si lo mostraste previamente
        menuPausa.SetActive(false);
    }

    private void SetRigidbodyToDynamic()
    {
        rigidbody2d.bodyType = RigidbodyType2D.Dynamic;
        rigidbody2d.interpolation = RigidbodyInterpolation2D.Interpolate;  // Movimiento m�s fluido
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
                // C�digo para la animaci�n de escalada
                //animator.SetBool("isClimbing", true);
                animator.SetFloat("Horizontal", 0); // Detiene animaci�n horizontal
            }
            else
            {
                // C�digo para la animaci�n de movimiento normal
                //animator.SetBool("isClimbing", false);
                animator.SetFloat("Horizontal", Mathf.Abs(moveInput));
            }
        }
        if (moveInput != 0) // Si se est� presionando A (-1) o D (+1)
        {
            if (IsGrounded()) // Pregunta si est� en el suelo
            {
                rigidbody2d.velocity = new Vector2(moveInput * moveSpeed, rigidbody2d.velocity.y);
                //si no esta atrapado el sfx se reproduce
                if (!atrapado) isWalk = true;
                else isWalk = false;
                
                ///SONIDO
                //rigidbody2d.mass = masaInicial;

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
                    isClimb = true;
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

            // Configura la direcci�n del sprite
            direccion.flipX = (moveInput < 0);
        }

        else
        {
            //si no apreta las teclas de movimiento, no se movera
            if (IsGrounded())
            {   
                rigidbody2d.velocity = new Vector2(0, rigidbody2d.velocity.y);
                isWalk=false;
                //rigidbody2d.mass = masaInicial;

                /*if(audioSource.isPlaying)
                {
                    audioSource.Stop();
                }*/

            }
            if (estaEnEnredadera)
            {
                // Dejar de Mover hacia los lados cuando esta en enredadera
                rigidbody2d.velocity = new Vector2(0, rigidbody2d.velocity.y);
                isClimb = false;
            }
        }
    }

    public void MoverEnParedLateral()
    {
        if (estaEnParedLateral)
        {
            float moveInput = Input.GetAxis(playerVerticalAxis); // Obtiene el valor del eje Vertical (-1 a 1)
            if (moveInput != 0) // Si se est� presionando S (-1) o W (+1)
            {
                // Mover hacia arriba
                rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, moveInput * moveSpeedTrepar);
                isClimb = true;

            }
            else
            {
                // Si no se presiona hacia arriba, dejar de moverse verticalmente
                rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, 0);
                isClimb = false;
            }
        }
    }

    public void MoverEnEnredadera()
    {
        if (estaEnEnredadera)
        {
            float moveInput = Input.GetAxis(playerVerticalAxis); // Obtiene el valor del eje Vertical (-1 a 1)
            if (moveInput < 0) // Si se est� presionando S (-1)
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
        direccion.sortingOrder = sortinOrderFinal;
        if(asignarJugador == 1)
        {
            Debug.Log("�El enemigo atrap� al jugador 1!");
        }
        else if (asignarJugador == 2)
        {
            Debug.Log("�El enemigo atrap� al jugador 2!");
        }
    }

    public void DejarEstarAtrapado() //Metodo para la mecanica de Atrapar de la FLOR
    {
        Debug.Log("Me ha liberado!");
        rigidbody2d.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
        rigidbody2d.constraints = RigidbodyConstraints2D.FreezeRotation;
        boxCollider2d.isTrigger = false;
        atrapado = false;
        direccion.sortingOrder = sortinOrderInicial;
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
        // Verificamos si la referencia sigue siendo v�lida
        if (partner != null)
        {
            // Si el objeto se ha destruido, Unity lo reconocer� como null
            if (partner == null)
            {
                Debug.Log("El objeto ha sido destruido.");
                partner = null; // Aseguramos que la variable se establezca en null
            }
        }
    }*/
    public void updateWalkParameter(bool isWalking) { walkEvent.setParameterByName("IsMove", isWalking ? 1f: 0f);}
    private void updateClimbParameter(bool isClimbing) { climbEvent.setParameterByName("IsClimb", isClimbing ? 1f:0f);}
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

    IEnumerator CambiarMasa()
    {
        rigidbody2d.mass = masaFinal;
        yield return new WaitForSeconds(0.1f);
        yield return new WaitUntil(()  => IsGrounded());
        yield return new WaitForSeconds(0.4f);
        rigidbody2d.mass = masaInicial;
        //isJumping = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enredadera"))
        {
            estaEnEnredadera = true;
            rigidbody2d.gravityScale = 0f; // Desactivar gravedad mientras est� en el techo
        }
        if (collision.gameObject.CompareTag("ParedLateral"))
        {
            estaEnParedLateral = true;
            rigidbody2d.gravityScale = 0f; // Desactivar gravedad mientras est� en el techo
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enredadera"))
        {
            estaEnEnredadera = false;
            rigidbody2d.gravityScale = 1f;// Restaurar la gravedad cuando sale del techo
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
            if (collision.GetComponent<FlorLocalScript>().JugadorYaAtrapado == true && gameObject != collision.GetComponent<FlorLocalScript>().JugadorAtrapado)
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
            if (collision.GetComponent<FlorLocalScript>().JugadorYaAtrapado == true && gameObject != collision.GetComponent<FlorLocalScript>().JugadorAtrapado)
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
                // Ajustar posici�n en ambos ejes
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
