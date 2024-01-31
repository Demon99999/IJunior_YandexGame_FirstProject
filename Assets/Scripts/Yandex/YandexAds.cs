using Agava.YandexGames;
using UI;
using UnityEngine;

namespace YandexSDK
{
    public class YandexAds : MonoBehaviour
    {
        [SerializeField] private VictoryScreen _victoryScreen;
        [SerializeField] private DefeatScreen _defeatScreen;
        [SerializeField] private BattleScreen _battleScreen;

        private void OnEnable()
        {
            _victoryScreen.BonusButtonClick += OnShowRewardAd;
            _defeatScreen.BonusButtonClick += OnShowRewardAd;
            _battleScreen.RewardButtonClick += OnShowRewardAd;
        }

        private void OnDisable()
        {
            _victoryScreen.BonusButtonClick -= OnShowRewardAd;
            _defeatScreen.BonusButtonClick -= OnShowRewardAd;
            _battleScreen.RewardButtonClick -= OnShowRewardAd;
        }

        public void ShowInterstitial()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
        InterstitialAd.Show(OnAdOpen, OnIterstitialAddClose);
#endif
        }

        private void OnShowRewardAd()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
        VideoAd.Show(OnAdOpen, OnAdClose);
#endif
        }

        private void OnAdOpen()
        {
            Time.timeScale = 0;
            AudioListener.volume = 0;
        }

        private void OnAdClose()
        {
            Time.timeScale = 1;
            AudioListener.volume = 1;
        }

        private void OnIterstitialAddClose(bool value)
        {
            Time.timeScale = 1;
            AudioListener.volume = 1;
        }
    }
}