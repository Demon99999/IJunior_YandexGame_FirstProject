using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class BattleScreen : Screen
    {
        [SerializeField] private Button _battleButton;
        [SerializeField] private Button _buySniperButton;
        [SerializeField] private Button _buyRiflButton;
        [SerializeField] private Button _buyBazukaButton;
        [SerializeField] private Button _settingButton;
        [SerializeField] private Button _advertisingButton;
        [SerializeField] private Button _leaderboardButton;

        [SerializeField] private GameHandler _gameHandler;

        public event Action PlayButtonClick;

        public event Action SniperButtonClick;

        public event Action RiflButtonClick;

        public event Action BazukaButtonClick;

        public event Action SettingButtonClick;

        public event Action LeaderboardButtonClick;

        public event Action RewardButtonClick;

        private void OnEnable()
        {
            _battleButton.onClick.AddListener(OnPlayButtonBattle);
            _buySniperButton.onClick.AddListener(OnBuyButtonSniper);
            _buyRiflButton.onClick.AddListener(OnBuyButtonRifl);
            _buyBazukaButton.onClick.AddListener(OnBuyButtonBazuka);
            _settingButton.onClick.AddListener(OnSettingButton);
            _advertisingButton.onClick.AddListener(ClaimGoldForAdvertising);
            _leaderboardButton.onClick.AddListener(OnLeaderboardButton);

            _gameHandler.StartGameClick += OnCloseScreen;
            _gameHandler.OpenAfterFightVictoryClick += OnOpenScreen;
            _gameHandler.OpenAfterFightDefeatClick += OnOpenScreen;
        }

        private void OnDisable()
        {
            _battleButton.onClick.RemoveListener(OnPlayButtonBattle);
            _buySniperButton.onClick.RemoveListener(OnBuyButtonSniper);
            _buyRiflButton.onClick.RemoveListener(OnBuyButtonRifl);
            _buyBazukaButton.onClick.RemoveListener(OnBuyButtonBazuka);
            _settingButton.onClick.RemoveListener(OnSettingButton);
            _advertisingButton.onClick.RemoveListener(ClaimGoldForAdvertising);
            _leaderboardButton.onClick.RemoveListener(OnLeaderboardButton);

            _gameHandler.StartGameClick -= OnCloseScreen;
            _gameHandler.OpenAfterFightVictoryClick -= OnOpenScreen;
            _gameHandler.OpenAfterFightDefeatClick -= OnOpenScreen;
        }

        private void Start()
        {
            OpenScreen();
        }

        private void OnBuyButtonSniper()
        {
            SniperButtonClick?.Invoke();
        }

        private void OnBuyButtonRifl()
        {
            RiflButtonClick?.Invoke();
        }

        private void OnBuyButtonBazuka()
        {
            BazukaButtonClick?.Invoke();
        }

        private void OnPlayButtonBattle()
        {
            PlayButtonClick?.Invoke();
        }

        private void OnSettingButton()
        {
            SettingButtonClick?.Invoke();
        }

        private void OnLeaderboardButton()
        {
            LeaderboardButtonClick?.Invoke();
        }

        private void ClaimGoldForAdvertising()
        {
            RewardButtonClick?.Invoke();
        }

        private void OnOpenScreen()
        {
            OpenScreen();
        }

        private void OnCloseScreen()
        {
            CloseScreen();
        }
    }
}