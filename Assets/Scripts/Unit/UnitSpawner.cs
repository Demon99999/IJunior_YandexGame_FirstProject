using System;
using System.Collections.Generic;
using GameLogic;
using UI;
using UnityEngine;

namespace UnitLogic
{
    public class UnitSpawner : MonoBehaviour
    {
        [SerializeField] private Unit _prefabSniper;
        [SerializeField] private Unit _prefabRifl;
        [SerializeField] private Unit _prefabBazuka;
        [SerializeField] private GridGenerator _gridGenerator;
        [SerializeField] private Wallet _wallet;
        [SerializeField] private BattleScreen _battleScreen;

        private int _percent = 100;
        private int _percentAgeIncrease = 10;

        private int _priceSniper;
        private int _priceRifl;
        private int _priceBazuka;
        private List<Cell> _cells;

        public event Action<int> PriseSniperChanged;

        public event Action<int> PriseRiflChanged;

        public event Action<int> PriseBazukaChanged;

        public int PriceSniper => _priceSniper;

        public int PriceRifl => _priceRifl;

        public int PriceBazuka => _priceBazuka;

        private void OnEnable()
        {
            _battleScreen.SniperButtonClick += OnSpawnSniper;
            _battleScreen.RiflButtonClick += OnSpawnRifl;
            _battleScreen.BazukaButtonClick += OnSpawnBazuka;
        }

        private void OnDisable()
        {
            _battleScreen.SniperButtonClick -= OnSpawnSniper;
            _battleScreen.RiflButtonClick -= OnSpawnRifl;
            _battleScreen.BazukaButtonClick -= OnSpawnBazuka;
        }

        private void Start()
        {
            PriseSniperChanged?.Invoke(_priceSniper);
            PriseRiflChanged?.Invoke(_priceRifl);
            PriseBazukaChanged?.Invoke(_priceBazuka);

            _cells = new List<Cell>();
            _cells = _gridGenerator.Cells;
        }

        public void InitPrise(int currentSniperPrise, int currentRiflPrise, int currentBazukaPrise)
        {
            _priceSniper = currentSniperPrise;
            _priceRifl = currentRiflPrise;
            _priceBazuka = currentBazukaPrise;

            PriseSniperChanged?.Invoke(_priceSniper);
            PriseRiflChanged?.Invoke(_priceRifl);
            PriseBazukaChanged?.Invoke(_priceBazuka);
        }

        public void Spawn(Unit unit)
        {
            Transform point = GetPoint();

            if (point != null)
            {
                Unit unitSpawn = null;
                unitSpawn = Instantiate(unit, point.position, Quaternion.identity);
                unitSpawn.transform.parent = point.transform;
            }
        }

        private void OnSpawnSniper()
        {
            SpawnUnit(ref _priceSniper, _prefabSniper);
            PriseSniperChanged?.Invoke(_priceSniper);
        }

        private void OnSpawnRifl()
        {
            SpawnUnit(ref _priceRifl, _prefabRifl);
            PriseRiflChanged?.Invoke(_priceRifl);
        }

        private void OnSpawnBazuka()
        {
            SpawnUnit(ref _priceBazuka, _prefabBazuka);
            PriseBazukaChanged?.Invoke(_priceBazuka);
        }

        private Transform GetPoint()
        {
            for (int i = 0; i < _cells.Count; i++)
            {
                if (_cells[i].IsEmployed == false)
                {
                    _cells[i].ChangeValue();
                    return _cells[i].transform;
                }
            }

            return null;
        }

        private void SpawnUnit(ref int prise, Unit unit)
        {
            if (_wallet.Money >= prise)
            {
                Spawn(unit);
                _wallet.RemoveMoney(-prise);
                RaisPrice(ref prise);
            }
        }

        private void RaisPrice(ref int prise)
        {
            prise += (prise / _percent) * _percentAgeIncrease;
        }
    }
}