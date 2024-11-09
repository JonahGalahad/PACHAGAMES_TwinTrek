using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class Player : NetworkBehaviour
{
    //Variables que sirven para moverse con la plataforma
    //--------------------------------------
    private float distanciaX;
    private float distanciaY;
    private bool sobrePlataforma;
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
    public float jumpVelocity = 5f; //para el alcance del salto
    public float moveSpeed = 5f; //para la velocidad de movimiento
    public float midAirControl = 3f; //controla el jugador en el aire, mientras mas valor tenga, el jugador podra controlar mejor su personaje en el aire
    //-------------------------------------------

    //private bool estaPausado = false; // Variable para rastrear el estado de pausa

    //[SerializeField] private GameObject menuPausa;

    //Variables para los odigos de trepar
    //-------------------------------------------
    public bool estaEnEnredadera = false;
    public bool estaEnParedLateral = false;
    public float moveSpeedTrepar = 2f; //Velocidad con la que el player trepará
    //-------------------------------------------

    public KeyCode BotonSalto;
    public KeyCode BotonAccion;
    // Agregada una variable para identificar el jugador
    public string playerVerticalAxis; // Asigna el nombre del eje vertical en el Inspector
    public string playerHorizontalAxis; // Asigna el nombre del eje horizontal en el Inspector

    public int asignarJugador;

    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        //rigidbody2d = transform.GetComponent<Rigidbody2D>();
        boxCollider2d = transform.GetComponent<BoxCollider2D>();
        //audioSource = GetComponent<AudioSource>(); //AGREGADO MAXI
    }

    public override void OnNetworkSpawn()
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
        // Solo el jugador local debería controlar su Rigidbody2D
        if (IsOwner)
        {
            SetRigidbodyToDynamic();
        }
    }

    /*private void Start()
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
    }*/

    void Update()
    {
        if (!IsOwner) { return; }

        // Chequea regularmente si es el jugador local y si el Rigidbody2D está en Dynamic
        if (IsOwner && rigidbody2d.bodyType == RigidbodyType2D.Kinematic)
        {
            SetRigidbodyToDynamic();
        }

        if (atrapado || atrapadoPorGolem) //Pregunta si el jugador esta atrapado
        {
            return;
        }

        if (IsGrounded() && Input.GetKeyDown(BotonSalto)) //Si el jugador esta en el suelo, con space salta
        {
            rigidbody2d.velocity = Vector2.up * jumpVelocity; //realiza el salto
            sobrePlataforma = false; //Variable que sirve para moverse con la plataforma
            //sobrePlataformaVertical = false;
        }
        /*if (IsGrounded2() && Input.GetKeyDown(KeyCode.Space)) //Si el jugador esta en el suelo, con space salta
        {
            rigidbody2d.velocity = Vector2.up * jumpVelocity; //realiza el salto
        }*/
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
        if (!IsOwner) { return; }
        float moveInput = Input.GetAxis(playerHorizontalAxis); // Obtiene el valor del eje Horizontal (-1 a 1)

        if (moveInput != 0) // Si se está presionando A (-1) o D (+1)
        {
            sobrePlataforma = false; //Variable que sirve para moverse con la plataforma
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
            //direccion.flipX = (moveInput < 0);
        }

        else
        {
            //si no apreta las teclas de movimiento, no se movera
            if (IsGrounded())
            {
                rigidbody2d.velocity = new Vector2(0, rigidbody2d.velocity.y);

                sobrePlataforma = true; //Variable que sirve para moverse con la plataforma

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
        if (!IsOwner) { return; }
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
        if (!IsOwner) { return; }
        if (estaEnEnredadera)
        {
            float moveInput = Input.GetAxis(playerVerticalAxis); // Obtiene el valor del eje Vertical (-1 a 1)
            if (moveInput < 0) // Si se está presionando S (-1)
            {
                rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, moveInput * moveSpeedTrepar);
            }
        }
    }

    /*public void Empujado() //AGREGADO MAXI
    {
        empujado = true;
        StartCoroutine(Empuje());
    }*/

    /*IEnumerator Empuje() //AGREGADO MAXI
    {
        yield return new WaitForSeconds(tiempoCongeladoPorEmpuje);
        empujado = false;

    }*/

    public void EstarAtrapado() //Metodo para la mecanica de Atrapar de la FLOR
    {
        atrapado = true;
        rigidbody2d.constraints = RigidbodyConstraints2D.FreezePositionY;
        boxCollider2d.isTrigger = true;
        this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        //this.gameObject.GetComponent<Vida>().juntos = false;
    }

    public void DejarEstarAtrapado() //Metodo para la mecanica de Atrapar de la FLOR
    {
        StartCoroutine(LiberarmeDeFlor());
        //Debug.Log("Me ha liberado!");
        //atrapado = false;
        //rigidbody2d.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
        //rigidbody2d.constraints = RigidbodyConstraints2D.FreezeRotation;
        //boxCollider2d.isTrigger = false;
    }

    IEnumerator LiberarmeDeFlor()
    {
        Debug.Log("Me ha liberado!");
        rigidbody2d.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
        rigidbody2d.constraints = RigidbodyConstraints2D.FreezeRotation;
        boxCollider2d.isTrigger = false;
        yield return new WaitForSeconds(0.1f);
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
        if (!IsOwner) { return; }
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
        /*if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.GetComponent<Player>().atrapado == true)  //Metodo para la mecanica de Atrapar de la FLOR
            {
                Debug.Log("Apreta 'E' para liberar");
                partner = collision.gameObject;
                zonaLiberar = true;
            }
        }*/
        if(collision.gameObject.CompareTag("FlorEnemy"))
        {
            if(collision.GetComponent<FlorScript>().jugadorYaAtrapado == true && gameObject != collision.GetComponent<FlorScript>().playerAtrapado)
            {
                zonaLiberar = true;
            }
        }

        if (collision.gameObject.CompareTag("Plataforma"))
        {
            // Calcular distancia inicial en X y Y
            distanciaX = transform.position.x - collision.transform.position.x;
            distanciaY = transform.position.y - collision.transform.position.y;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        /*if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.GetComponent<Player>().atrapado == true)  //Metodo para la mecanica de Atrapar de la FLOR
            {
                Debug.Log("Fuera de rango para liberar");
                partner = null;
                zonaLiberar = false;
            }
        }*/
        if (collision.gameObject.CompareTag("FlorEnemy"))
        {
            if (collision.GetComponent<FlorScript>().jugadorYaAtrapado == true && gameObject != collision.GetComponent<FlorScript>().playerAtrapado)
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
                collision.gameObject.GetComponent<FlorScript>().Liberar();
                paraLiberar = false;
            }
        }

        if (collision.gameObject.CompareTag("Plataforma"))
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
        }

        if(collision.gameObject.CompareTag("EspirituTierra"))
        {
            if(atrapado)
            {
                transform.position = new Vector2(
                    collision.transform.position.x,
                    collision.transform.position.y
                );
            }
        }
    }
}
