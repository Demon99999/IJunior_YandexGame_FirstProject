using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    [SerializeField] private EnemyHandler _enemyHandler;
    [SerializeField] private SceneNext _sceneManage;
    [SerializeField] private Transform _container;
    [SerializeField] private List<Level> _levels;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private StrongPoint _strongPoint;
    [SerializeField] private SpawnBarrels _spawnBarrels;

    private int _currentLevelIndex = 0;
    private int _levelIndex = 1;
    private Level _currentLevel;
    private EnemyCount[] _enemyCounts;

    public event UnityAction<int> LevelChanged;
    public event UnityAction LevelStarted;
    public event UnityAction CurrentLevelExceedsCount;

    public Level Level => _currentLevel;
    public int CurrentLevelIndex => _currentLevelIndex;
    public int LevelIndex => _levelIndex;

    private void OnEnable()
    {
        _enemyHandler.AllEnemiesKilled += NextLevel;
    }

    private void OnDisable()
    {
        _enemyHandler.AllEnemiesKilled -= NextLevel;
    }

    private void Start()
    {
        ShowLevel();
        StartLevel();
    }

    public void NextLevel()
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

    public void ShowLevel()
    {
        LevelChanged?.Invoke(_levelIndex);
    }

    public void SwitchAnotherMap()
    {
        if (_currentLevelIndex >= _levels.Count)
        {
            _sceneManage.NextScene();
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
                enemyCurrent.Init(_strongPoint, _strongPoint.GetPoint());
                _enemyHandler.AddEnemy(enemyCurrent);
                enemyCurrent = null;
                countEnemies--;
            }
        }
    }

    private Transform GetSpawnPoint()
    {
        int indexPoint = Random.Range(0, _spawnPoints.Length);

        if (_spawnPoints !=null)
        {
            return _spawnPoints[indexPoint];
        }
        else
        {
            return null;
        }
    }
}
