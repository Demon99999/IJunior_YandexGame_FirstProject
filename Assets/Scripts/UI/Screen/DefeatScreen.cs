using Agava.YandexGames;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DefeatScreen : Screen
{
    [SerializeField] private Button _bonusButton;
    [SerializeField] private Button _restartButton;

    [SerializeField] private EnemyHandler _enemyHandler;
    [SerializeField] private HealthContainer _healthContainer;
    [SerializeField] private LevelReward _levelReward;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Squad _squad;
    [SerializeField] private CameraChanger _cameraChanger;
    [SerializeField] private GridGenerator _gridGenerator;
    [SerializeField] private AimCursor _aimCursor;
    [SerializeField] private SaveSystem _saveSystem;
    [SerializeField] private UnitSpawner _unitSpawner;
    [SerializeField] private SpawnBarrels _spawnBarrels;
    [SerializeField] private YandexAds _yandexAds;

    private int _counter = 0;
    private int numberOfRepetitions = 2;

    public event UnityAction RestartButtonClick;
    public event UnityAction BonusButtonClick;

    private void OnEnable()
    {
        _healthContainer.Died += OpenDefeatScreen;

        _restartButton.onClick.AddListener(OnRestartButton);
        _bonusButton.onClick.AddListener(OnBonusButton);

    }

    private void OnDisable()
    {
        _healthContainer.Died -= OpenDefeatScreen;

        _restartButton.onClick.RemoveListener(OnRestartButton);
        _bonusButton.onClick.RemoveListener(OnBonusButton);
    }

    private void OpenDefeatScreen()
    {
        _enemyHandler.OnDestroyEnemies();
        _aimCursor.CursorOn(false);
        OpenScreen();
    }

    private void OnRestartButton()
    {
        if (_counter >= numberOfRepetitions)
        {
            _yandexAds.ShowInterstitial();
            _counter = 0;
        }
        else
        {
            _counter++;
        }

        _saveSystem.Load();
        RestartButtonClick?.Invoke();
        OnMenuAfterFightScreen();
    }

    private void OnBonusButton()
    {
        _saveSystem.Load();
        VideoAd.Show(_yandexAds.OnAdOpen, _levelReward.ClaimGoldForAdvertisingForDefeat, OnBonusButtonCallback);
    }

    private void OnMenuAfterFightScreen()
    {
        _spawner.ShowLevel();
        _spawnBarrels.Clear();
        _enemyHandler.OnDestroyEnemies();
        _cameraChanger.OnSwitchCamera();
        _gridGenerator.ShowPoints();
        _squad.ReturnUnits();
        _saveSystem.LoadUnits();

        if (_spawner.ChecForMaximumLevel())
            _saveSystem.ResetLevel();

        _spawner.StartLevel();
        _healthContainer.ResetHealth();
    }

    private void OnBonusButtonCallback()
    {
        BonusButtonClick?.Invoke();
        OnMenuAfterFightScreen();
        _yandexAds.OnAdClose();
    }
}
