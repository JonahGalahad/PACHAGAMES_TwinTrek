using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1_mov : MonoBehaviour
{
    public bool atrapado = false; //AGREGADO MAXI
    public bool empujado = false; //AGREGADO MAXI
    public float tiempoCongeladoPorEmpuje = 1f; //AGREGADO MAXI

    private AudioSource audioSource; //AGREGADO MAXI

    [SerializeField] private LayerMask platformsLayerMask; //toma el layerMask que seria el piso para que el jugador pueda saltar
    [SerializeField] private LayerMask platformsLayerMask2;  //toma el layerMask que seria el piso para que el jugador pueda saltar
    private Rigidbody2D rigidbody2d; //toma el rigidbody del mismo jugador
    private BoxCollider2D boxCollider2d; //toma el box collider del mismo jugador
    public float jumpVelocity = 5f; //para el alcance del salto
    public float moveSpeed = 5f; //para la velocidad de movimiento
    public float midAirControl = 3f; //controla el jugador en el aire, mientras mas valor tenga, el jugador podra controlar mejor su personaje en el aire

    //private bool estaPausado = false; // Variable para rastrear el estado de pausa

    //[SerializeField] private GameObject menuPausa;
    private void Awake()
    {
        rigidbody2d = transform.GetComponent<Rigidbody2D>();
        boxCollider2d = transform.GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>(); //AGREGADO MAXI
    }

    void Update()
    {

        if (atrapado || empujado) //AGREGADO MAXI
        {
            return; //AGREGADO MAXI
        }

        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space)) //Si el jugador esta en el suelo, con space salta
        {
            rigidbody2d.velocity = Vector2.up * jumpVelocity; //realiza el salto
        }
        if (IsGrounded2() && Input.GetKeyDown(KeyCode.Space)) //Si el jugador esta en el suelo, con space salta
        {
            rigidbody2d.velocity = Vector2.up * jumpVelocity; //realiza el salto
        }
        HandleMovement();
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
    private bool IsGrounded2()
    {
        //Permite que el objeto conozca el suelo, en este caso esta como playermask que seria "piso" Luego le devuleve un valor
        RaycastHit2D raycastHit2d2 = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down, 0.1f, platformsLayerMask2);
        return raycastHit2d2.collider != null;

    }

    public void HandleMovement()
    {

        if (Input.GetKey(KeyCode.A)) //Al presionar la letra A
        {
            if (IsGrounded()) //pregunta si esta en el suelo, y si lo esta se movera
            {
                rigidbody2d.velocity = new Vector2(-moveSpeed, rigidbody2d.velocity.y);
                if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                }
            }
            else //si no esta en el suelo, puede moverse igual pero tendra un retraso en el aire
            {
                rigidbody2d.velocity += new Vector2(-moveSpeed * midAirControl * Time.deltaTime, 0);
                rigidbody2d.velocity = new Vector2(Mathf.Clamp(rigidbody2d.velocity.x, -moveSpeed, +moveSpeed), rigidbody2d.velocity.y);
                if (audioSource.isPlaying)
                {
                    audioSource.Stop();
                }
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.D)) //Al presionar la letra D
            {
                if (IsGrounded()) //pregunta si esta en el suelo, y si lo esta se movera
                {
                    rigidbody2d.velocity = new Vector2(+moveSpeed, rigidbody2d.velocity.y);
                    if (!audioSource.isPlaying)
                    {
                        audioSource.Play();
                    }
                }
                else //si no esta en el suelo, puede moverse igual pero tendra un retraso en el aire
                {
                    rigidbody2d.velocity += new Vector2(+moveSpeed * midAirControl * Time.deltaTime, 0);
                    rigidbody2d.velocity = new Vector2(Mathf.Clamp(rigidbody2d.velocity.x, -moveSpeed, +moveSpeed), rigidbody2d.velocity.y);
                    if (audioSource.isPlaying)
                    {
                        audioSource.Stop();
                    }
                }
            }
            else
            {
                //si no apreta las teclas de movimiento, no se movera
                if (IsGrounded())
                {
                    rigidbody2d.velocity = new Vector2(0, rigidbody2d.velocity.y);
                    if(audioSource.isPlaying)
                    {
                        audioSource.Stop();
                    }
                }
            }
        }
    }

    public void Empujado() //AGREGADO MAXI
    {
        empujado = true;
        StartCoroutine(Empuje());
    }

    IEnumerator Empuje() //AGREGADO MAXI
    {
        yield return new WaitForSeconds(tiempoCongeladoPorEmpuje);
        empujado = false;

    }


}
