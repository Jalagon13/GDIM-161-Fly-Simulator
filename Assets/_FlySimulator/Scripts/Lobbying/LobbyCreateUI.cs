using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//Code from CodeMonkey Tutorial!!!! Project download.
//Modified for our game's purposes by Alaina Klaes.
public class LobbyCreateUI : MonoBehaviour {


    public static LobbyCreateUI Instance { get; private set; }


    [SerializeField] private Button createButton;
    [SerializeField] private Button publicPrivateButton;
    [SerializeField] private TextMeshProUGUI publicPrivateText;
    [SerializeField] private TMP_InputField lobbyNameInputField;
    [SerializeField] private GameObject inLobbyMenu;
    [SerializeField] private GameObject errorMessage;


    private string lobbyName;
    private bool isPrivate;
    private int maxPlayers = 4;

    private void Awake() {
        Instance = this;

        createButton.onClick.AddListener(() => {
            UpdateLobbyNameText();
            try
            {
                LobbyManager.Instance.CreateLobby(
                    lobbyName,
                    maxPlayers,
                    isPrivate
                );
                this.gameObject.SetActive( false );
                inLobbyMenu.SetActive( true );
            }
            catch
            {
                errorMessage.SetActive(true);
            }
        });



        publicPrivateButton.onClick.AddListener(() => {
            isPrivate = !isPrivate;
            UpdateText();
        });

        lobbyNameInputField.onValueChanged.AddListener(delegate { UpdateLobbyNameText(); });
    }

    private void UpdateLobbyNameText()
    {
        lobbyName = ValidateText.ReturnValidString(lobbyNameInputField.text);
        Debug.Log($"Updated LobbyNameText to {lobbyName}");
    }

    private void UpdateText() {
        publicPrivateText.text = isPrivate ? "<color=#DD1800>Private</color>" : "<color=#70CC00>Public</color>";
    }

    public void OnEnable() {
        Debug.Log("Performing OnEnable function!");
        lobbyName = "MyLobby";
        isPrivate = false;
        maxPlayers = 4;

        UpdateText();
    }

}