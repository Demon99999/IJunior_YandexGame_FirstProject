using Agava.YandexGames;
using UnityEngine;

public class YandexAds : MonoBehaviour
{
    public void ShowInterstitial()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        InterstitialAd.Show(OnAdOpen, OnIterstitialAddClose);
#endif
    }

    public void ShowRewardAd()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        VideoAd.Show(OnAdOpen, OnAdClose);
#endif
    }

    public void OnAdOpen()
    {
        Time.timeScale = 0;
        AudioListener.volume = 0;
    }

    public void OnAdClose()
    {
        Time.timeScale = 1;
        AudioListener.volume = 1;
    }

    public void OnIterstitialAddClose(bool value)
    {
        Time.timeScale = 1;
        AudioListener.volume = 1;
    }
}
