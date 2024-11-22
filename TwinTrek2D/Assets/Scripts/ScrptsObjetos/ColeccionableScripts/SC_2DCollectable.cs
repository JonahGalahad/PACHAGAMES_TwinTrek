using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_2DCollectable : MonoBehaviour
{
    [SerializeField] private GameObject imageLocked;
    [SerializeField] private GameObject imageUnlocked;

    //Keep track of total picked coins (Since the value is static, it can be accessed at "SC_2DCollectable.totalCoins" from any script)
    public static int totalCollectables = 0; // Mantener el total de coleccionables recogidos
    public bool activado = false; // Indica si el objeto está activado
    public float transitionSpeed = 2.0f; // Velocidad de la transición
    public int puntosPorColeccionable = 1000; // Puntos que se añaden por coleccionable

    void Awake()
    {
        // Hacer que el Collider2D sea un trigger 
        GetComponent<Collider2D>().isTrigger = true;
    }

    public void ActivarObjeto()
    {
        StartCoroutine(FadeInAndActivate());
    }

    IEnumerator FadeInAndActivate()
    {
        float elapsedTime = 0;
        Vector3 initialScale = gameObject.transform.localScale;

        while (elapsedTime < 1.0f)
        {
            elapsedTime += Time.deltaTime * transitionSpeed;
            gameObject.transform.localScale = Vector3.Lerp(Vector3.zero, initialScale, elapsedTime);
            yield return null;
        }
        //yield return new WaitForSeconds(1f);
        activado = true;
    }


    void OnTriggerEnter2D(Collider2D c2d)
    {
        // Si el objeto con la etiqueta "Player" entra en contacto con el coleccionable
        if (activado && (c2d.CompareTag("Player")))
        {
            // Añadir la moneda al contador
            //totalCollectables++;
            // Imprimir el número total de monedas (para depuración)
            //Debug.Log("Tienes actualmente " + totalCollectables + " coleccionables.");

            //Desactiva y activa las imagenes de la UI
            imageLocked.SetActive(false);
            imageUnlocked.SetActive(true);
            // Añadir los puntos por el coleccionable al contador de monedas 
            SC_2DCoin.totalCoins += puntosPorColeccionable;
            // Destruir la moneda
            Destroy(gameObject);
        }
    }
}
