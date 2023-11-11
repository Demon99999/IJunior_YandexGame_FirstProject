using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHandler : MonoBehaviour
{
    private List<Enemy> _enemies;
    
    public event UnityAction AllEnemiesKilled;
    public event UnityAction EnemiesIncluded;
    public event UnityAction EnemiesRemoved;

    private void Awake()
    {
        _enemies = new List<Enemy>();
    }

    public void AddEnemy(Enemy enemy)
    {
        _enemies.Add(enemy);
        enemy.Died += OnEnemyDeath;
        enemy.enabled = false;
    }

    public void OnEnemyDeath(Enemy enemy)
    {
        _enemies.Remove(enemy);
        enemy.Died -= OnEnemyDeath;

        if (_enemies.Count <= 0)
        {
            AllEnemiesKilled?.Invoke();
        }
    }

    public void OnDestroyEnemies()
    {
        foreach (var enemy in _enemies)
        {
            Destroy(enemy.gameObject);
        }

        _enemies.Clear();
        EnemiesRemoved?.Invoke();
    }

    public void OnEnemies()
    {
        foreach (var enemy in _enemies)
        {
            enemy.enabled = true;
        }

        EnemiesIncluded?.Invoke();
    }
}
