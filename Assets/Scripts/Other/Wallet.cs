using Agava.YandexGames;
using UnityEngine;
using UnityEngine.Events;

public class Wallet : MonoBehaviour
{
    private int _deltaMoney = 300;
    public static int Money { get; private set; }

    private static int _allMoneyReceived;

    public static int AllMoneyReceived => _allMoneyReceived;

    public static event UnityAction<int> MoneyChanged;

    private void Awake()
    {
        Money = _deltaMoney;
        MoneyChanged?.Invoke(Money);
    }

    private void Start()
    {
        MoneyChanged?.Invoke(Money);
    }

    public static void ChangeMoney(int money)
    {
        Money += money;
        _allMoneyReceived += money;
        MoneyChanged?.Invoke(Money);

#if UNITY_WEBGL && !UNITY_EDITOR
        if (PlayerAccount.IsAuthorized)
        {
            Leaderboard.SetScore("Coins", _allMoneyReceived);
        }
#endif
    }

    public static void InitGold(int gold, int allMoneyReceived)
    {
        Money = gold;
        _allMoneyReceived = allMoneyReceived;
        MoneyChanged?.Invoke(Money);
    }
}
