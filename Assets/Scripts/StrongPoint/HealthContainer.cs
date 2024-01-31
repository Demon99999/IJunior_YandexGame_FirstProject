using System;
using UI;
using UnityEngine;

namespace EnemyLogic
{
    public class HealthContainer : MonoBehaviour
    {
        [SerializeField] private int _health;
        [SerializeField] private VictoryScreen _victoryScreen;
        [SerializeField] private DefeatScreen _defeatScreen;

        private int _maxHealth;

        public event Action<int> HealthChanged;

        public event Action MaxHealthChanged;

        public event Action Died;

        public int Health => _health;

        public int MaxHealth => _maxHealth;

        private void Awake()
        {
            _maxHealth = _health;
        }

        private void OnEnable()
        {
            _victoryScreen.ResumeButtonClick += OnResetHealth;
            _victoryScreen.BonusButtonClick += OnResetHealth;
            _defeatScreen.RestartButtonClick += OnResetHealth;
            _defeatScreen.BonusButtonClick += OnResetHealth;
        }

        private void OnDisable()
        {
            _victoryScreen.ResumeButtonClick -= OnResetHealth;
            _victoryScreen.BonusButtonClick -= OnResetHealth;
            _defeatScreen.RestartButtonClick -= OnResetHealth;
            _defeatScreen.BonusButtonClick -= OnResetHealth;
        }

        public void TakeDamage(int damageAmount)
        {
            _health -= damageAmount;

            if (_health <= 0)
            {
                _health = 0;
                Died?.Invoke();
            }

            HealthChanged?.Invoke(_health);
        }

        public void OnResetHealth()
        {
            _health = _maxHealth;
            MaxHealthChanged?.Invoke();
        }
    }
}