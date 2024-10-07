using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControllerEnredadera : MonoBehaviour
{
    private AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Max"))
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
