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
        Cursor.lockState = CursorLockMode.Locked;
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
        humanObject.SetActive(humanSetting);
        flyObject.SetActive(!humanSetting);
    }
}