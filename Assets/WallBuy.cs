// Aaron Williams
// 11/16/2022

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class WallBuy : MonoBehaviour
{
    // variables
    [SerializeField] private GameObject weaponPrefab;
    [SerializeField] private GameObject weaponSpawnPoint;
    [SerializeField] private float price;
    private PlayerStats player;

    // methods
    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerStats>();
    }

    void Update()
    {
        if (player == null)
            player = GameObject.FindObjectOfType<PlayerStats>();
    }

    public void BuyWeaponOnPress()
    {
        gameObject.GetComponent<PhotonView>().RPC("BuyWeapon", RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void BuyWeapon()
    {
        if (player.currCurrency >= price)
        {
            player.currCurrency -= price;
            PhotonNetwork.Instantiate(weaponPrefab.name, weaponSpawnPoint.transform.position, weaponSpawnPoint.transform.rotation);
        }
    }

}
