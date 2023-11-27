using UnityEngine;
using UnityEngine.Events;
using Agava.YandexGames;
using PlayerPrefs = UnityEngine.PlayerPrefs;

public class SaveSystem : MonoBehaviour
{
    private const string LeaderboardName = "ScoreCoinDemon";
    private const string CurrentLevel = "CurrentLevel";
    private const string Level = "Level";
    private const string Gold = "Gold";
    private const string AllGold = "AllGold";
    private const string Map = "Map";

    [SerializeField] private Spawner _spawner;
    [SerializeField] private SceneNext _sceneManage;

    private int _initialLevel = 1;
    private int _initialMap = 2;
    private int _initialAmountGold = 300;

    public event UnityAction SaveNotFound;

    private void Awake()
    {
        Load();
    }

    public void Save()
    {
        PlayerPrefs.SetInt(CurrentLevel, _spawner.CurrentLevelIndex);
        PlayerPrefs.SetInt(Level, _spawner.LevelIndex);
        PlayerPrefs.SetInt(Gold, Wallet.Money);
        PlayerPrefs.SetInt(Map, _sceneManage.SceneIndex);
        PlayerPrefs.SetInt(AllGold, Wallet.AllMoneyReceived);
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey(Level))
        {
            if (_spawner != null)
            {
                _spawner.InitCurrentLevel(PlayerPrefs.GetInt(CurrentLevel));
                _spawner.InitLevel(PlayerPrefs.GetInt(Level));
                Wallet.InitGold(PlayerPrefs.GetInt(Gold), PlayerPrefs.GetInt(AllGold));
                _sceneManage.InitScene(PlayerPrefs.GetInt(Map));
            }
        }
    }

    public void LoadScene()
    {
        if (PlayerPrefs.HasKey(Map))
        {
            _sceneManage.InitScene(PlayerPrefs.GetInt(Map));
            SaveNotFound?.Invoke();
        }
    }

    public void ResetLevel()
    {
        PlayerPrefs.SetInt(CurrentLevel, 0);
        PlayerPrefs.SetInt(Map, _sceneManage.SceneIndex);
    }

    public void ResetSave()
    {
        PlayerPrefs.SetInt(CurrentLevel, 0);
        PlayerPrefs.SetInt(Level, _initialLevel);
        PlayerPrefs.SetInt(Gold, _initialAmountGold);
        PlayerPrefs.SetInt(Map, _initialMap);
    }

    public void SetScore()
    {
        SaveLeaderboardScore(PlayerPrefs.GetInt(AllGold));
    }

    private void SaveLeaderboardScore(int value)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        Leaderboard.GetPlayerEntry(LeaderboardName, response =>
        {
            Leaderboard.SetScore(LeaderboardName, value);
        });
#endif
    }
}
