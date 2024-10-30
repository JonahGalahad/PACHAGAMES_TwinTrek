using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_2DCollectable : MonoBehaviour
{
    //Keep track of total picked coins (Since the value is static, it can be accessed at "SC_2DCollectable.totalCoins" from any script)
    public static int totalCollectables = 0;
    public bool activado = false;
    public float transitionSpeed = 2.0f; // Velocidad de la transición
    void Awake()
    {
        //Make Collider2D as trigger 
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
        // Si el objeto con la etiqueta "Player" o "Max" entra en contacto con la moneda
        if (activado && (c2d.CompareTag("Sam") || c2d.CompareTag("Max")))
        {
            // Añadir la moneda al contador
            totalCollectables++;
            // Imprimir el número total de monedas (para depuración)
            Debug.Log("Tienes actualmente " + totalCollectables + " coleccionables.");
            // Destruir la moneda
            Destroy(gameObject);
        }
    }
}
