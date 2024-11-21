using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoOndaTriangular : MonoBehaviour
{
    [SerializeField] private float velocidad = 1.5f;
    [SerializeField] private float amplitud = 0.02f; // Ajusta la amplitud para un movimiento más suave
    [SerializeField] private float frecuencia = 0.7f; // Controla la frecuencia del movimiento vertical
    private Vector3 posicionInicial;
    private float tiempo;
    public int direccion = -1; // -1 para izquierda a derecha (inicia de esa manera), 1 para derecha a izquierda

    private RotacionSentidoHorario rotacionScript; // Referencia al script de rotación

    void Start()
    {
        posicionInicial = transform.position;
        tiempo = 0f;
        // Obtener la referencia al script de rotación 
        rotacionScript = transform.Find("EspirituDeLuzSprite").GetComponent<RotacionSentidoHorario>();
    }

    void Update()
    {
        MoverEnemigo();
    }
    public void CambiarDireccion()
    {
        direccion *= -1;
    }
    public void EstablecerDireccion(int nuevaDireccion)
    {
        direccion = nuevaDireccion;
    }
    void MoverEnemigo()
    {
        tiempo += Time.deltaTime * frecuencia;
        float movimientoHorizontal = direccion * velocidad * Time.deltaTime;
        float movimientoVertical = amplitud * Mathf.PingPong(tiempo, 1f) * 2f - amplitud; // Mathf.PingPong(tiempo, 1f) devuelve un valor que varía de 0 a 1 y luego regresa a 0, creando un efecto de ida y vuelta
        transform.Translate(new Vector3(movimientoHorizontal, movimientoVertical, 0));

        // Actualizar la dirección de rotación 
        rotacionScript.moviendoDerecha = direccion == 1;
    }
}