using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using Unity.Services.Authentication;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    private void FixedUpdate()
    {
        UIUpdate();
    }

    private void Start()
    {
        //adding listeners for other systems
        LobbyManager.Instance.OnJoinedLobby += JoinedLobby;
        LobbyManager.Instance.OnKickedFromLobby += LeftLobby;
        LobbyManager.Instance.OnLeftLobby += LeftLobby;
        LobbyManager.Instance.StartGame += OnStartGame;


        //ADD UI EVENTS
        OnRoundStart += ActivateDuringRoundUI;
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
        if (NetworkManager.IsHost)
        {
            Debug.Log("I AM THE HOST! I am adding references of everyone's player objects to my GameManager");
            StartCoroutine(WaitForEveryoneToConnect_ThenAddThemToGameManagerReferences());
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

        //play countdown sound and wait for it to stop, before starting new game
        GameMusicManager.Instance.PlayCountdownAndStart();
        yield return new WaitForSeconds(3.5f);

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
            ActivateGameEndUI(false);
            //display Victory UI, activate button for starting a new game, and a button for Quit to Menu
        }
        else
        {
            ActivateRoundEndUI(false);
            RestartRoundAfterSeconds(timeBetweenRounds);
        }
    }

    private void HumanWinRound()
    {
        OnRoundEnd?.Invoke();
        Debug.Log("Human wins!");
        if (HumanRoundsWon.Value == 3)
        {
            ActivateGameEndUI(true);
            //display Victory UI, activate button for starting a new game, and a button for Quit to Menu
        }
        else
        {
            ActivateRoundEndUI(false);
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


    //HANDLE ALL UI HERE
    [SerializeField] private GameObject SharedUI;
    
    [SerializeField] private GameObject duringRoundUI;
    [SerializeField] private Slider humanHealthMeter;
    [SerializeField] private TextMeshProUGUI timerText;

    [SerializeField] private GameObject roundEndUI;
    [SerializeField] private Image roundEndBG;
    [SerializeField] private TextMeshProUGUI roundWinTeamTextUI;
    [SerializeField] private TextMeshProUGUI roundFlyScoresTextUI;
    [SerializeField] private TextMeshProUGUI roundHumanScoresTextUI;

    [SerializeField] private GameObject gameEndUI;
    [SerializeField] private TextMeshProUGUI gameWinTeamTextUI;
    [SerializeField] private TextMeshProUGUI gameFlyScoresTextUI;
    [SerializeField] private TextMeshProUGUI gameHumanScoresTextUI;

    [SerializeField] private GameObject DamageOverlay;

    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button playAgainButton;
    [SerializeField] private Button quitGameButton;

    private void UIUpdate()
    {
        if (roundState == RoundMode.Active)
        {
            humanHealthMeter.value = HumanHealth.Value;
            timerText.text = timeToTimerText(TimeLeft.Value);
        }
    }

    private string timeToTimerText(float time)
    {
        string timerText = "";
        timerText += Math.Floor(time / 60);
        timerText += ":" + Math.Floor(time % 60);
        return timerText;
    }

    private void ActivateDuringRoundUI()
    {
        duringRoundUI.SetActive(true);
        humanHealthMeter.value = HumanHealth.Value;
        timerText.text = timeToTimerText(TimeLeft.Value);
    }

    private void ActivateRoundEndUI(bool humanWin)
    {
        roundEndUI.SetActive(true);
        roundFlyScoresTextUI.text = $"Flies: {FlyRoundsWon.Value}";
        roundHumanScoresTextUI.text = $"Human: {HumanRoundsWon.Value}";
        if (humanWin)
        {
            GameMusicManager.Instance.PlayTimerEndAndCarScreech();
            roundEndBG.color = new Color(173, 82, 0);
            roundWinTeamTextUI.text = "Human Wins!";
        }
        else
        {
            roundEndBG.color = new Color(87, 140, 25);
            roundWinTeamTextUI.text = "Flies Win!";
        }
    }

    private void ActivateGameEndUI(bool humanWin)
    {
        gameEndUI.SetActive(true);
        gameFlyScoresTextUI.text = $"Flies: {FlyRoundsWon.Value}";
        gameHumanScoresTextUI.text = $"Human: {HumanRoundsWon.Value}";
        gameWinTeamTextUI.text = humanWin? "Human Wins!" : "Flies Win!";
    }
}