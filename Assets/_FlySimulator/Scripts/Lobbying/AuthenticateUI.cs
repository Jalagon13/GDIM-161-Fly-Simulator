using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Code from CodeMonkey Tutorial!!!! Project download.
public class AuthenticateUI : MonoBehaviour {


    [SerializeField] private Button authenticateButton;


    private void Awake() {
        authenticateButton.onClick.AddListener(() => {
            LobbyManager.Instance.Authenticate(EditPlayerName.Instance.GetPlayerName());
            Hide();
        });
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

}