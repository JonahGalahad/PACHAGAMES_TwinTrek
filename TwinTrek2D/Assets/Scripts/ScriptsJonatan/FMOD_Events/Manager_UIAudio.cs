using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class Manager_UIAudio : MonoBehaviour
{
    public static Manager_UIAudio instance;
    [SerializeField] private EventReference clickButton;
    //[SerializeField] private EventReference passOnButton;

    private void Awake() {
        instance = this;
    }
    public void PlayClickEvent() {
        RuntimeManager.PlayOneShot(clickButton);
    }
    /*public void PlayPassOnEvent() {
        RuntimeManager.PlayOneShot(passOnButton);
    }*/
}
