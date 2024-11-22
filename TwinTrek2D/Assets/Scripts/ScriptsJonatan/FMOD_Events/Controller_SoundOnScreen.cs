using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class Controller_SoundOnScreen : MonoBehaviour
{
    //public static Controller_SoundOnScreen Instance ;
    [SerializeField] private StudioEventEmitter eventSound;

    //[SerializeField] private StudioEventEmitter espinoSound;
    //[SerializeField] private StudioEventEmitter slimeSound;
    //[SerializeField] private StudioEventEmitter fuegoSound;
    private Camera mainCamera;

    public StudioEventEmitter EventSound { get { return eventSound; } set { eventSound = value; } }

    public bool reproducir = true;

    private void Start() {
        mainCamera = Camera.main;
    }
    private void Update() {
        if(!reproducir)
        {
            return;
        }

        if(IsVisible()) {
            if(!eventSound.IsPlaying())
            eventSound.Play();
        } else {
            if(eventSound.IsPlaying())
            eventSound.Stop();
        }
    }
    public bool IsVisible() {
        if (mainCamera == null) return false;

        Vector3 screenPoint = mainCamera.WorldToViewportPoint(transform.position);
        bool isInViewport = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;

        return isInViewport;
    }
}
