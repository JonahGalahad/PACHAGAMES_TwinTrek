using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keysController : MonoBehaviour
{
    [SerializeField] private int currentNumKeys;
    public int CurrentNumKeys { get { return currentNumKeys; } set { currentNumKeys = value; } }
    
}
