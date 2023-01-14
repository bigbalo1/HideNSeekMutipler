using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetworkManagerMainMenu : NetworkManager
{
    private void OnLevelWasLoaded(int level)
    {
        if(level == 0)
        {
            //this is the main menu
            Button buttonHost = GameObject.Find("ButtonHost").GetComponent<Button>();
            buttonHost.onClick.RemoveAllListeners();
            buttonHost.onClick.AddListener(HostGame);

            Button buttonJoin = GameObject.Find("ButtonJoin").GetComponent<Button>();
            buttonJoin.onClick.RemoveAllListeners();
            buttonJoin.onClick.AddListener(JoinGame);
        }
        else
        {
            Button buttonDisconnect = GameObject.Find("ButtonDisconnect").GetComponent<Button>();
            buttonDisconnect.onClick.RemoveAllListeners();
            buttonDisconnect.onClick.AddListener(Disconnect);
        }
    }

    public void HostGame()
    {
        NetworkManager.singleton.networkPort = 7777; 
        NetworkManager.singleton.StartHost();
    }

    public void JoinGame()
    {
        NetworkManager.singleton.networkPort = 7777;
        NetworkManager.singleton.networkAddress = "127.0.0.1";
        NetworkManager.singleton.StartClient();
    }

    public void Disconnect()
    {
        NetworkManager.singleton.StopHost();
    }
}
