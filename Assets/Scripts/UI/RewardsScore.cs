using GameLogic;
using TMPro;
using UnityEngine;

namespace UI
{
    public class RewardsScore : MonoBehaviour
    {
        [SerializeField] private LevelReward _levelReward;
        [SerializeField] private TMP_Text _gold;

        private void OnEnable()
        {
            _gold.text = _levelReward.GoldCount.ToString();
            _levelReward.GoldChanged += OnGoldChanged;
        }

        private void OnDisable()
        {
            _levelReward.GoldChanged -= OnGoldChanged;
        }

        private void OnGoldChanged(int score)
        {
            _gold.text = score.ToString();
        }
    }
}