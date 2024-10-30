using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Controller : MonoBehaviour
{
    //Registra la cantidad actual de llaves obtenidas surante la partida
    [SerializeField] private int currentNumKeys;
    public int CurrentNumKeys { get { return currentNumKeys; } set { currentNumKeys = value; } }
    
}
