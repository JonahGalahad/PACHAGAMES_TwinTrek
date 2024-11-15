using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SC_2DShinyCollectable : MonoBehaviour
{
    public GameObject Coleccionable; // Declaración de la variable
    public float transitionSpeed = 2.0f; // Velocidad de la transición
    private bool activado = false;

    void Awake()
    {
        //Make Collider2D as trigger 
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!activado && (collision.CompareTag("Player")))
        {
            activado = true;
            // Desactiva gradualmente este objeto (Estrellas_coleccionable)
            StartCoroutine(FadeOutAndDeactivate(Coleccionable));
            // Activa gradualmente el objeto "Coleccionable_corona"
            //StartCoroutine(FadeInAndActivate(Coleccionable));
        }
    }

    IEnumerator FadeOutAndDeactivate(GameObject obj)
    {
        float elapsedTime = 0;
        Vector3 initialScale = gameObject.transform.localScale;

        while (elapsedTime < 1.0f)
        {
            elapsedTime += Time.deltaTime * transitionSpeed;
            gameObject.transform.localScale = Vector3.Lerp(initialScale, Vector3.zero, elapsedTime);
            yield return null;
        }
        gameObject.SetActive(false);
        obj.SetActive(true);
        obj.GetComponent<SC_2DCollectable>().ActivarObjeto();
        Destroy(gameObject);
    }
}

