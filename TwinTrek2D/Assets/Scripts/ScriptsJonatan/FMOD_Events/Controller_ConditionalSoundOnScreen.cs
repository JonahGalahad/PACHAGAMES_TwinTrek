using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class Controller_ConditionalSoundOnScreen : MonoBehaviour
{
    [SerializeField] private StudioEventEmitter eventSound;
    private EventInstance eventInstance;
    private Camera mainCamera;
    //private bool isFlag = false;
    private void Start() {
        mainCamera = Camera.main;
        //isFlag = false;
        //eventInstance = RuntimeManager.cl
    }
    private void Update() {
        if(IsVisible()) {
            setParameter(true);
            if(!eventSound.IsPlaying())
            eventSound.Play();
        } else {
            setParameter(false);
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
    public void setParameter(bool isFlag) {
        eventSound.SetParameter("IsInScene", isFlag ? 1f : 0F);
    }
}
