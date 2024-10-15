using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class doorKeys : MonoBehaviour
{
    [SerializeField] private keysController controller;
    private bool isCollisionKey = false;

    private void Update() {
        TakedKey();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Player2")) {
            isCollisionKey = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Player2")) {
            isCollisionKey = false;
        }
    }
    private void TakedKey() {
        if (isCollisionKey==true && Input.GetKeyDown(KeyCode.E)) {
                controller.CurrentNumKeys += 1;
                isCollisionKey = false;
                Destroy(this.gameObject);
            }
    }
}
