using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class MovimientoSlime : MonoBehaviour
{
    [SerializeField] private float velocidadMaxima = 2.0f;
    [SerializeField] private float umbralDistancia = 0.1f;
    [SerializeField] private float tiempoDeAceleracion = 1.0f;
    [SerializeField] private float pausaEntreMovimientos = 0.5f;
    [SerializeField] private float pausaAlLlegar = 1f;

    private Transform puntoA;
    private Transform puntoB;
    private Transform objetivoActual;
    private SpriteRenderer spriteRenderer;
    private float velocidadActual = 0.0f;
    private float tiempoInicioMovimiento;
    private DejarBaba dejarBaba;
    private SlimeSalto slimeSalto;
    private int contadorMovimientos = 0;

    void Start()
    {
        Transform nuevoSlime = transform.parent;
        puntoA = nuevoSlime.Find("Punto A");
        puntoB = nuevoSlime.Find("Punto B");

        objetivoActual = puntoA;
        tiempoInicioMovimiento = Time.time;

        spriteRenderer = GetComponent<SpriteRenderer>();
        dejarBaba = GetComponent<DejarBaba>();
        slimeSalto = GetComponent<SlimeSalto>();

        StartCoroutine(MoverSlime());
    }

    IEnumerator MoverSlime()
    {
        while (true)
        {
            // 1° Comprueba donde está el punto objetivo
            Vector3 direccion = objetivoActual.position - transform.position;

            // 2° Ajustar la dirección del sprite //IMPORTANTE El slime debe empezar entre el punto A y el punto B
            if (direccion.x < 0 && spriteRenderer.transform.localScale.x > 0) // Mover a la izquierda
            {
                Vector3 nuevaEscala = spriteRenderer.transform.localScale;
                nuevaEscala.x *= -1;
                spriteRenderer.transform.localScale = nuevaEscala;
            }
            else if (direccion.x > 0 && spriteRenderer.transform.localScale.x < 0) // Mover a la derecha
            {
                Vector3 nuevaEscala = spriteRenderer.transform.localScale;
                nuevaEscala.x *= -1;
                spriteRenderer.transform.localScale = nuevaEscala;
            }

            // 3° Dejar baba en el piso si está inicializado
            if (dejarBaba != null && dejarBaba.inicializado)
            {
                dejarBaba.DejarSlime();
            }
            
            // 4° Hacer un avance o salto
            if (contadorMovimientos % 3 == 0) //Si el contadorMovimientos es múltiplo de 3 y no es la primera vez, se llama al método Saltar
            {
                if (contadorMovimientos != 0) //Esto es para evitar que la primera vez inicie con un salto ya que 0/3 = 0, o sea como si 0 fuera múltiplo de 3
                    yield return slimeSalto.Saltar(objetivoActual, puntoA, puntoB, tiempoInicioMovimiento, spriteRenderer, umbralDistancia);
            }
            else
            //De lo contrario, se mueve el Slime hacia el objetivo durante el tiempo de aceleración
            {
                float tiempoTranscurrido = 0; //Inicializa tiempoTranscurrido a 0. Este valor será usado para medir cuánto tiempo ha pasado desde que el Slime comenzó a moverse
                while (tiempoTranscurrido < tiempoDeAceleracion) //Comienza un bucle que continuará hasta que tiempoTranscurrido sea mayor o igual a tiempoDeAceleracion
                {
                    tiempoTranscurrido += Time.deltaTime; //Aumenta tiempoTranscurrido en cada frame por Time.deltaTime
                    velocidadActual = Mathf.Lerp(0, velocidadMaxima, tiempoTranscurrido / tiempoDeAceleracion); //Se usa Mathf.Lerp para que la velocidad del Slime aumente gradualmente desde 0 hasta velocidadMaxima a lo largo de tiempoDeAceleracion
                    transform.position = Vector3.MoveTowards(transform.position, objetivoActual.position, velocidadActual * Time.deltaTime); //Mueve el Slime desde su posición actual hacia objetivoActual.position a una velocidad de velocidadActual

                    if (Vector3.Distance(transform.position, objetivoActual.position) <= umbralDistancia)
                    {
                        yield return new WaitForSeconds(pausaAlLlegar);
                        objetivoActual = (objetivoActual == puntoA) ? puntoB : puntoA;
                        tiempoInicioMovimiento = Time.time;
                    }

                    yield return null;
                }
            }

            // 5° Aumenta el contador y espera para hacer el siguiente movimiento
            contadorMovimientos++;
            yield return new WaitForSeconds(pausaEntreMovimientos);
        }
    }
}
