using EnemyLogic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class FightScreen : Screen
    {
        [SerializeField] private HealthContainer _healthContainer;
        [SerializeField] private Slider _slider;
        [SerializeField] private EnemyHandler _enemyHandler;
        [SerializeField] private GameHandler _gameHandler;

        private void OnEnable()
        {
            _enemyHandler.AllEnemiesKilled += CloseScreen;

            _healthContainer.MaxHealthChanged += OnMaxHealthChanged;
            _healthContainer.HealthChanged += OnChangeHealth;

            _gameHandler.StartGameClick += OnOpen;
            _gameHandler.OpenAfterFightDefeatClick += OnClose;
        }

        private void OnDisable()
        {
            _enemyHandler.AllEnemiesKilled -= CloseScreen;

            _healthContainer.HealthChanged -= OnChangeHealth;
            _healthContainer.MaxHealthChanged -= OnMaxHealthChanged;

            _gameHandler.StartGameClick -= OnOpen;
            _gameHandler.OpenAfterFightDefeatClick -= OnClose;
        }

        private void Start()
        {
            _slider.maxValue = _healthContainer.MaxHealth;
            _slider.value = _slider.maxValue;
        }

        public void OnMaxHealthChanged()
        {
            _slider.value = _slider.maxValue;
        }

        private void OnChangeHealth(int health)
        {
            _slider.value = health;
        }

        private void OnOpen()
        {
            OpenScreen();
        }

        private void OnClose()
        {
            CloseScreen();
        }
    }
}