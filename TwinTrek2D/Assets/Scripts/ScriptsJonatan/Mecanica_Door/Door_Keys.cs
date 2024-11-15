using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class Door_Keys : MonoBehaviour
{
    [SerializeField] private Door_Controller controller; //Colocar el GameObject donde esta el script correspondiente
    private bool isCollisionKey = false; //flag de colision con las llaves

    private void Update() {
        TakedKey();
    }

    // Detecta cuando el player entra o sale del collider trigger del objeto llave
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            isCollisionKey = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            isCollisionKey = false;
        }
    }

    // Metedo para obtener llave, aumenta en 1 en contador de llaves actuales
    private void TakedKey() {
        if (isCollisionKey==true && (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.RightControl))) {
                controller.CurrentNumKeys += 1;
                isCollisionKey = false;
                Destroy(this.gameObject);
            }
    }
}
