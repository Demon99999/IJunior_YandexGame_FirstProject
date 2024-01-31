using System;
using System.Collections.Generic;
using GameLogic;
using UI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace EnemyLogic
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private EnemyHandler _enemyHandler;
        [SerializeField] private SceneNext _sceneManage;
        [SerializeField] private Transform _container;
        [SerializeField] private List<Level> _levels;
        [SerializeField] private Transform[] _spawnPoints;
        [SerializeField] private EnemyTarget _enemyTarget;
        [SerializeField] private SpawnBarrels _spawnBarrels;
        [SerializeField] private VictoryScreen _victoryScreen;
        [SerializeField] private DefeatScreen _defeatScreen;

        private int _currentLevelIndex = 0;
        private int _levelIndex = 1;
        private Level _currentLevel;
        private EnemyCount[] _enemyCounts;

        public event Action<int> LevelChanged;

        public event Action LevelStarted;

        public event Action CurrentLevelExceedsCount;

        public Level Level => _currentLevel;

        public int CurrentLevelIndex => _currentLevelIndex;

        public int LevelIndex => _levelIndex;

        private void OnEnable()
        {
            _enemyHandler.AllEnemiesKilled += OnNextLevel;
            _victoryScreen.ResumeButtonClick += OnShowLevel;
            _victoryScreen.BonusButtonClick += OnShowLevel;
            _defeatScreen.RestartButtonClick += OnShowLevel;
            _defeatScreen.BonusButtonClick += OnShowLevel;
        }

        private void OnDisable()
        {
            _enemyHandler.AllEnemiesKilled -= OnNextLevel;
            _victoryScreen.ResumeButtonClick -= OnShowLevel;
            _victoryScreen.BonusButtonClick -= OnShowLevel;
            _defeatScreen.RestartButtonClick -= OnShowLevel;
            _defeatScreen.BonusButtonClick -= OnShowLevel;
        }

        private void Start()
        {
            OnShowLevel();
            StartLevel();
        }

        public void OnNextLevel()
        {
            if (_currentLevelIndex < _levels.Count)
            {
                _currentLevelIndex++;
                _levelIndex++;
                LevelStarted?.Invoke();
            }
        }

        public void StartLevel()
        {
            if (_currentLevelIndex >= _levels.Count)
            {
                return;
            }

            _currentLevel = _levels[_currentLevelIndex];
            _enemyCounts = _currentLevel.EnemyCounts;

            SpawnEnemies();
            _spawnBarrels.Create();
        }

        public void OnShowLevel()
        {
            LevelChanged?.Invoke(_levelIndex);
        }

        public void SwitchAnotherMap()
        {
            if (_currentLevelIndex >= _levels.Count)
            {
                _sceneManage.OpenAnotherScene();
            }
        }

        public bool ChecForMaximumLevel()
        {
            if (_currentLevelIndex >= _levels.Count)
            {
                _sceneManage.ShowScene();
                CurrentLevelExceedsCount?.Invoke();
                return true;
            }

            return false;
        }

        public void InitCurrentLevel(int currentLevel)
        {
            _currentLevelIndex = currentLevel;
        }

        public void InitLevel(int currentLevel)
        {
            _levelIndex = currentLevel;
        }

        private void SpawnEnemies()
        {
            foreach (var enemy in _enemyCounts)
            {
                Transform spawnPoint;
                Enemy enemyCurrent = null;
                int countEnemies = enemy.Count;

                while (countEnemies > 0)
                {
                    spawnPoint = GetSpawnPoint();
                    enemyCurrent = Instantiate(enemy.Enemy, spawnPoint.position, Quaternion.identity, _container);
                    enemyCurrent.Init(_enemyTarget, _enemyTarget.GetPoint());
                    _enemyHandler.AddEnemy(enemyCurrent);
                    enemyCurrent = null;
                    countEnemies--;
                }
            }
        }

        private Transform GetSpawnPoint()
        {
            int indexPoint = Random.Range(0, _spawnPoints.Length);

            if (_spawnPoints != null)
            {
                return _spawnPoints[indexPoint];
            }
            else
            {
                return null;
            }
        }
    }
}