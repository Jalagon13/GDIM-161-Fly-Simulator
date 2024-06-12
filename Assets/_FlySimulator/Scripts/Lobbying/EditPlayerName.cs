using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    [SerializeField] private Button authenticateButton;


    private string playerName = "";


    private void Awake()
    {
        Instance = this;
    }

    private void Start() {
        OnNameChanged += EditPlayerName_OnNameChanged;

        UI_InputWindow.onValueChanged.AddListener(delegate { UpdateNameText(); });

        Debug.Log("Added Listener to authenticateButton onClick!");
        authenticateButton.onClick.AddListener(() => {
            UpdateNameText();
            OnNameChanged.Invoke(this, EventArgs.Empty);
        });
    }

    private void UpdateNameText()
    {
        playerName = ValidateText.ReturnValidString(playerNameText.text);
        Debug.Log($"Updated PlayerNameText to {playerName}");
    }

    private void EditPlayerName_OnNameChanged(object sender, EventArgs e)
    {
        Debug.Log("Performed  EditPlayerName_OnNameChanged  function.");
        LobbyManager.Instance.UpdatePlayerName(GetPlayerName());
    }

    public string GetPlayerName() {
        return playerName;
    }


}