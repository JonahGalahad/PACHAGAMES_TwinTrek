using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AbejaV2 : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform player2;
    [SerializeField] private Transform shootPosition;
    [SerializeField] private GameObject bulletPrefab;
    public float bulletSpeed = 20f;
    public float minDistance = 5f;
    public float maxDistance = 30f;
    private bool playerInRange;
    private bool player2InRange;

    public float speed;
    private float angle = 0.0f;
    public float longitud = 1f;

    void Start()
    {
        InvokeRepeating("Shoot", 0.5f, 2f);
        UpdatePlayers();
    }

    void Update()
    {
        if (player == null || player2 == null)
        {
            UpdatePlayers();
        }

        // Actualizar el estado de cada jugador dentro del rango
        playerInRange = player != null && Vector2.Distance(player.position, transform.position) < maxDistance && Vector2.Distance(player.position, transform.position) > minDistance;
        player2InRange = player2 != null && Vector2.Distance(player2.position, transform.position) < maxDistance && Vector2.Distance(player2.position, transform.position) > minDistance;

        // Movimiento en el eje X con oscilación
        transform.Translate(speed * Time.deltaTime, Mathf.Sin(angle) * Time.deltaTime * longitud, 0f);
        angle += 0.01f;
    }

    private void UpdatePlayers()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        if (players.Length > 0)
            player = players[0].transform;

        if (players.Length > 1)
            player2 = players[1].transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bloque"))
        {
            Debug.Log("Faaaack");
            speed *= -1;
        }
    }

    private void Shoot()
    {
        // Verificar si al menos un jugador está en rango
        if (playerInRange || player2InRange)
        {
            // Crear la bala
            GameObject bullet = Instantiate(bulletPrefab, shootPosition.position, Quaternion.identity);
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

            // Elegir aleatoriamente entre el jugador 1 y el jugador 2 si ambos están en rango, o disparar al único en rango
            Transform targetPlayer;
            if (playerInRange && player2InRange)
            {
                targetPlayer = (Random.Range(0, 2) == 0) ? player : player2;
            }
            else if (playerInRange)
            {
                targetPlayer = player;
            }
            else
            {
                targetPlayer = player2;
            }

            // Calcular la dirección hacia el jugador elegido
            Vector2 direction = (targetPlayer.position - transform.position).normalized;

            // Establecer la velocidad de la bala en dirección al jugador elegido
            bulletRb.velocity = direction * bulletSpeed;

            // Calcular el ángulo entre la bala y el jugador para rotarla en la dirección correcta
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.Euler(0, 0, angle - 180); // Ajusta la rotación según el sprite
        }
    }
}
/*[SerializeField] private Transform player;
[SerializeField] private Transform player2;
[SerializeField] private Transform shootPosition;
[SerializeField] private GameObject bulletPrefab;
public float bulletSpeed = 20f;
public float minDistance = 5f;
public float maxDistance = 30f;
private bool Shoots;

// [SerializeField] private SpriteRenderer direccion; //para cambiar el sentido de la imagen
public float speed;
private float angle = 0.0f;
public float longitud = 1f;
// public bool seno;

void Start()
{
    //Invokerepeating para que invoke el metodo disparar cada 2 segundos
    InvokeRepeating("Shoot", 0.5f, 2f);
    UpdatePlayers();

}

void Update()
{

    if (player == null || player2 == null)
    {
        UpdatePlayers();
    }

    if (player != null)
    {
        float distanceToPlayer = Vector2.Distance(player.position, transform.position);
        Shoots = distanceToPlayer < maxDistance && distanceToPlayer > minDistance;
    }

    if (player2 != null)
    {
        float distanceToPlayer2 = Vector2.Distance(player2.position, transform.position);
        Shoots = distanceToPlayer2 < maxDistance && distanceToPlayer2 > minDistance;
    }

    // Movimiento en el eje X con oscilación
    transform.Translate(speed * Time.deltaTime, Mathf.Sin(angle) * Time.deltaTime * longitud, 0f);
    angle += 0.01f;


}
private void UpdatePlayers()
{
    GameObject[] players = GameObject.FindGameObjectsWithTag("Sam");

    if (players.Length > 0)
        player = players[0].transform;

    if (players.Length > 1)
        player2 = players[1].transform;
}

private void OnTriggerEnter2D(Collider2D collision)
{
    if (collision.gameObject.CompareTag("MetaFinal")) 
    {
        Debug.Log("Faaaack");
        speed *= -1;

    }
}



private void Shoot()
{
    if (Shoots && player != null && player2 != null)
    {
        // Crear la bala
        GameObject bullet = Instantiate(bulletPrefab, shootPosition.position, Quaternion.identity);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

        // Elegir aleatoriamente entre el jugador 1 y el jugador 2
        Transform targetPlayer = (Random.Range(0, 2) == 0) ? player : player2;

        // Calcular la dirección hacia el jugador elegido
        Vector2 direction = (targetPlayer.position - transform.position).normalized;

        // Establecer la velocidad de la bala en dirección al jugador elegido
        bulletRb.velocity = direction * bulletSpeed;

        // Calcular el ángulo entre la bala y el jugador para rotarla en la dirección correcta
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(0, 0, angle - 180); // Ajusta la rotación según el sprite
    }
}


}

*/



//UPDATE
/*  //Calcula la Distancia entre el objeto que dispara y su objetivo (Sirve nomas para conocer la distancia)

  //float distanceToPlayer = (player.transform.position - transform.position).magnitude; //Una forma
  float distanceToPlayer = Vector2.Distance(player.transform.position, transform.position); //Otra forma
  float distanceToPlayer2 = Vector2.Distance(player2.transform.position, transform.position); //Otra forma
  //Debug.Log(distanceToPlayer);

  //Hace posible que el objeto (cañon) te apunte y dispare solo cuando el jugador esta en su rango
  if (distanceToPlayer < maxDistance && distanceToPlayer > minDistance)
  {
     // transform.up = player.transform.position - transform.position;
      Shoots = true;
  }
  else
  {
      Shoots = false;
  }
  if (distanceToPlayer2 < maxDistance && distanceToPlayer2 > minDistance)
  {
     // transform.up = player2.transform.position - transform.position;
      Shoots = true;
  }
  else
  {
      Shoots = false;
  }

  //Movimiento hacia arriba
  //transform.Translate(Mathf.Sin(angle) * Time.deltaTime * longitud, speed * Time.deltaTime, 0f);
  //Movimiento hacia derecha
  transform.Translate(speed * Time.deltaTime, Mathf.Sin(angle) * Time.deltaTime * longitud, 0f);
  angle += 0.01f;
*/
// Comprueba y actualiza los jugadores si alguno falta


/*  private void Shoot()
      {
          if (Shoots)
          {
              // Crear la bola de ácido (bulletPrefab)
              GameObject bullet = Instantiate(bulletPrefab, shootPosition.position, Quaternion.identity);

              // Obtener el Rigidbody2D de la bola de ácido
              Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

              // Calcular la dirección hacia el jugador
              Vector2 direction = (player.transform.position - transform.position).normalized;
              Vector2 direction2 = (player2.transform.position - transform.position).normalized;

              // Establecer la velocidad de la bola en dirección al jugador
              bulletRb.velocity = direction * bulletSpeed;
              bulletRb.velocity = direction2 * bulletSpeed;

              // Calcular el ángulo entre la bola y el jugador para rotar la bola en la dirección correcta
              float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
              float angle2 = Mathf.Atan2(direction2.y, direction2.x) * Mathf.Rad2Deg;

              // Aplicar la rotación a la bola de ácido para que su "cola" apunte hacia el gusano
              bullet.transform.rotation = Quaternion.Euler(0, 0, angle - 180); // -90 para ajustar la orientación de la cola del sprite
              bullet.transform.rotation = Quaternion.Euler(0, 0, angle2 - 180); // -90 para ajustar la orientación de la cola del sprite
          }
      }*/