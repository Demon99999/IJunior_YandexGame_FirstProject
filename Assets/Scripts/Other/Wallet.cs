using System;
using UnityEngine;

namespace GameLogic
{
    public class Wallet : MonoBehaviour
    {
        private int _deltaMoney = 300;

        private int _allMoneyReceived;

        public int Money { get; private set; }

        public event Action<int> MoneyChanged;

        public int AllMoneyReceived => _allMoneyReceived;

        private void Awake()
        {
            Money = _deltaMoney;
            _allMoneyReceived = _deltaMoney;
            MoneyChanged?.Invoke(Money);
        }

        private void Start()
        {
            MoneyChanged?.Invoke(Money);
        }

        public void AddMoney(int money)
        {
            Money += money;
            _allMoneyReceived += money;
            MoneyChanged?.Invoke(Money);
        }

        public void RemoveMoney(int money)
        {
            Money += money;
            MoneyChanged?.Invoke(Money);
        }

        public void InitGold(int gold, int allMoneyReceived)
        {
            Money = gold;
            _allMoneyReceived = allMoneyReceived;
            MoneyChanged?.Invoke(Money);
        }
    }
}