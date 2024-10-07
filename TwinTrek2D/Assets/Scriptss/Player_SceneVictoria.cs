using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_SceneVictoria : MonoBehaviour
{
    public static bool dentro = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Max") || collision.gameObject.CompareTag("Player"))
            dentro = true;
    }
}
