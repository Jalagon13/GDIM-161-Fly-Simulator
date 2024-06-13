using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Services.Authentication;
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
    public NetworkVariable<int> HumanRoundsWon { get; private set; } = new NetworkVariable<int>(0,
                                                                                            NetworkVariableReadPermission.Everyone,
                                                                                            NetworkVariableWritePermission.Server);
    public NetworkVariable<int> FlyRoundsWon { get; private set; } = new NetworkVariable<int>(0,
                                                                                            NetworkVariableReadPermission.Everyone,
                                                                                            NetworkVariableWritePermission.Server);
    public NetworkVariable<float> TimeLeft { get; private set; } = new NetworkVariable<float>(startHumanHealth,
                                                                                            NetworkVariableReadPermission.Everyone,
                                                                                            NetworkVariableWritePermission.Server);
    public string LocalName { get; private set; }
    
    [SerializeField] private static int playersToStartRound = 4;
    [SerializeField] private static int startHumanHealth = 100;
    [SerializeField] private static float roundTime = 120;  // seconds
    [SerializeField] private static float timeBetweenRounds = 10; // seconds
    
    [SerializeField] private List<GameObject> _flyPlayers;
    [SerializeField] private GameObject _humanPlayer;
    private bool _canJoinGame;
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
            _canJoinGame = true;
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
        LobbyManager.Instance.StartGame += OnStartGame;
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
        if (IsHost)
        {
            Debug.Log("I AM THE HOST! I am adding references of everyone's player objects to my GameManager");
            //StartCoroutine(WaitForEveryoneToConnect_ThenAddThemToGameManagerReferences());
        }
    }

    private IEnumerator WaitForEveryoneToConnect_ThenAddThemToGameManagerReferences()
    {
        //Starts round after everyone is in and added to the lists.
        while (NetworkManager.Singleton.ConnectedClients.Count < LobbyManager.MAXIMUM_LOBBY_PLAYERS)
        {
            yield return new WaitForSeconds(0.5f);
        }
        foreach (KeyValuePair<ulong, NetworkClient> clientPair in NetworkManager.Singleton.ConnectedClients)
        {
            NetworkClient client = clientPair.Value;
            if (client.ClientId == NetworkManager.LocalClientId)
            {
                _humanPlayer = client.PlayerObject.gameObject;
                Debug.Log("Adding Human.");
            }
            else
            {
                _flyPlayers.Add(client.PlayerObject.gameObject);
                Debug.Log("Adding Fly.");
            }
        }
        StartNewGame();
    }

    private void StartNewGame()
    {
        HumanRoundsWon.Value = 0;
        FlyRoundsWon.Value = 0;
        StartRound();
    }

    private void FlyWinRound()
    {
        OnRoundEnd?.Invoke();
        Debug.Log("Flies win!");
        if (FlyRoundsWon.Value == 3)
        {
            //display Victory UI, activate button for starting a new game, and a button for Quit to Menu
        }
        else
        {
            RestartRoundAfterSeconds(timeBetweenRounds);
        }
    }

    private void HumanWinRound()
    {
        OnRoundEnd?.Invoke();
        Debug.Log("Human wins!");
        if (HumanRoundsWon.Value == 3)
        {
            //display Victory UI, activate button for starting a new game, and a button for Quit to Menu
        }
        else
        {
            RestartRoundAfterSeconds(timeBetweenRounds);
        }
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

    public void JoinGame(Action roundStartCallback, Action roundEndCallback, GameObject player, bool human)
    {
        if (!_canJoinGame)
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
            _canJoinGame = false;
            StartNewGame();
        }
    }
}