using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Code from CodeMonkey Tutorial!!!! Project download.
//Modified for our game's purposes by Alaina Klaes.
public class AuthenticateUI : MonoBehaviour {


    [SerializeField] private Button authenticateButton;


    private void Start() {
        authenticateButton.onClick.AddListener(() => {
            if (LobbyManager.Instance == null)
            {
                Debug.Log("LobbyManager Instance null!");
            }
            if (EditPlayerName.Instance == null)
            {
                Debug.Log("EditPlayerName Instance null!");
            }

            Debug.Log($"EditPlayerName GetPlayerName == {EditPlayerName.Instance.GetPlayerName()}");
            LobbyManager.Instance.Authenticate(EditPlayerName.Instance.GetPlayerName());
        });
    }

}