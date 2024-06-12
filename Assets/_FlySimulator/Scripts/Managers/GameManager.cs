using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : NetworkBehaviour
{
    public static GameManager Instance;
    public event Action OnRoundStart;
    public event Action OnRoundEnd;
    public NetworkVariable<int> HumanHealth { get; private set; } = new NetworkVariable<int>(startHumanHealth,
                                                                                            NetworkVariableReadPermission.Everyone,
                                                                                            NetworkVariableWritePermission.Server);
    public NetworkVariable<float> TimeLeft { get; private set; } = new NetworkVariable<float>(startHumanHealth,
                                                                                            NetworkVariableReadPermission.Everyone,
                                                                                            NetworkVariableWritePermission.Server);
    public string LocalName { get; private set; }
    
    [SerializeField] private static int playersToStartRound = 4;
    [SerializeField] private static int startHumanHealth = 100;
    [SerializeField] private static float roundTime = 120;  // seconds
    
    private List<GameObject> _flyPlayers;
    private GameObject _humanPlayer;
    private bool _canJoinRound;
    private GameManager.RoundMode roundState;

    private enum RoundMode
    {
        Menu,
        Lobby,
        Active,
        Restarting
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            _flyPlayers = new();
            _humanPlayer = null;
            _canJoinRound = true;
            roundState = RoundMode.Menu;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //adding listeners for other systems
        LobbyManager.Instance.OnJoinedLobby += JoinedLobby;
        LobbyManager.Instance.OnKickedFromLobby += LeftLobby;
        LobbyManager.Instance.OnLeftLobby += LeftLobby;
    }

    private void JoinedLobby(object sender, LobbyManager.LobbyEventArgs e)
    {
        roundState = RoundMode.Lobby;
    }

    private void LeftLobby(object sender, System.EventArgs e)
    {
        roundState = RoundMode.Menu;
    }

    private void StartRound()
    {
        HumanHealth.Value = startHumanHealth;
        roundState = RoundMode.Active;
        TimeLeft.Value = roundTime;
        OnRoundStart?.Invoke();
    }

    private void Update()
    {
        if (roundState == RoundMode.Active)
        {
            TimeLeft.Value = Math.Max(0, TimeLeft.Value - Time.deltaTime);
            if (TimeLeft.Value == 0)
            {
                HumanWinRound();
            }
        }
    }

    private void OnStartGame(object sender, LobbyManager.LobbyEventArgs e)
    {
        SceneManager.LoadScene("netcoding");
        _flyPlayers.Add(null);                  //FIND PLAYERS SOMEHOW
        _humanPlayer = null;                    //FIND PLAYERS SOMEHOW
    }

    private void FlyWinRound()
    {
        OnRoundEnd?.Invoke();
        Debug.Log("Flies win!");
    }

    private void HumanWinRound()
    {
        OnRoundEnd?.Invoke();
        Debug.Log("Human wins!");
    }

    IEnumerator RestartRoundAfterSeconds(float seconds)
    {
        if (roundState != RoundMode.Restarting)
        {
            roundState = RoundMode.Restarting;
            yield return new WaitForSeconds(seconds);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void DamageHuman(int damage)
    {
        if (roundState != RoundMode.Active)
        {
            return;
        }
        
        HumanHealth.Value = Math.Max(0, HumanHealth.Value - damage);
        if (HumanHealth.Value == 0)
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