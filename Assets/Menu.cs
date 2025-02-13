// Aaron Williams
// 11/11/2022

using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Menu : MonoBehaviourPun //change to MonoBehaviourPunCallbacks??
{
    // variables
    GameObject ray;

    // methods
    void OnEnable()
    {
        ray = GameObject.Find("XR Origin/Camera Offset/RightHand Controller/Ray Interactor");
    }
    public void ToggleVisibilityOverNet()
    {
        gameObject.GetComponent<PhotonView>().RPC("ToggleVisibility", RpcTarget.AllBuffered);
    }
    [PunRPC]
    public void ToggleVisibility()
    {
        gameObject.SetActive(!gameObject.activeInHierarchy);
        ray.SetActive(!ray.activeInHierarchy);
        if (gameObject.activeInHierarchy)
            GameObject.FindObjectOfType<HoldCheck>().hasItemInHand = true;
        if (!gameObject.activeInHierarchy)
            GameObject.FindObjectOfType<HoldCheck>().hasItemInHand = false;
    }
    public void RecalibrateHeight()
    {
        GameObject.FindObjectOfType<VRHeightController>().Resize();
    }
    public void BackToLobby()
    {
        PhotonNetwork.Disconnect();
        PhotonNetwork.LoadLevel(0);
    }
    public void Exit()
    {
        if(PhotonNetwork.IsConnected == true)
            PhotonNetwork.Disconnect();
        Application.Quit();
    }
}
