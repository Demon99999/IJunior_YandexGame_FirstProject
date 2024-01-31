using System;
using EnemyLogic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class DefeatScreen : Screen
    {
        [SerializeField] private Button _bonusButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private HealthContainer _healthContainer;
        [SerializeField] private GameHandler _gameHandler;

        public event Action RestartButtonClick;

        public event Action BonusButtonClick;

        public event Action DestroyEnemies;

        private void OnEnable()
        {
            _healthContainer.Died += OpenDefeatScreen;

            _restartButton.onClick.AddListener(OnRestartButton);
            _bonusButton.onClick.AddListener(OnBonusButton);

            _gameHandler.OpenAfterFightDefeatClick += OnClose;
        }

        private void OnDisable()
        {
            _healthContainer.Died -= OpenDefeatScreen;

            _restartButton.onClick.RemoveListener(OnRestartButton);
            _bonusButton.onClick.RemoveListener(OnBonusButton);

            _gameHandler.OpenAfterFightDefeatClick -= OnClose;
        }

        private void OpenDefeatScreen()
        {
            DestroyEnemies?.Invoke();
            OpenScreen();
        }

        private void OnRestartButton()
        {
            RestartButtonClick?.Invoke();
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