using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorLock : MonoBehaviour
{
    [SerializeField] private keysController keys;
    [SerializeField] private int condNumKeys = 0;
    private bool isInDoor = false;
    //private bool isHasAllKeys = false;

    void Update()
    {
        OpenDoor();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Player2")) {
            isInDoor = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Player2")) {
            isInDoor = false;
        }
    }
    private void OpenDoor() {
        if (isInDoor==true  && Input.GetKeyDown(KeyCode.E) && keys.CurrentNumKeys==condNumKeys) {
            Debug.Log("Abrete sesamo");
        } else if(isInDoor==true && Input.GetKeyDown(KeyCode.E) && keys.CurrentNumKeys!=condNumKeys) {
            Debug.Log("Aun no sesamo");
        }
    }
}
