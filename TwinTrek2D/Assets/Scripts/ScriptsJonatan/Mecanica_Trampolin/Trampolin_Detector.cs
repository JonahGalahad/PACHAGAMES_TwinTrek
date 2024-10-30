using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampolin_Detector : MonoBehaviour
{
    [SerializeField]private Trampolin_Controller detector; //Colocar el GameObject donde esta el script correspondiente

    // detecta si al roca toca collider trigger del trampolin para disparar el contador
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Roca")) {
            detector.IsMoved = true;
            detector.IsMoveFinish = true;
        }
    }
}
