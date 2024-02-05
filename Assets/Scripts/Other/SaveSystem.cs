using System;
using System.Collections.Generic;
using Agava.YandexGames;
using EnemyLogic;
using UI;
using UnitLogic;
using UnityEngine;
using PlayerPrefs = UnityEngine.PlayerPrefs;

namespace GameLogic
{
    public class SaveSystem : MonoBehaviour
    {
        private const string LeaderboardName = "Demon9000";
        private const string CurrentLevel = "CurrentLevel";
        private const string Level = "Level";
        private const string Gold = "Gold";
        private const string AllGold = "AllGold";
        private const string Map = "Map";
        private const string UnitId = "UnitId";
        private const string UnitCount = "UnitCount";
        private const string UnitGrade = "UnitGrade";
        private const string NameMetod = "LoadUnits";
        private const string PriseSniper = "PriseSniper";
        private const string PriseRifl = "PriseRifl";
        private const string PriseBazuka = "PriseBazuka";

        [SerializeField] private Squad _squad;
        [SerializeField] private UnitSpawner _unitSpawner;
        [SerializeField] private List<UnitCard> _unitCards;
        [SerializeField] private Spawner _spawner;
        [SerializeField] private SceneNext _sceneManage;
        [SerializeField] private Wallet _wallet;
        [SerializeField] private VictoryScreen _victoryScreen;
        [SerializeField] private DefeatScreen _defeatScreen;

        private int _initialLevel = 1;
        private int _initialMap = 2;
        private int _initialAmountGold = 300;
        private float _timeLoad = 0.5f;
        private int _startPriseSniper = 100;
        private int _startPriseRifl = 150;
        private int _startPriceBazuka = 200;

        public event Action SaveNotFound;

        private void Awake()
        {
            Load();
        }

        private void OnEnable()
        {
            _victoryScreen.ResumeButtonClick += OnMenuAfterFightVictory;
            _victoryScreen.BonusButtonClick += OnMenuAfterFightVictory;
            _defeatScreen.RestartButtonClick += OnMenuAfterFightDefeat;
            _defeatScreen.BonusButtonClick += OnMenuAfterFightDefeat;
        }

        private void OnDisable()
        {
            _victoryScreen.ResumeButtonClick -= OnMenuAfterFightVictory;
            _victoryScreen.BonusButtonClick -= OnMenuAfterFightVictory;
            _defeatScreen.RestartButtonClick -= OnMenuAfterFightDefeat;
            _defeatScreen.BonusButtonClick -= OnMenuAfterFightDefeat;
        }

        private void Start()
        {
            Invoke(NameMetod, _timeLoad);
        }

        public void Save()
        {
            PlayerPrefs.SetInt(CurrentLevel, _spawner.CurrentLevelIndex);
            PlayerPrefs.SetInt(Level, _spawner.LevelIndex);
            PlayerPrefs.SetInt(Gold, _wallet.Money);
            PlayerPrefs.SetInt(Map, _sceneManage.SceneIndex);
            PlayerPrefs.SetInt(AllGold, _wallet.AllMoneyReceived);
            PlayerPrefs.SetInt(PriseSniper, _unitSpawner.PriceSniper);
            PlayerPrefs.SetInt(PriseRifl, _unitSpawner.PriceRifl);
            PlayerPrefs.SetInt(PriseBazuka, _unitSpawner.PriceBazuka);
            SaveUnit();
        }

        public void OnMenuAfterFightVictory()
        {
            Save();
            SetScore();
            LoadUnits();

            if (_spawner.ChecForMaximumLevel())
            {
                ResetLevel();
                _spawner.SwitchAnotherMap();
            }

            _spawner.StartLevel();
        }

        public void OnMenuAfterFightDefeat()
        {
            Load();
            LoadUnits();

            if (_spawner.ChecForMaximumLevel())
            {
                ResetLevel();
            }

            _spawner.StartLevel();
        }

        public void Load()
        {
            if (PlayerPrefs.HasKey(Level))
            {
                if (_spawner != null)
                {
                    _spawner.InitCurrentLevel(PlayerPrefs.GetInt(CurrentLevel));
                    _spawner.InitLevel(PlayerPrefs.GetInt(Level));
                    _wallet.InitGold(PlayerPrefs.GetInt(Gold), PlayerPrefs.GetInt(AllGold));
                    _sceneManage.InitScene(PlayerPrefs.GetInt(Map));
                    _unitSpawner.InitPrise(PlayerPrefs.GetInt(PriseSniper), PlayerPrefs.GetInt(PriseRifl), PlayerPrefs.GetInt(PriseBazuka));
                }
            }
        }

        public void LoadScene()
        {
            if (PlayerPrefs.HasKey(Map))
            {
                _sceneManage.InitScene(PlayerPrefs.GetInt(Map));
                SaveNotFound?.Invoke();
            }
        }

        public void SetScore()
        {
            SaveLeaderboardScore(PlayerPrefs.GetInt(AllGold));
        }

        public void ResetSave()
        {
            PlayerPrefs.SetInt(CurrentLevel, 0);
            PlayerPrefs.SetInt(Level, _initialLevel);
            PlayerPrefs.SetInt(Gold, _initialAmountGold);
            PlayerPrefs.SetInt(Map, _initialMap);
            PlayerPrefs.SetInt(UnitCount, 0);
            PlayerPrefs.SetInt(PriseSniper, _startPriseSniper);
            PlayerPrefs.SetInt(PriseRifl, _startPriseRifl);
            PlayerPrefs.SetInt(PriseBazuka, _startPriceBazuka);
        }

        private void ResetLevel()
        {
            PlayerPrefs.SetInt(CurrentLevel, 0);
            PlayerPrefs.SetInt(Map, _sceneManage.SceneIndex);
        }

        private void SaveUnit()
        {
            if (_squad.IsPositive())
            {
                int unitCount = _squad.Units.Count;

                for (int i = 0; i < unitCount; i++)
                {
                    PlayerPrefs.SetInt(UnitId + i, _squad.Units[i].Card.Id);
                    PlayerPrefs.SetInt(UnitGrade + i, _squad.Units[i].Card.Grade);
                }

                PlayerPrefs.SetInt(UnitCount, unitCount);
            }
        }

        private void LoadUnits()
        {
            int unitCount = PlayerPrefs.GetInt(UnitCount);
            List<int> unitsId = new List<int>();
            List<int> unitsGrade = new List<int>();

            for (int i = 0; i < unitCount; i++)
            {
                unitsId.Add(PlayerPrefs.GetInt(UnitId + i));
                unitsGrade.Add(PlayerPrefs.GetInt(UnitGrade + i));
            }

            for (int i = 0; i < _unitCards.Count; i++)
            {
                for (int j = 0; j < unitsId.Count; j++)
                {
                    if (_unitCards[i].Id == unitsId[j])
                    {
                        _unitSpawner.Spawn(_unitCards[i].Template);
                    }
                }
            }

            unitsId.Clear();
        }

        private void SaveLeaderboardScore(int value)
        {
#if UNITY_WEBGL && !UNITY_EDITOR
        Leaderboard.GetPlayerEntry(LeaderboardName, response =>
        {
            Leaderboard.SetScore(LeaderboardName, value);
        });
#endif
        }
    }
}