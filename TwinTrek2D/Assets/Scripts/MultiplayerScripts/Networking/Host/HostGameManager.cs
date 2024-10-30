using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HostGameManager
{
    private string joinCode;
    private Allocation allocation;
    private const int MaxConnections = 2;
    private const string GameSceneName = "PruebasScripts";
   public async Task StartHostAsync()
    {
        try
        {
           allocation = await Relay.Instance.CreateAllocationAsync(MaxConnections);

        }
        catch (Exception e)
        {
            Debug.Log("e");
            return;
        }
        try
        {
            joinCode = await Relay.Instance.GetJoinCodeAsync(allocation.AllocationId);
            Debug.Log(joinCode);
        }
        catch (Exception e)
        {
            Debug.Log("e");
            return;
        }

       UnityTransport transport = NetworkManager.Singleton.GetComponent<UnityTransport>();


        RelayServerData relayServerData = new RelayServerData(allocation, "dtls");
        transport.SetRelayServerData(relayServerData);

        NetworkManager.Singleton.StartHost();

        NetworkManager.Singleton.SceneManager.LoadScene(GameSceneName, LoadSceneMode.Single);
    }
}