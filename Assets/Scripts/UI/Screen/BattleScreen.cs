using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BattleScreen : Screen
{
    [SerializeField] private Button _battleButton;
    [SerializeField] private Button _buySniperButton;
    [SerializeField] private Button _buyRiflButton;
    [SerializeField] private Button _buyBazukaButton;
    [SerializeField] private Button _settingButton;
    [SerializeField] private Button _advertisingButton;
    [SerializeField] private Button _leaderboardButton;

    [SerializeField] private TMP_Text _buySniperText;
    [SerializeField] private TMP_Text _buyRiflText;
    [SerializeField] private TMP_Text _buyBazukaText;
    [SerializeField] private TMP_Text _walletMoneyText;

    [SerializeField] private Squad _squad;
    [SerializeField] private CameraChanger _cameraChanger;
    [SerializeField] private GridGenerator _gridGenerator;
    [SerializeField] private UnitSpawner _unitSpawner;
    [SerializeField] private AimCursor _aimCursor;
    [SerializeField] private EnemyHandler _enemyHandler;
    [SerializeField] private SaveSystem _saveSystem;
    [SerializeField] private GroundAudio _groundAudio;
    [SerializeField] private LevelReward _levelReward;

    public event UnityAction PlayButtonClick;
    public event UnityAction SettingButtonClick;
    public event UnityAction LeaderboardButtonClick;

    private void OnEnable()
    {
        _battleButton.onClick.AddListener(OnPlayButtonButtle);
        _buySniperButton.onClick.AddListener(OnBuyButtonSniper);
        _buyRiflButton.onClick.AddListener(OnBuyButtonRifl);
        _buyBazukaButton.onClick.AddListener(OnBuyButtonBazuka);
        _settingButton.onClick.AddListener(OnSettingButton);
        _advertisingButton.onClick.AddListener(ClaimGoldForAdvertising);
        _leaderboardButton.onClick.AddListener(OnLeaderboardButton);

        _unitSpawner.PriseSniperChanged += OnPriseSniperChanged;
        _unitSpawner.PriseRiflChanged += OnPriseRiflChanged;
        _unitSpawner.PriseBazukaChanged += OnPriseBazukaChanged;

        Wallet.MoneyChanged += OnMoneyChanged;
    }

    private void OnDisable()
    {
        _battleButton.onClick.RemoveListener(OnPlayButtonButtle);
        _buySniperButton.onClick.RemoveListener(OnBuyButtonSniper);
        _buyRiflButton.onClick.RemoveListener(OnBuyButtonRifl);
        _buyBazukaButton.onClick.RemoveListener(OnBuyButtonBazuka);
        _settingButton.onClick.RemoveListener(OnSettingButton);
        _advertisingButton.onClick.RemoveListener(ClaimGoldForAdvertising);
        _leaderboardButton.onClick.RemoveListener(OnLeaderboardButton);

        _unitSpawner.PriseSniperChanged -= OnPriseSniperChanged;
        _unitSpawner.PriseRiflChanged -= OnPriseRiflChanged;
        _unitSpawner.PriseBazukaChanged -= OnPriseBazukaChanged;

        Wallet.MoneyChanged -= OnMoneyChanged;
    }

    private void Start()
    {
        OpenScreen();
        Time.timeScale = 1;
    }

    private void OnBuyButtonSniper()
    {
        _unitSpawner.SpawnSniper();
    }

    private void OnBuyButtonRifl()
    {
        _unitSpawner.SpawnRifl();
    }

    private void OnBuyButtonBazuka()
    {
        _unitSpawner.SpawnBazuka();
    }

    private void OnPlayButtonButtle()
    {
        PlayButtonClick?.Invoke();
        _cameraChanger.OnSwitchCamera();
        _squad.UnitsMove();
        _squad.HideUnitsLevel();
        _gridGenerator.HidePoints();
        _aimCursor.CursorOn(true);
        _enemyHandler.OnEnemies();
        _groundAudio.OnFightClip();
    }

    private void OnPriseSniperChanged(int prise)
    {
        OnValueChanged(prise,_buySniperText);
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
        OnValueChanged(money,_walletMoneyText);
    }

    private void OnValueChanged(int value,TMP_Text tmpText)
    {
        tmpText.text = value.ToString();
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
        _levelReward.ClaimGoldForAdvertising();
    }
}
