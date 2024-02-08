using EnemyLogic;
using UnityEngine;

namespace UI
{
    public class LevelScore : Score 
    { 
        [SerializeField] private Spawner _spawner;

        private void OnEnable()
        {
            OnScoreChanged(_spawner.LevelIndex);
            _spawner.LevelChanged += OnScoreChanged;
        }

        private void OnDisable()
        {
            _spawner.LevelChanged -= OnScoreChanged;
        }
    }
}