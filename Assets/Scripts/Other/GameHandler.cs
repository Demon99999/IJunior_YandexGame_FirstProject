using System;
using UnityEngine;

namespace UI
{
    public class GameHandler : MonoBehaviour
    {
        [SerializeField] private BattleScreen _battleScreen;
        [SerializeField] private VictoryScreen _victoryScreen;
        [SerializeField] private FightScreen _fightScreen;
        [SerializeField] private DefeatScreen _defeatScreen;
        [SerializeField] private SettingMenuScreen _settingMenuScreen;
        [SerializeField] private LeaderboardScreen _leaderboardScreen;

        public event Action StartGameClick;

        public event Action OpenAfterFightVictoryClick;

        public event Action OpenAfterFightDefeatClick;

        public event Action OpenSettingMenu;

        public event Action OpenLeaderboard;

        private void OnEnable()
        {
            _battleScreen.PlayButtonClick += OnStartGame;
            _battleScreen.SettingButtonClick += OnSettingMenuScreen;
            _battleScreen.LeaderboardButtonClick += OnOpenLeaderboardScreen;

            _victoryScreen.BonusButtonClick += OnMenuAfterFightScreen;
            _victoryScreen.ResumeButtonClick += OnMenuAfterFightScreen;

            _defeatScreen.RestartButtonClick += OnMenuFightScreen;
            _defeatScreen.BonusButtonClick += OnMenuFightScreen;
        }

        private void OnDisable()
        {
            _battleScreen.PlayButtonClick -= OnStartGame;
            _battleScreen.SettingButtonClick -= OnSettingMenuScreen;
            _battleScreen.LeaderboardButtonClick -= OnOpenLeaderboardScreen;

            _victoryScreen.BonusButtonClick -= OnMenuAfterFightScreen;
            _victoryScreen.ResumeButtonClick -= OnMenuAfterFightScreen;

            _defeatScreen.RestartButtonClick -= OnMenuFightScreen;
            _defeatScreen.BonusButtonClick -= OnMenuFightScreen;
        }

        private void OnStartGame()
        {
            StartGameClick?.Invoke();
        }

        private void OnMenuAfterFightScreen()
        {
            OpenAfterFightVictoryClick?.Invoke();
        }

        private void OnMenuFightScreen()
        {
            OpenAfterFightDefeatClick?.Invoke();
        }

        private void OnSettingMenuScreen()
        {
            OpenSettingMenu?.Invoke();
        }

        private void OnOpenLeaderboardScreen()
        {
            OpenLeaderboard?.Invoke();
        }
    }
}