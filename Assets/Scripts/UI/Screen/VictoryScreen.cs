using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class VictoryScreen : Screen
{
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _bonusButton;

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
    [SerializeField] private YandexAds _yandexAds;

    private int _counter = 0;
    private int numberOfRepetitions = 2;

    public event UnityAction ResumeButtonClick;
    public event UnityAction BonusButtonClick;

    private void OnEnable()
    {
        _enemyHandler.AllEnemiesKilled += OpenScreen;

        _resumeButton.onClick.AddListener(OnResumeButton);
        _bonusButton.onClick.AddListener(OnBonusButton);
    }

    private void OnDisable()
    {
        _enemyHandler.AllEnemiesKilled -= OpenScreen;

        _resumeButton.onClick.RemoveListener(OnResumeButton);
        _bonusButton.onClick.RemoveListener(OnBonusButton);
    }

    private void OnResumeButton()
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

        ResumeButtonClick?.Invoke();
        _levelReward.ClaimReward();
        OnMenuAfterFightScreen();
    }

    private void OnBonusButton()
    {
        _levelReward.ClaimDoubleReward();
        BonusButtonClick?.Invoke();
        OnMenuAfterFightScreen();
    }

    private void OnMenuAfterFightScreen()
    {
        _spawner.ShowLevel();
        _enemyHandler.OnDestroyEnemies();
        _cameraChanger.OnSwitchCamera();
        _gridGenerator.ShowPoints();
        _aimCursor.CursorOn(false);
        _squad.ReturnUnits();
        _unitSpawner.ResetPrise();
        _saveSystem.Save();

        if (_spawner.ChecForMaximumLevel())
        {
            _saveSystem.ResetLevel();
            _spawner.SwitchAnotherMap();
        }
        
        _spawner.StartLevel();
        _healthContainer.ResetHealth();
    }
}
