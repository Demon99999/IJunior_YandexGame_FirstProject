using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private BattleScreen _buttleScreen;
    [SerializeField] private VictoryScreen _victoryScreen;
    [SerializeField] private FightScreen _fightScreen;
    [SerializeField] private DefeatScreen _defeatScreen;
    [SerializeField] private SettingMenuScreen _settingMenuScreen;

    private void OnEnable()
    {
        _buttleScreen.PlayButtonClick += OnStartGame;
        _buttleScreen.SettingButtonClick += OnSettingMenuScreen;

        _victoryScreen.BonusButtonClick += OnMenuAfterFightScreen;
        _victoryScreen.ResumeButtonClick += OnMenuAfterFightScreen;

        _defeatScreen.RestartButtonClick += OnMenuFightScreen;
        _defeatScreen.BonusButtonClick += OnMenuFightScreen;
    }

    private void OnDisable()
    {
        _buttleScreen.PlayButtonClick -= OnStartGame;
        _buttleScreen.SettingButtonClick -= OnSettingMenuScreen;

        _victoryScreen.BonusButtonClick -= OnMenuAfterFightScreen;
        _victoryScreen.ResumeButtonClick -= OnMenuAfterFightScreen;

        _defeatScreen.RestartButtonClick -= OnMenuFightScreen;
        _defeatScreen.BonusButtonClick -= OnMenuFightScreen;
    }

    private void OnStartGame()
    {
        _buttleScreen.CloseScreen();
        _fightScreen.OpenScreen();
    }

    private void OnMenuAfterFightScreen()
    {
        _victoryScreen.CloseScreen();
        _buttleScreen.OpenScreen();
    }

    private void OnMenuFightScreen()
    {
        _fightScreen.CloseScreen();
        _defeatScreen.CloseScreen();
        _buttleScreen.OpenScreen();
    }

    private void OnSettingMenuScreen()
    {
        _settingMenuScreen.OpenScreen();
    }
}
