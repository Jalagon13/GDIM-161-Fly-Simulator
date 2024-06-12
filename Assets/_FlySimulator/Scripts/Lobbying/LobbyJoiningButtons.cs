using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.UI;

//Code from CodeMonkey Tutorial!!!! Project download.
//Modified for our game's purposes by Alaina Klaes.
public class LobbyJoiningButtons : MonoBehaviour
{
    public void OnClickJoin()
    {
        LobbyManager.Instance.ToggleCurrentOffAndLobbyUIOn();
    }
}