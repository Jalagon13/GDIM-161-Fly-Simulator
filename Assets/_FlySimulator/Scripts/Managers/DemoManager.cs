using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoManager : MonoBehaviour
{
    [SerializeField] private GameObject humanObject;
    [SerializeField] private GameObject flyObject;
    // Start is called before the first frame update
    private void Start()
    {
        Cursor.visible = false;
        humanObject.SetActive(true);
        flyObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            TogglePlayerMode();
        }
    }

    public void TogglePlayerMode()
    {
        bool humanSetting;
        humanSetting = (humanObject.activeSelf == false) ? true : false;
        Cursor.lockState = (humanSetting) ? CursorLockMode.Locked : CursorLockMode.None;
        humanObject.SetActive(humanSetting);
        Cursor.visible = !humanSetting;
        flyObject.SetActive(!humanSetting);
    }
}