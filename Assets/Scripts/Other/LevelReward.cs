using UnityEngine;
using UnityEngine.Events;

public class LevelReward : MonoBehaviour
{
    [SerializeField] private EnemyHandler _enemyHandler;
    [SerializeField] private Spawner _spawner;
    
    private int _goldCount = 0;
    private int _goldForAdvertising = 50;
    private int _goldForAdvertisingForDefeat = 100;
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
        Wallet.AddMoney(_goldCount);
        _goldCount = 0;
    }

    public void ClaimDoubleReward()
    {
        Wallet.AddMoney(_goldCount * _doubleMultiplier);
        _goldCount = 0;
    }

    public void ClaimGoldForAdvertising()
    {
        Wallet.AddMoney(_goldForAdvertising);
    }

    public void ClaimGoldForAdvertisingForDefeat()
    {
        Wallet.AddMoney(_goldForAdvertisingForDefeat);
    }
}
