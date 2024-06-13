using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanPlayer : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.JoinGame(OnRoundStart, OnRoundEnd, gameObject, true);
    }

    private void OnRoundStart()
    {
        
    }

    private void OnRoundEnd()
    {
        
    }
}
