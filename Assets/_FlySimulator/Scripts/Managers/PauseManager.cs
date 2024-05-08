using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseOverlay;


    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleOverlay();
        }
    }

    public void ToggleOverlay()
    {
        pauseOverlay.SetActive(!pauseOverlay.activeSelf);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}