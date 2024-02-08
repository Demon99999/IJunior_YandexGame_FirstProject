using GameLogic;
using UnityEngine;

namespace UI
{
    public class RewardsScore : Score
    {
        [SerializeField] private Reward _reward;

        private void OnEnable()
        {
            OnScoreChanged(_reward.GoldCount);
            _reward.GoldChanged += OnScoreChanged;
        }

        private void OnDisable()
        {
            _reward.GoldChanged -= OnScoreChanged;
        }
    }
}