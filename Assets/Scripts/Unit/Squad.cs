using System.Collections.Generic;
using GameLogic;
using UI;
using UnityEngine;

namespace UnitLogic
{
    public class Squad : MonoBehaviour
    {
        [SerializeField] private GridGenerator _gridGenerator;
        [SerializeField] private AimCursor _aimCursor;
        [SerializeField] private VictoryScreen _victoryScreen;
        [SerializeField] private DefeatScreen _defeatScreen;
        [SerializeField] private BattleScreen _battleScreen;

        private int _maxUnit = 5;
        private List<Unit> _units;
        private float _positionX = 1.5f;
        private float _positionY = 0f;
        private float _positionZ = 4f;
        private int _multiplierDistance = 2;

        public List<Unit> Units => _units;

        private void OnEnable()
        {
            _victoryScreen.ResumeButtonClick += OnReturnUnits;
            _victoryScreen.BonusButtonClick += OnReturnUnits;
            _defeatScreen.RestartButtonClick += OnReturnUnits;
            _defeatScreen.BonusButtonClick += OnReturnUnits;
            _battleScreen.PlayButtonClick += OnUnitsMove;
        }

        private void OnDisable()
        {
            _victoryScreen.ResumeButtonClick -= OnReturnUnits;
            _victoryScreen.BonusButtonClick -= OnReturnUnits;
            _defeatScreen.RestartButtonClick -= OnReturnUnits;
            _defeatScreen.BonusButtonClick -= OnReturnUnits;
            _battleScreen.PlayButtonClick -= OnUnitsMove;
        }

        public bool IsPositive()
        {
            _units = GetUnits(_gridGenerator.Cells);
            return _units.Count > 0;
        }

        private void OnReturnUnits()
        {
            DeleteUnits();
        }

        private void OnUnitsMove()
        {
            _units = GetUnits(_gridGenerator.Cells);
            DistributionMove(_maxUnit, _units);
            HideUnitsLevel();
        }

        private void HideUnitsLevel()
        {
            foreach (var unit in _units)
            {
                if (unit.TryGetComponent(out UnitLevel unitLevel))
                {
                    unitLevel.HideLevel();
                }
            }
        }

        private void DeleteUnits()
        {
            foreach (var unit in _units)
            {
                Destroy(unit.gameObject);
            }

            _units.Clear();
        }

        private List<Unit> GetUnits(List<Cell> cells)
        {
            List<Unit> units = new List<Unit>();

            foreach (var point in cells)
            {
                if (point.gameObject.GetComponentInChildren<Unit>())
                {
                    Unit unit = point.gameObject.GetComponentInChildren<Unit>();
                    unit.GetComponent<ShootState>().Init(_aimCursor);
                    units.Add(unit);
                }
            }

            return units;
        }

        private void DistributionMove(int count, List<Unit> units)
        {
            AddUnits(units.Count >= count ? count : units.Count, units);
        }

        private void AddUnits(int count, List<Unit> units)
        {
            List<Vector3> firingLine = GetPointPosition(count);

            for (int i = 0; i < count; i++)
            {
                units[i].Init(firingLine[i]);
                units[i].Move();
                units[i].GetComponent<Collider>().enabled = false;
            }
        }

        private List<Vector3> GetPointPosition(int count)
        {
            List<Vector3> points = new List<Vector3>();

            for (float i = 1; i < count * _multiplierDistance; i += _positionX)
            {
                points.Add(new Vector3(i, _positionY, _positionZ));
            }

            return points;
        }
    }
}