using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public event Action OnRoundStart;
    public event Action OnRoundEnd;
    public int HumanHealth { get; private set; }
    public float TimeLeft { get; private set; }
    public string LocalName { get; private set; }
    
    [SerializeField] private int playersToStartRound = 4;
    [SerializeField] private int startHumanHealth = 100;
    [SerializeField] private float roundTime = 120;  // seconds
    
    private List<GameObject> _flyPlayers;
    private GameObject _humanPlayer;
    private bool _canJoinRound;
    private bool _roundActive;
    private bool _restartingRound;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            _flyPlayers = new();
            _humanPlayer = null;
            _canJoinRound = true;
            _roundActive = false;
            _restartingRound = false;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void StartRound()
    {
        HumanHealth = startHumanHealth;
        _roundActive = true;
        TimeLeft = roundTime;
        OnRoundStart?.Invoke();
    }

    private void Update()
    {
        if (_roundActive)
        {
            TimeLeft = Math.Max(0, TimeLeft - Time.deltaTime);
            if (TimeLeft == 0)
            {
                HumanWinRound();
            }
        }
    }

    private void FlyWinRound()
    {
        _roundActive = false;
        OnRoundEnd?.Invoke();
        Debug.Log("Flies win!");
    }

    private void HumanWinRound()
    {
        _roundActive = false;
        OnRoundEnd?.Invoke();
        Debug.Log("Human wins!");
    }

    IEnumerator RestartRoundAfterSeconds(float seconds)
    {
        if (!_restartingRound)
        {
            _restartingRound = true;
            yield return new WaitForSeconds(seconds);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void DamageHuman(int damage)
    {
        if (!_roundActive)
        {
            return;
        }
        
        HumanHealth = Math.Max(0, HumanHealth - damage);
        if (HumanHealth == 0)
        {
            FlyWinRound();
        }
    }

    public void JoinRound(Action roundStartCallback, Action roundEndCallback, GameObject player, bool human)
    {
        if (!_canJoinRound)
        {
            return;
        }

        if (human)
        {
            _humanPlayer = player;
        }
        else
        {
            _flyPlayers.Add(player);
        }

        OnRoundStart += roundStartCallback;
        OnRoundEnd += roundEndCallback;
        if (OnRoundStart.GetInvocationList().Length == playersToStartRound)
        {
            _canJoinRound = false;
            StartRound();
        }
    }
}