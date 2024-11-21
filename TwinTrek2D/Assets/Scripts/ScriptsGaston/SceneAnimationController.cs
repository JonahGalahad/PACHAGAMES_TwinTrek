using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneAnimationController : MonoBehaviour
{
    // Singleton: Para que exista una unica instancia en todas las escenas.
    public static SceneAnimationController Instance;

    private void Awake()
    {
        if(Instance==null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // ---------------------------------
    [SerializeField]
    private Animator animator;


    // Inicia la animación de FadeIn (oscurecer la pantalla).
    // Esta función puede ser llamada desde cualquier otro script
    public void FadeIn(float duracion=1.3f)
    {
        StartCoroutine(FadeInCoroutine(duracion));
    }


    // Inicia la animación de FadeOut(desoscurecer la pantalla).
    // Esta función puede ser llamada desde cualquier otro script
    public void FadeOut(float duracion=1.3f)
    {
        StartCoroutine(FadeOutCoroutine(duracion));
    }


    //---------------------------------


    // Corrutinas:
    private IEnumerator FadeInCoroutine(float duracion)
    {
        animator.SetTrigger("FadeIn");

        yield return new WaitForSeconds(duracion);
    }


    private IEnumerator FadeOutCoroutine(float duracion)
    {   
        animator.SetTrigger("FadeOut");

        yield return new WaitForSeconds(duracion);
    }
}
