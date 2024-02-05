using EnemyLogic;
using TMPro;
using UnityEngine;

namespace UI
{
    public class LevelScore : MonoBehaviour
    {
        [SerializeField] private Spawner _spawner;
        [SerializeField] private TMP_Text _score;

        private void OnEnable()
        {
            OnScoreChanged(_spawner.LevelIndex);
            _spawner.LevelChanged += OnScoreChanged;
        }

        private void OnDisable()
        {
            _spawner.LevelChanged -= OnScoreChanged;
        }

        private void OnScoreChanged(int score)
        {
            _score.text = score.ToString();
        }
    }
}