using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_2DShinyCollectable : MonoBehaviour
{
public GameObject Coleccionable_corona; // Declaración de la variable
public float transitionSpeed = 2.0f; // Velocidad de la transición



    void Awake()
    {
        //Make Collider2D as trigger 
        GetComponent<Collider2D>().isTrigger = true;
    }

    void OnTriggerExit2D(Collider2D c2d)
    {
        // Si el objeto con la etiqueta "Player" o "Max" entra en contacto con la moneda
        if (c2d.CompareTag("Player") || c2d.CompareTag("Max"))
        {
                   
            // Desactiva gradualmente este objeto (Estrellas_coleccionable)
            StartCoroutine(FadeOutAndDeactivate(gameObject));
        
            // Activa gradualmente el objeto "Coleccionable_corona"
            StartCoroutine(FadeInAndActivate(Coleccionable_corona));
        }
    }
    IEnumerator FadeOutAndDeactivate(GameObject obj)
    {
        float elapsedTime = 0;
        Vector3 initialScale = obj.transform.localScale;

        while (elapsedTime < 1.0f)
        {
            elapsedTime += Time.deltaTime * transitionSpeed;
            obj.transform.localScale = Vector3.Lerp(initialScale, Vector3.zero, elapsedTime);
            yield return null;
        }

        obj.SetActive(false);
    }

    IEnumerator FadeInAndActivate(GameObject obj)
    {
        float elapsedTime = 0;
        Vector3 initialScale = obj.transform.localScale;
        obj.SetActive(true);

        while (elapsedTime < 1.0f)
        {
            elapsedTime += Time.deltaTime * transitionSpeed;
            obj.transform.localScale = Vector3.Lerp(Vector3.zero, initialScale, elapsedTime);
            yield return null;
        }
    }
}

