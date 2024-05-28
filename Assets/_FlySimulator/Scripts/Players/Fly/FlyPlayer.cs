using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyPlayer : MonoBehaviour
{
    [SerializeField] private int damage = 10;
    
    private void Start()
    {
        GameManager.Instance.JoinRound(OnRoundStart, OnRoundEnd, this.gameObject, true);
    }

    private void OnRoundStart()
    {
        
    }

    private void OnRoundEnd()
    {
        
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameManager.Instance.DamageHuman(damage);
        }
    }
}