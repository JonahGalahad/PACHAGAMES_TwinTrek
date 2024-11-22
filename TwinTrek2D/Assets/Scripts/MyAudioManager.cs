using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyAudioManager : MonoBehaviour
{
    //[SerializeField] GameObject player1, player2;
    [SerializeField] private PlayerLocal[] playersLocal;
    [SerializeField] private EspirituTierraLocalScript[] espiritusTierras;
    [SerializeField] private FlorLocalScript[] floresSuelo;
    [SerializeField] private Controller_SoundOnScreen[] abejasEnScene;
    //[SerializeField] private AbejaV2[] abejaV2s;

    private bool estabaSonando = false;

    private void Start()
    {
        playersLocal = FindObjectsOfType<PlayerLocal>();
        espiritusTierras = FindObjectsOfType<EspirituTierraLocalScript>();
        floresSuelo = FindObjectsOfType<FlorLocalScript>();
        abejasEnScene = FindObjectsOfType<Controller_SoundOnScreen>();
        //abejaV2s = FindObjectsOfType<AbejaV2>();
    }
    public void PararSonidos()
    {
        //player1.GetComponent<PlayerLocal>().WalkEvent.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        //player1.GetComponent<PlayerLocal>().ClimbEvent.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        //player1.GetComponent<PlayerLocal>().WalkEvent.release();
        //player2.GetComponent<PlayerLocal>().WalkEvent.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        //player2.GetComponent<PlayerLocal>().ClimbEvent.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        foreach (PlayerLocal playerDetenerCosas in playersLocal)
        {
            playerDetenerCosas.WalkEvent.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            playerDetenerCosas.ClimbEvent.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }
        foreach (EspirituTierraLocalScript espirituTierraReinicio in espiritusTierras)
        {
            espirituTierraReinicio.TierraSound.Stop();
        }

        foreach (Controller_SoundOnScreen abejasEnSceneStopSound in abejasEnScene)
        {
            if(abejasEnSceneStopSound.EventSound.IsPlaying())
            {
                abejasEnSceneStopSound.reproducir = false;
                abejasEnSceneStopSound.EventSound.Stop();
                //estabaSonando = true;
            }
        }
        /*foreach (AbejaV2 abejaPararSonidos in abejaV2s)
        {
            abejaPararSonidos.AbejaShotSound.Stop();
        }*/
    }

    public void ReanudarSonidos()
    {
        foreach (PlayerLocal playerReanudarSonidos in playersLocal)
        {
            playerReanudarSonidos.WalkEvent.start();
            playerReanudarSonidos.ClimbEvent.start();
        }
        foreach (EspirituTierraLocalScript espirituTierraReinicio in espiritusTierras)
        {
            if(espirituTierraReinicio.JugadorCapturado == true)
            {
                espirituTierraReinicio.TierraSound.Play();
            }
        }

        foreach (Controller_SoundOnScreen abejasEnScenePlay in abejasEnScene)
        {
            if (!abejasEnScenePlay.reproducir)
            {
                abejasEnScenePlay.reproducir=true;
                //abejasEnSceneStopSound.EventSound.Play();
            }
        }
        //estabaSonando = false;
    }
}
