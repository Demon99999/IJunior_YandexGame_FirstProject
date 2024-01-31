using System.Collections.Generic;
using UI;
using UnityEngine;

namespace GameLogic
{
    public class GridGenerator : MonoBehaviour
    {
        [SerializeField] private Vector2Int _gridSize;
        [SerializeField] private Cell _prefab;
        [SerializeField] private float _offset;
        [SerializeField] private Transform _container;
        [SerializeField] private VictoryScreen _victoryScreen;
        [SerializeField] private DefeatScreen _defeatScreen;
        [SerializeField] private BattleScreen _battleScreen;

        private List<Cell> _cells;

        public List<Cell> Cells => _cells;

        private void Awake()
        {
            _cells = new List<Cell>();
            Build();
        }

        private void OnEnable()
        {
            _victoryScreen.ResumeButtonClick += OnShowPoints;
            _victoryScreen.BonusButtonClick += OnShowPoints;
            _defeatScreen.RestartButtonClick += OnShowPoints;
            _defeatScreen.BonusButtonClick += OnShowPoints;
            _battleScreen.PlayButtonClick += OnHidePoints;
        }

        private void OnDisable()
        {
            _victoryScreen.ResumeButtonClick -= OnShowPoints;
            _victoryScreen.BonusButtonClick -= OnShowPoints;
            _defeatScreen.RestartButtonClick -= OnShowPoints;
            _defeatScreen.BonusButtonClick -= OnShowPoints;
            _battleScreen.PlayButtonClick -= OnHidePoints;
        }

        private void OnHidePoints()
        {
            foreach (var cell in _cells)
            {
                cell.Hide();
            }
        }

        private void OnShowPoints()
        {
            foreach (var cell in _cells)
            {
                cell.Show();
            }
        }

        [ContextMenu("Generate Grid")]
        private void Build()
        {
            var cellSize = _prefab.GetComponentInChildren<CellRenderer>().GetComponent<MeshRenderer>().bounds.size;

            for (int x = 0; x < _gridSize.x; x++)
            {
                for (int y = _gridSize.y; y > 0; y--)
                {
                    var positionCell = new Vector3(y * (cellSize.x + _offset), 0, x * (cellSize.z + _offset));
                    var cell = Instantiate(_prefab, positionCell, Quaternion.identity, _container);
                    _cells.Add(cell);
                }
            }
        }
    }
}