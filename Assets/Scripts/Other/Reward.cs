using System;
using EnemyLogic;
using UI;
using UnityEngine;

namespace GameLogic
{
    public class Reward : MonoBehaviour
    {
        [SerializeField] private EnemyHandler _enemyHandler;
        [SerializeField] private Spawner _spawner;
        [SerializeField] private Wallet _wallet;
        [SerializeField] private VictoryScreen _victoryScreen;
        [SerializeField] private DefeatScreen _defeatScreen;
        [SerializeField] private BattleScreen _battleScreen;

        private int _goldCount = 0;
        private int _goldForAdvertising = 50;
        private int _goldForAdvertisingForDefeat = 100;
        private int _doubleMultiplier = 2;

        public event Action<int> GoldChanged;

        public int GoldCount => _goldCount;

        private void OnEnable()
        {
            _enemyHandler.AllEnemiesKilled += OnCalculateReward;
            _victoryScreen.ResumeButtonClick += OnClaimReward;
            _victoryScreen.BonusButtonClick += OnClaimDoubleReward;
            _defeatScreen.BonusButtonClick += OnClaimGoldForAdvertisingForDefeat;
            _battleScreen.RewardButtonClick += OnClaimGoldForAdvertising;
        }

        private void OnDisable()
        {
            _enemyHandler.AllEnemiesKilled -= OnCalculateReward;
            _victoryScreen.ResumeButtonClick -= OnClaimReward;
            _victoryScreen.BonusButtonClick -= OnClaimDoubleReward;
            _defeatScreen.BonusButtonClick -= OnClaimGoldForAdvertisingForDefeat;
            _battleScreen.RewardButtonClick -= OnClaimGoldForAdvertising;
        }

        private void OnCalculateReward()
        {
            _goldCount = _spawner.Level.GoldReward;
            GoldChanged?.Invoke(_goldCount);
        }

        private void OnClaimReward()
        {
            AddGold(_goldCount);
        }

        private void OnClaimDoubleReward()
        {
            AddGold(_goldCount * _doubleMultiplier);
        }

        private void OnClaimGoldForAdvertising()
        {
            AddGold(_goldForAdvertising);
        }

        private void OnClaimGoldForAdvertisingForDefeat()
        {
            AddGold(_goldForAdvertisingForDefeat);
        }

        private void AddGold(int gold)
        {
            _wallet.AddMoney(gold);
            _goldCount = 0;
        }
    }
}