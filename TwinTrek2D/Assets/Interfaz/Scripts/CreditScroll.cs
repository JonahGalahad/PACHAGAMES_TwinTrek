using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditScroll : MonoBehaviour
{
      public float scrollSpeed = 50f;           // Velocidad de desplazamiento
    public GameObject objectToActivate;       // Objeto a activar al finalizar
    public float endPositionY = 1000f;        // Posición Y donde queremos que desaparezca el texto

    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        // Desplazar el texto hacia arriba
        rectTransform.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;

        if (rectTransform.anchoredPosition.y >= endPositionY)
        {
            gameObject.SetActive(false);  // Ocultar el texto
            objectToActivate.SetActive(true);  // Activar el objeto deseado
        }
    }
}
