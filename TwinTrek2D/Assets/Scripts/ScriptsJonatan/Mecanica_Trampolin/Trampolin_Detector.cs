using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampolin_Detector : MonoBehaviour
{
    [SerializeField]private Trampolin_Controller detector;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Roca")) detector.IsMoveFinish = true;
    }
}
