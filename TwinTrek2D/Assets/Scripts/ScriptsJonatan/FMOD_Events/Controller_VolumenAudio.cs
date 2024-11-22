using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FMODUnity;

public class Controller_VolumenAudio : MonoBehaviour
{
    [SerializeField] private Slider slider=null;
    [SerializeField] private string busPath="";
    private FMOD.Studio.Bus bus;
    // Start is called before the first frame update
    private void Start() {
        if (busPath != "") {
            bus = RuntimeManager.GetBus(busPath);
        }
        bus.getVolume(out float volume);
        slider.value = volume * slider.maxValue;
        UpdateVolumeValue();
    }

    public void UpdateVolumeValue() {
        if (slider != null) {
            bus.setVolume(slider.value / slider.maxValue);
        }
        
    }
}
