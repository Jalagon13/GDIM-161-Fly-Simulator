using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyPlayer : MonoBehaviour
{
    void Start()
    {
        GameManager.Instance.JoinRound(OnRoundStart, OnRoundEnd, gameObject, false);
    }
    
    private void OnRoundStart()
    {
        
    }

    private void OnRoundEnd()
    {
        
    }
}