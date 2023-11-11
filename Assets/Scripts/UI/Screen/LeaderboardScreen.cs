using Agava.YandexGames;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using PlayerPrefs = UnityEngine.PlayerPrefs;

public class LeaderboardScreen : Screen
{
    private const string AnonymousEn = "Anonymous";
    private const string AnonymousRu = "Аноним";
    private const string AnonymousTr = "İsimsiz";

    [SerializeField] private Button _exitButton1;
    [SerializeField] private Button _exitButton2;
    [SerializeField] private Button _authorizationButton;
    [SerializeField] private GameObject _authorizationPanel;
    [SerializeField] private GameObject _leaderboardPanel;
    [SerializeField] private GameObject[] _players;
    [SerializeField] private TMP_Text[] _ranks;
    [SerializeField] private TMP_Text[] _leaderNames;
    [SerializeField] private TMP_Text[] _scoreList;
    [SerializeField] private string _leaderboardName = "Coins";

    private const string AllGold = "AllGold";
    private int _playerScore = 0;

    private void Start()
    {
        _playerScore = PlayerPrefs.GetInt(AllGold);
    }

    private void OnEnable()
    {
        _exitButton1.onClick.AddListener(OnExitButton);
        _exitButton2.onClick.AddListener(OnExitButton);
        _authorizationButton.onClick.AddListener(OpenLeaderboardPanel);
    }

    private void OnDisable()
    {
        _exitButton1.onClick.RemoveListener(OnExitButton);
        _exitButton2.onClick.RemoveListener(OnExitButton);
        _authorizationButton.onClick.RemoveListener(OpenLeaderboardPanel);
    }

    public void OnExitButton()
    {
        CloseScreen();
    }

    public void OpenAuthorizationPanel()
    {
        OpenScreen();
#if UNITY_WEBGL && !UNITY_EDITOR
        if (!PlayerAccount.IsAuthorized)
#endif
        {
            _authorizationPanel.SetActive(true);
            _leaderboardPanel.SetActive(false);
        }
#if UNITY_WEBGL && !UNITY_EDITOR
        else
        {
            OpenLeaderboardPanel();
        }
#endif
    }

    public void OpenYandexLeaderboard()
    {
        PlayerAccount.RequestPersonalProfileDataPermission();
        if (!PlayerAccount.IsAuthorized)
            PlayerAccount.Authorize();

        Leaderboard.GetEntries(_leaderboardName, (result) =>
        {
            int leadersNumber = result.entries.Length >= _leaderNames.Length ? _leaderNames.Length : result.entries.Length;

            for (int i = 0; i < leadersNumber; i++)
            {
                _players[i].SetActive(true);
                string name = result.entries[i].player.publicName;
                string anon = AnonymousEn;

                if (YandexGamesSdk.Environment.i18n.lang == "ru")
                {
                    anon = AnonymousRu;
                }
                else if (YandexGamesSdk.Environment.i18n.lang == "tr")
                {
                    anon = AnonymousTr;
                }

                if (string.IsNullOrEmpty(name))
                    name = anon;

                _leaderNames[i].text = name;
                _scoreList[i].text = result.entries[i].formattedScore;
                _ranks[i].text = result.entries[i].rank.ToString();
            }
        });
    }

    public void SetLeaderboardScore()
    {
        if (YandexGamesSdk.IsInitialized)
        {
            Leaderboard.GetPlayerEntry(_leaderboardName, OnSuccessCallback);
        }
    }

    private void OpenLeaderboardPanel()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        SetLeaderboardScore();
        OpenYandexLeaderboard();

        if (PlayerAccount.IsAuthorized)
#endif
        {
            _authorizationPanel.SetActive(false);
            _leaderboardPanel.SetActive(true);
        }
    }

    private void OnSuccessCallback(LeaderboardEntryResponse result)
    {
        if (result == null || _playerScore > result.score)
        {
            Leaderboard.SetScore(_leaderboardName, _playerScore);
        }
    }
}
