using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EspinoDetectorScript : MonoBehaviour
{
    [SerializeField] private GameObject colisionEspino;
    [SerializeField] private EspinoAndanteScript espinoScript;
    [SerializeField] private bool jugadorDetectado = false;
    [SerializeField] private BoxCollider2D boxCollider;

    //[SerializeField] private List<Transform> jugadoresDentro = new List<Transform>(); // Lista para almacenar las posiciones de los jugadores
    private void Start()
    {
        espinoScript = colisionEspino.GetComponent<EspinoAndanteScript>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    public void DejarDetectarJugador()
    {
        jugadorDetectado = false;
        boxCollider.enabled = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !jugadorDetectado)
        {
            jugadorDetectado = true;
            espinoScript.CambiarModoDiablo();
            boxCollider.enabled = false;
        }
    }
}
