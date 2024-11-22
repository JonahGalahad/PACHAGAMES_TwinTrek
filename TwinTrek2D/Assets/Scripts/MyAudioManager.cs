using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyAudioManager : MonoBehaviour
{
    //[SerializeField] GameObject player1, player2;
    [SerializeField] private PlayerLocal[] playersLocal;
    [SerializeField] private EspirituTierraLocalScript[] espiritusTierras;
    [SerializeField] private FlorLocalScript[] floresSuelo;

    private void Start()
    {
        playersLocal = FindObjectsOfType<PlayerLocal>();
        espiritusTierras = FindObjectsOfType<EspirituTierraLocalScript>();
        floresSuelo = FindObjectsOfType<FlorLocalScript>();
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
            playerDetenerCosas.WalkEvent.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }
        foreach (EspirituTierraLocalScript espirituTierraReinicio in espiritusTierras)
        {
            espirituTierraReinicio.TierraSound.Stop();
        }
    }
}
