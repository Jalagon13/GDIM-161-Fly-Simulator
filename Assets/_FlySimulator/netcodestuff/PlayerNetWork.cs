using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class PlayerNetWork : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playerNameUI;
    private NetworkObject thisNetworkObject;
    private string thisPlayerName = "";
    // Start is called before the first frame update
    void Start()
    {
        thisNetworkObject = GetComponent<NetworkObject>();
        PopulatePlayerName();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PopulatePlayerName()
    {
        GameManager gmInst = GameManager.Instance;
        if (gmInst != null && gmInst.LocalName != null)
        {
            thisPlayerName = gmInst.LocalName;
        }
        else
        {
            thisPlayerName = "Player " + (thisNetworkObject.OwnerClientId + 1);
        }
        playerNameUI.text = thisPlayerName;
    }
}
