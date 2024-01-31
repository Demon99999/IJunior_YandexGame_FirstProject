using System;
using EnemyLogic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class VictoryScreen : Screen
    {
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _bonusButton;
        [SerializeField] private EnemyHandler _enemyHandler;
        [SerializeField] private GameHandler _gameHandler;

        public event Action ResumeButtonClick;

        public event Action BonusButtonClick;

        private void OnEnable()
        {
            _enemyHandler.AllEnemiesKilled += OpenVictoryScreen;

            _resumeButton.onClick.AddListener(OnResumeButton);
            _bonusButton.onClick.AddListener(OnBonusButton);

            _gameHandler.OpenAfterFightVictoryClick += OnClose;
        }

        private void OnDisable()
        {
            _enemyHandler.AllEnemiesKilled -= OpenVictoryScreen;

            _resumeButton.onClick.RemoveListener(OnResumeButton);
            _bonusButton.onClick.RemoveListener(OnBonusButton);

            _gameHandler.OpenAfterFightVictoryClick -= OnClose;
        }

        private void OpenVictoryScreen()
        {
            OpenScreen();
        }

        private void OnResumeButton()
        {
            ResumeButtonClick?.Invoke();
        }

        private void OnBonusButton()
        {
            BonusButtonClick?.Invoke();
        }

        private void OnClose()
        {
            CloseScreen();
        }
    }
}