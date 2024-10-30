using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_UnlockDoor : MonoBehaviour
{
    [SerializeField] private Door_Controller keys; //Colocar el GameObject donde esta el script correspondiente
    [SerializeField] private int condNumKeys = 0; //numero de llaves necesaria para desbloquear puerta
    private bool isInDoor = false;  //flag si esta en rango para interactuar con la puerta
    private Animator anim; 

    private void Start() {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        OpenDoor();
    }

    // detecta si el jugador entra o sale del collider trigger de la puerta
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

    // compara si la cantidad de llaves en posicion es >= a la cantidad de llaves necesarias para abrir la puerta, y abre la puerta
    private void OpenDoor() {
        if (isInDoor==true  && Input.GetKeyDown(KeyCode.E) && keys.CurrentNumKeys>=condNumKeys) {
            Debug.Log("Abrete sesamo");
            keys.CurrentNumKeys -= condNumKeys;
            anim.SetBool("IsAnimStart", true);
        } else if(isInDoor==true && Input.GetKeyDown(KeyCode.E) && keys.CurrentNumKeys!=condNumKeys) {
            Debug.Log("Aun no sesamo");
        }
    }
}
