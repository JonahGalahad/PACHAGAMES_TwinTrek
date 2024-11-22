using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_UnlockDoor : MonoBehaviour
{
    [SerializeField] private Runas_Counter llavesText;

    [SerializeField] private Door_Controller keys; //Colocar el GameObject donde esta el script correspondiente
    [SerializeField] private int condNumKeys = 0; //numero de llaves necesaria para desbloquear puerta
    private bool isInDoor = false;  //flag si esta en rango para interactuar con la puerta
    private Animator anim; 

    [SerializeField] private GameObject key1;
    [SerializeField] private GameObject key2;
    [SerializeField] private GameObject key3;

    private void Start() {
        anim = GetComponent<Animator>();
        if (condNumKeys == 1)
        {
            key1.SetActive(true);
            key2.SetActive(true);
        }
        else if (condNumKeys == 2)
        {
            key1.SetActive(true);
        }
    }
    void Update()
    {
        OpenDoor();
    }

    // detecta si el jugador entra o sale del collider trigger de la puerta
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            isInDoor = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            isInDoor = false;
        }
    }

    // compara si la cantidad de llaves en posicion es >= a la cantidad de llaves necesarias para abrir la puerta, y abre la puerta
    private void OpenDoor() {
        if (isInDoor==true  && (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.RightControl)) && keys.CurrentNumKeys>=condNumKeys)
        {
            Debug.Log("Abrete sesamo");
            llavesText.RestarRunasObtenidas(condNumKeys);
            keys.CurrentNumKeys -= condNumKeys;
            anim.SetBool("IsAnimStart", true);
            key3.SetActive(true);
            key2.SetActive(true);
            key1.SetActive(true);
        }
        else if(isInDoor==true && (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.RightControl)) && keys.CurrentNumKeys!=condNumKeys)
        {
            Debug.Log("Aun no sesamo");
        }
    }
}
