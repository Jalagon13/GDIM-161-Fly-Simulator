using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Services.Lobbies.Models;
using UnityEngine.UI;

//Code from CodeMonkey Tutorial!!!! Project download.
//Modified for our game's purposes by Alaina Klaes.
public class LobbyPlayerSingleUI : MonoBehaviour {


    [SerializeField] private TextMeshProUGUI playerNameText;
    [SerializeField] private GameObject humanObject;
    [SerializeField] private GameObject flyObject;
    [SerializeField] private Button kickPlayerButton;


    private Player player;


    private void Awake() {
        kickPlayerButton.onClick.AddListener(KickPlayer);
    }

    public void SetKickPlayerButtonVisible(bool visible) {
        kickPlayerButton.gameObject.SetActive(visible);
    }

    public void UpdatePlayer(Player player) {
        this.player = player;
        playerNameText.text = player.Data[LobbyManager.KEY_PLAYER_NAME].Value;
        LobbyManager.PlayerCharacter playerCharacter = 
            System.Enum.Parse<LobbyManager.PlayerCharacter>(player.Data[LobbyManager.KEY_PLAYER_CHARACTER].Value);
        if (playerCharacter == LobbyManager.PlayerCharacter.Human)
        {
            Debug.Log("Human should be visible");
            humanObject.SetActive(true);
        }
        else
        {
            Debug.Log("Fly should be visible");
            flyObject.SetActive(true);
        }
    }

    private void KickPlayer() {
        if (player != null) {
            LobbyManager.Instance.KickPlayer(player.Id);
        }
    }


}