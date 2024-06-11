using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//Code from CodeMonkey Tutorial!!!! Project download.
//Modified for our game's purposes by Alaina Klaes.
public class EditPlayerName : MonoBehaviour {


    public static EditPlayerName Instance { get; private set; }


    public event EventHandler OnNameChanged;


    [SerializeField] private TextMeshProUGUI playerNameText;
    [SerializeField] private TMP_InputField UI_InputWindow;


    private string playerName = "";


    private void Awake() {
    }

    private void Start() {
        OnNameChanged += EditPlayerName_OnNameChanged;
    }
    private void FixedUpdate()
    {
    }

    private void EditPlayerName_OnNameChanged(object sender, EventArgs e) {
        LobbyManager.Instance.UpdatePlayerName(GetPlayerName());
    }

    public string GetPlayerName() {
        return playerName;
    }


}