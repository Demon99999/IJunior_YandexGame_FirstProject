using System;
using Agava.YandexGames;
using GameLogic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LeaderboardPanel : Screen
    {
        private const int MinPlayerCount = 1;
        private const int MaxPlayerCount = 5;
        private const string LeaderboardName = "Demon9000";

        [SerializeField] private Button _closeButton;
        [SerializeField] private LeaderboardView _leaderboardView;
        [SerializeField] private SaveSystem _saveSystem;

        public event Action Closed;

        private void OnEnable()
        {
            _closeButton.onClick.AddListener(OnClose);
        }

        private void OnDisable()
        {
            _closeButton.onClick.RemoveListener(OnClose);
        }

        private void Start()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
        PlayerAccount.RequestPersonalProfileDataPermission();
        LoadEntries();
#endif
        }

        public void Init()
        {
            OpenScreen();
            _saveSystem.SetScore();
            ClearViews();
            LoadEntries();
        }

        private void LoadEntries()
        {
            Leaderboard.GetEntries(LeaderboardName, (result) =>
            {
                var results = result.entries.Length;
                results = Mathf.Clamp(results, MinPlayerCount, MaxPlayerCount);

                for (int i = 0; i < results; i++)
                {
                    _leaderboardView.Create(result.entries[i]);
                }
            });
        }

        private void ClearViews()
        {
            _leaderboardView.Clear();
        }

        private void OnClose()
        {
            CloseScreen();
            Closed?.Invoke();
        }
    }
}