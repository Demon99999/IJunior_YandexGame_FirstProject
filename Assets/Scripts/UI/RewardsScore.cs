using GameLogic;
using TMPro;
using UnityEngine;

namespace UI
{
    public class RewardsScore : MonoBehaviour
    {
        [SerializeField] private Reward _reward;
        [SerializeField] private TMP_Text _gold;

        private void OnEnable()
        {
            _gold.text = _reward.GoldCount.ToString();
            _reward.GoldChanged += OnGoldChanged;
        }

        private void OnDisable()
        {
            _reward.GoldChanged -= OnGoldChanged;
        }

        private void OnGoldChanged(int score)
        {
            _gold.text = score.ToString();
        }
    }
}