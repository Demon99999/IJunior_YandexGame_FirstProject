using GameLogic;
using TMPro;
using UnitLogic;
using UnityEngine;

namespace UI
{
    public class BattlePrise : MonoBehaviour
    {
        [SerializeField] private TMP_Text _buySniperText;
        [SerializeField] private TMP_Text _buyRiflText;
        [SerializeField] private TMP_Text _buyBazukaText;
        [SerializeField] private TMP_Text _walletMoneyText;

        [SerializeField] private UnitSpawner _unitSpawner;
        [SerializeField] private Wallet _wallet;

        private void OnEnable()
        {
            _unitSpawner.PriseSniperChanged += OnPriseSniperChanged;
            _unitSpawner.PriseRiflChanged += OnPriseRiflChanged;
            _unitSpawner.PriseBazukaChanged += OnPriseBazukaChanged;

            _wallet.MoneyChanged += OnMoneyChanged;
        }

        private void OnDisable()
        {
            _unitSpawner.PriseSniperChanged -= OnPriseSniperChanged;
            _unitSpawner.PriseRiflChanged -= OnPriseRiflChanged;
            _unitSpawner.PriseBazukaChanged -= OnPriseBazukaChanged;

            _wallet.MoneyChanged -= OnMoneyChanged;
        }

        private void OnPriseSniperChanged(int prise)
        {
            OnValueChanged(prise, _buySniperText);
        }

        private void OnPriseRiflChanged(int prise)
        {
            OnValueChanged(prise, _buyRiflText);
        }

        private void OnPriseBazukaChanged(int prise)
        {
            OnValueChanged(prise, _buyBazukaText);
        }

        private void OnMoneyChanged(int money)
        {
            OnValueChanged(money, _walletMoneyText);
        }

        private void OnValueChanged(int value, TMP_Text tmpText)
        {
            tmpText.text = value.ToString();
        }
    }
}