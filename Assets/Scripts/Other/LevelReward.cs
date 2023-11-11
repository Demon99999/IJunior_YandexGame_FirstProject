using UnityEngine;
using UnityEngine.Events;

public class LevelReward : MonoBehaviour
{
    [SerializeField] private EnemyHandler _enemyHandler;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private YandexAds _yandexAds;

    private int _goldCount = 0;
    private int _goldForAdvertising = 250;
    private int _goldForAdvertisingForDefeat = 500;
    private int _doubleMultiplier = 2;

    public event UnityAction<int> GoldChanged;

    public int GoldCount => _goldCount;

    private void OnEnable()
    {
        _enemyHandler.AllEnemiesKilled += CalculateReward;
    }

    private void OnDisable()
    {
        _enemyHandler.AllEnemiesKilled -= CalculateReward;
    }

    public void CalculateReward()
    {
        _goldCount = _spawner.Level.GoldReward;
        GoldChanged?.Invoke(_goldCount);
    }

    public void ClaimReward()
    {
        Wallet.ChangeMoney(_goldCount);
        _goldCount = 0;
    }

    public void ClaimDoubleReward()
    {
        _yandexAds.ShowRewardAd();
        Wallet.ChangeMoney(_goldCount * _doubleMultiplier);
        _goldCount = 0;
    }

    public void ClaimGoldForAdvertising()
    {
        _yandexAds.ShowRewardAd();
        Wallet.ChangeMoney(_goldForAdvertising);
    }

    public void ClaimGoldForAdvertisingForDefeat()
    {
        Wallet.ChangeMoney(_goldForAdvertisingForDefeat);
    }
}
