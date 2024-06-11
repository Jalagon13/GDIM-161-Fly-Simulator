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
    [SerializeField] private Button lobbyNameButton;
    [SerializeField] private Button publicPrivateButton;
    [SerializeField] private Button maxPlayersButton;
    [SerializeField] private Button gameModeButton;
    [SerializeField] private TextMeshProUGUI lobbyNameText;
    [SerializeField] private TextMeshProUGUI publicPrivateText;
    [SerializeField] private TextMeshProUGUI maxPlayersText;
    [SerializeField] private TextMeshProUGUI gameModeText;
    [SerializeField] private TMP_InputField playerNameInputField;
    [SerializeField] private TMP_InputField lobbyNameInputField;


    private string lobbyName;
    private bool isPrivate;
    private int maxPlayers;

    private void Awake() {
        Instance = this;

        createButton.onClick.AddListener(() => {
            LobbyManager.Instance.CreateLobby(
                lobbyName,
                maxPlayers,
                isPrivate
            );
            Hide();
        });



        publicPrivateButton.onClick.AddListener(() => {
            isPrivate = !isPrivate;
            UpdateText();
        });

        Hide();
    }

    private void UpdateText() {
        lobbyNameText.text = lobbyName;
        publicPrivateText.text = isPrivate ? "Private" : "Public";
        maxPlayersText.text = maxPlayers.ToString();
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

    public void Show() {
        gameObject.SetActive(true);

        lobbyName = "MyLobby";
        isPrivate = false;
        maxPlayers = 4;

        UpdateText();
    }

}