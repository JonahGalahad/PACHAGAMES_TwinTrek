using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Player1 : MonoBehaviour
{
    //Variables que sirven para moverse con la plataforma
    //--------------------------------------
    private float distancia;
    private bool sobrePlataforma;
    //--------------------------------------

    //Variables que sirven para la mecanica de quedar atrapado de la Flor
    //--------------------------------------
    public bool atrapado = false; //variable de si estas atrapado o no
    [SerializeField] private bool zonaLiberar = false;
    [SerializeField] private bool paraLiberar = false;
    private GameObject sam; //Variable que sirven cuando el jugador reconozco a su compañero cuando es atrapado por la planta
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

    //Agregada una variable para identificar el jugador
    //public string playerVerticalAxis; // Asigna el nombre del eje vertical en el Inspector
    //public string playerHorizontalAxis; // Asigna el nombre del eje horizontal en el Inspector

    private void Awake()
    {
        rigidbody2d = transform.GetComponent<Rigidbody2D>();
        boxCollider2d = transform.GetComponent<BoxCollider2D>();
        //audioSource = GetComponent<AudioSource>(); //AGREGADO MAXI
    }

    void Update()
    {

        if (atrapado) //Pregunta si el jugador esta atrapado
        {
            return;
        }

        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space)) //Si el jugador esta en el suelo, con space salta
        {
            rigidbody2d.velocity = Vector2.up * jumpVelocity; //realiza el salto
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
        VerificarExisteJugador();
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
    private bool IsGrounded()
    {
        //Permite que el objeto conozca el suelo, en este caso esta como playermask que seria "piso" Luego le devuleve un valor
        RaycastHit2D raycastHit2d = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down, 0.1f, platformsLayerMask);
        return raycastHit2d.collider != null;

    }
    /*private bool IsGrounded2()
    {
        //Permite que el objeto conozca el suelo, en este caso esta como playermask que seria "piso" Luego le devuleve un valor
        RaycastHit2D raycastHit2d2 = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down, 0.1f, platformsLayerMask2);
        return raycastHit2d2.collider != null;

    }*/

    public void HandleMovement()
    {

        if (Input.GetKey(KeyCode.A)) //Al presionar la letra A
        {
            if (IsGrounded()) //pregunta si esta en el suelo, y si lo esta se movera
            {
                rigidbody2d.velocity = new Vector2(-moveSpeed, rigidbody2d.velocity.y);

                sobrePlataforma = false; //Variable que sirve para moverse con la plataforma

                /*if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                }*/
            }
            else //si no esta en el suelo, puede moverse igual pero tendra un retraso en el aire
            {
                if(estaEnEnredadera)
                {
                    // Mover hacia los lados cuando esta en enredadera
                    rigidbody2d.velocity = new Vector2(-moveSpeedTrepar, rigidbody2d.velocity.y);
                }
                else
                {
                    rigidbody2d.velocity += new Vector2(-moveSpeed * midAirControl * Time.deltaTime, 0);
                    rigidbody2d.velocity = new Vector2(Mathf.Clamp(rigidbody2d.velocity.x, -moveSpeed, +moveSpeed), rigidbody2d.velocity.y);
                }
                
                /*if (audioSource.isPlaying)
                {
                    audioSource.Stop();
                }*/
            }
            
        }
        else
        {
            if (Input.GetKey(KeyCode.D)) //Al presionar la letra D
            {
                if (IsGrounded()) //pregunta si esta en el suelo, y si lo esta se movera
                {
                    rigidbody2d.velocity = new Vector2(+moveSpeed, rigidbody2d.velocity.y);

                    sobrePlataforma = false; //Variable que sirve para moverse con la plataforma

                    /*if (!audioSource.isPlaying)
                    {
                        audioSource.Play();
                    }*/
                }
                else //si no esta en el suelo, puede moverse igual pero tendra un retraso en el aire
                {
                    if (estaEnEnredadera)
                    {
                        // Mover hacia los lados cuando esta en enredadera
                        rigidbody2d.velocity = new Vector2(+moveSpeedTrepar, rigidbody2d.velocity.y);
                    }
                    else
                    {
                        rigidbody2d.velocity += new Vector2(+moveSpeed * midAirControl * Time.deltaTime, 0);
                        rigidbody2d.velocity = new Vector2(Mathf.Clamp(rigidbody2d.velocity.x, -moveSpeed, +moveSpeed), rigidbody2d.velocity.y);
                    }
                    /*if (audioSource.isPlaying)
                        {
                            audioSource.Stop();
                        }*/
                }
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
    }

    public void MoverEnParedLateral()
    {
        if (estaEnParedLateral)
        {
            if (Input.GetKey(KeyCode.W))
            {
                // Mover hacia arriba
                rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, +moveSpeedTrepar);
            }
            else if(Input.GetKey(KeyCode.S))
            {
                rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, -moveSpeedTrepar);
            }
            else
            {
                // Si no se presiona hacia arriba, dejar de moverse verticalmente
                rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, 0);

                
                /*rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, 0);
                if (estaEnEnredadera)
                {
                    if (Input.GetKey(KeyCode.S))
                    {
                        rigidbody2d.gravityScale = 1f;
                        estaEnEnredadera = false;
                    }
                }*/
            }
        }
    }

    public void MoverEnEnredadera()
    {
        if(estaEnEnredadera)
        {
            if (Input.GetKey(KeyCode.S))
            {
                rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, -moveSpeedTrepar);
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
        this.gameObject.GetComponent<Vida>().juntos = false;
    }

    public void DejarEstarAtrapado() //Metodo para la mecanica de Atrapar de la FLOR
    {
        atrapado = false;
        rigidbody2d.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
        rigidbody2d.constraints = RigidbodyConstraints2D.FreezeRotation;
        boxCollider2d.isTrigger = false;
    }

    public void LiberarCompa() //Metodo para la mecanica de Atrapar de la FLOR
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            sam.gameObject.GetComponent<Player2>().DejarEstarAtrapado();
            zonaLiberar = false;
            paraLiberar = true;
            //imagenLiberar2.SetActive(false);
        }
    }

    public void VerificarExisteJugador() //metodo que verifica si el objeto sigue en el juego  //Metodo para la mecanica de Atrapar de la FLOR
    {
        // Verificamos si la referencia sigue siendo válida
        if (sam != null)
        {
            // Si el objeto se ha destruido, Unity lo reconocerá como null
            if (sam == null)
            {
                Debug.Log("El objeto ha sido destruido.");
                sam = null; // Aseguramos que la variable se establezca en null
            }
        }
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

        if (collision.collider.CompareTag("Plataforma")) //Mecanica para moverse con la plataforma
        {
            distancia = transform.position.x - collision.transform.position.x;
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

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Plataforma")) //Mecanica para poder moverse con la plataforma
        {
            if(sobrePlataforma)
            {
                transform.position = new Vector2(collision.transform.position.x + distancia, transform.position.y);
            }
            else
            {
                distancia = transform.position.x - collision.transform.position.x;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Max"))
        {
            if (collision.gameObject.GetComponent<Player2>().atrapado == true)  //Metodo para la mecanica de Atrapar de la FLOR
            {
                Debug.Log("Apreta 'E' para liberar");
                sam = collision.gameObject;
                zonaLiberar = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Max"))
        {
            if (collision.gameObject.GetComponent<Player2>().atrapado == true)  //Metodo para la mecanica de Atrapar de la FLOR
            {
                Debug.Log("Fuera de rango para liberar");
                sam = null;
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
    }
}
