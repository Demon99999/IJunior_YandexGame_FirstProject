using System;
using System.Collections.Generic;
using UI;
using UnityEngine;

namespace EnemyLogic
{
    public class EnemyHandler : MonoBehaviour
    {
        [SerializeField] private VictoryScreen _victoryScreen;
        [SerializeField] private DefeatScreen _defeatScreen;
        [SerializeField] private BattleScreen _battleScreen;

        private List<Enemy> _enemies;

        public event Action AllEnemiesKilled;

        public event Action EnemiesIncluded;

        public event Action EnemiesRemoved;

        private void Awake()
        {
            _enemies = new List<Enemy>();
        }

        private void OnEnable()
        {
            _victoryScreen.ResumeButtonClick += OnDestroyEnemies;
            _victoryScreen.BonusButtonClick += OnDestroyEnemies;
            _defeatScreen.DestroyEnemies += OnDestroyEnemies;
            _battleScreen.PlayButtonClick += OnTurnOnEnemies;
        }

        private void OnDisable()
        {
            _victoryScreen.ResumeButtonClick -= OnDestroyEnemies;
            _victoryScreen.BonusButtonClick -= OnDestroyEnemies;
            _defeatScreen.DestroyEnemies -= OnDestroyEnemies;
            _battleScreen.PlayButtonClick -= OnTurnOnEnemies;
        }

        public void AddEnemy(Enemy enemy)
        {
            _enemies.Add(enemy);
            enemy.Died += OnEnemyDeath;
            enemy.enabled = false;
        }

        private void OnEnemyDeath(Enemy enemy)
        {
            _enemies.Remove(enemy);
            enemy.Died -= OnEnemyDeath;

            if (_enemies.Count <= 0)
            {
                AllEnemiesKilled?.Invoke();
            }
        }

        private void OnDestroyEnemies()
        {
            foreach (var enemy in _enemies)
            {
                Destroy(enemy.gameObject);
            }

            _enemies.Clear();
            EnemiesRemoved?.Invoke();
        }

        private void OnTurnOnEnemies()
        {
            foreach (var enemy in _enemies)
            {
                enemy.enabled = true;
            }

            EnemiesIncluded?.Invoke();
        }
    }
}