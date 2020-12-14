using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class MyPhotonNetwork : MonoBehaviourPunCallbacks
{
    public override void OnConnected()
    {
        Debug.Log("OnConnected");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("OnDisConnected");
    }

    public void OnClickConnect()
    {
        Photon.Pun.PhotonNetwork.ConnectUsingSettings();
        //Photon.Pun.PhotonNetwork.GameVersion = "test";
    }

    public void OnClickDisconnect()
    {
        Photon.Pun.PhotonNetwork.Disconnect();
    }


    public override void OnConnectedToMaster()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom();
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);

        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 2 });
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        PhotonNetwork.LoadLevel("Level_FiveInARow");
    }
}
