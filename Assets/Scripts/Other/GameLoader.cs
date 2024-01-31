using Agava.YandexGames;
using GameLogic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GameLoader : MonoBehaviour
    {
        private const string Map = "Map";

        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _newPlayButton;
        [SerializeField] private Button _settingButton;
        [SerializeField] private Button _leaderboardButton;

        [SerializeField] private SaveSystem _saveSystem;
        [SerializeField] private SettingMenuScreen _settingMenuScreen;
        [SerializeField] private LeaderboardScreen _leaderboardScreen;
        [SerializeField] private SceneNext _sceneNext;

        private void Awake()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            YandexGamesSdk.GameReady();
#endif

            _saveSystem.LoadScene();

            if (UnityEngine.PlayerPrefs.HasKey(Map))
            {
                _continueButton.gameObject.SetActive(true);
            }
            else
            {
                _continueButton.gameObject.SetActive(false);
            }
        }

        private void OnEnable()
        {
            _saveSystem.SaveNotFound += OffContinueButton;
            _settingMenuScreen.ExitButtonClick += CloseSettingMenuScreen;
            _settingButton.onClick.AddListener(OpenSettingMenuScreen);
            _continueButton.onClick.AddListener(OpenPlayMenu);
            _leaderboardButton.onClick.AddListener(OpenLeaderboardScreen);
            _newPlayButton.onClick.AddListener(ResetPlay);
        }

        private void OnDisable()
        {
            _saveSystem.SaveNotFound -= OffContinueButton;
            _settingMenuScreen.ExitButtonClick -= CloseSettingMenuScreen;
            _settingButton.onClick.RemoveListener(OpenSettingMenuScreen);
            _leaderboardButton.onClick.RemoveListener(OpenLeaderboardScreen);
            _continueButton.onClick.RemoveListener(OpenPlayMenu);
            _newPlayButton.onClick.RemoveListener(ResetPlay);
        }

        private void OffContinueButton()
        {
            _continueButton.gameObject.SetActive(true);
        }

        private void OpenPlayMenu()
        {
            _sceneNext.OpenScene();
        }

        private void OpenSettingMenuScreen()
        {
            _settingMenuScreen.OpenScreen();
        }

        private void CloseSettingMenuScreen()
        {
            _settingMenuScreen.CloseScreen();
        }

        private void ResetPlay()
        {
            _saveSystem.ResetSave();
            _saveSystem.LoadScene();
            _sceneNext.OpenScene();
        }

        private void OpenLeaderboardScreen()
        {
            _leaderboardScreen.Open();
        }
    }
}