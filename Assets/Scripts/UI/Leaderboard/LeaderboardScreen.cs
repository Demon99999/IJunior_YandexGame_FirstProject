using Agava.YandexGames;
using UnityEngine;

namespace UI
{
    public class LeaderboardScreen : Screen
    {
        [SerializeField] private LoginPanel _logInPanel;
        [SerializeField] private LeaderboardPanel _leaderboardPanel;
        [SerializeField] private GameHandler _gameHandler;

        private void OnEnable()
        {
            _leaderboardPanel.Closed += OnLeaderboardClose;
            _logInPanel.Decline += OnLeaderboardClose;
            _logInPanel.Accept += OnLeaderboardClose;

            _gameHandler.OpenLeaderboard += OnOpen;
        }

        private void OnDisable()
        {
            _leaderboardPanel.Closed -= OnLeaderboardClose;
            _logInPanel.Decline -= OnLeaderboardClose;
            _logInPanel.Accept -= OnLeaderboardClose;

            _gameHandler.OpenLeaderboard -= OnOpen;
        }

        public void Open()
        {
            OpenScreen();
#if UNITY_WEBGL && !UNITY_EDITOR
        if (!PlayerAccount.IsAuthorized)
#endif
            {
                _logInPanel.OpenScreen();
                _leaderboardPanel.CloseScreen();
            }
#if UNITY_WEBGL && !UNITY_EDITOR
        else
        {
            if (!PlayerAccount.IsAuthorized)
            {
                PlayerAccount.Authorize();
            }
            
            _logInPanel.CloseScreen();

            if (PlayerAccount.IsAuthorized)
            {
                PlayerAccount.RequestPersonalProfileDataPermission();
                _leaderboardPanel.Init();
            }

            if (PlayerAccount.IsAuthorized == false)
            {
                return;
            }
        }
#endif
        }

        private void OnOpen()
        {
            OpenScreen();
        }

        private void OnLeaderboardClose()
        {
            CloseScreen();
        }
    }
}