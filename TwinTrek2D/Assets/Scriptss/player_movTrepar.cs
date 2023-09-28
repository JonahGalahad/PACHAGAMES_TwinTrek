using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_movTrepar : MonoBehaviour
{
    public bool estaEnEnredadera = false;
    public bool estaEnParedLateral = false;
    private Rigidbody2D rigidbody2D;
    public float moveSpeedTrepar = 1.5f; //Velocidad con la que el player trepará

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (estaEnEnredadera || estaEnParedLateral)
        {
            float verticalInput = Input.GetAxis("Vertical");

            if (verticalInput != 0)
            {
                // Mover hacia arriba
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, verticalInput * moveSpeedTrepar);
            }
            else
            {
                // Si no se presiona hacia arriba, dejar de moverse verticalmente
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0);
                if (estaEnEnredadera)
                {
                    if (verticalInput < 0)
                    {
                        rigidbody2D.gravityScale = 1f;
                        estaEnEnredadera = false;
                    }
                }
            }

            float horizontalInput = Input.GetAxis("Horizontal");

            if (horizontalInput != 0)
            {
                // Mover hacia los lados
                rigidbody2D.velocity = new Vector2(horizontalInput * moveSpeedTrepar, rigidbody2D.velocity.y);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enredadera"))
        {
            estaEnEnredadera = true;
            rigidbody2D.gravityScale = 0f; // Desactivar gravedad mientras está en el techo
        }
        if (collision.gameObject.CompareTag("ParedLateral"))
        {
            estaEnParedLateral = true;
            rigidbody2D.gravityScale = 0f; // Desactivar gravedad mientras está en el techo
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enredadera"))
        {
            estaEnEnredadera = false;
            rigidbody2D.gravityScale = 1f; // Restaurar la gravedad cuando sale del techo
        }
        if (collision.gameObject.CompareTag("ParedLateral"))
        {
            estaEnParedLateral = false;
            rigidbody2D.gravityScale = 1f; // Restaurar la gravedad cuando sale del techo
        }
    }
}

/*private bool estaEnTecho = false;
private Rigidbody2D rigidbody2D;
public float moveSpeedTrepar = 1.5f; //Velocidad con la que el player trepará

void Start()
{
    rigidbody2D = GetComponent<Rigidbody2D>();
}

void Update()
{
    if (estaEnTecho)
    {
        float verticalInput = Input.GetAxis("Vertical");

        if (verticalInput > 0)
        {
            // Mover hacia arriba
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, verticalInput * moveSpeedTrepar);
        }
        else
        {
            // Si no se presiona hacia arriba, dejar de moverse verticalmente
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0);
        }

        float horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput != 0)
        {
            // Mover hacia los lados
            rigidbody2D.velocity = new Vector2(horizontalInput * moveSpeedTrepar, rigidbody2D.velocity.y);
        }
    }
}

void OnCollisionEnter2D(Collision2D collision)
{
    if (collision.gameObject.CompareTag("Techo"))
    {
        estaEnTecho = true;
        rigidbody2D.gravityScale = 0f; // Desactivar gravedad mientras está en el techo
    }
}

void OnCollisionExit2D(Collision2D collision)
{
    if (collision.gameObject.CompareTag("Techo"))
    {
        estaEnTecho = false;
        rigidbody2D.gravityScale = 1f; // Restaurar la gravedad cuando sale del techo
    }
}
}*/
