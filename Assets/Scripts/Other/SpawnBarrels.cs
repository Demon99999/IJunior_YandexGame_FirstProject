using System.Collections.Generic;
using UI;
using UnityEngine;

namespace GameLogic
{
    public class SpawnBarrels : MonoBehaviour
    {
        private readonly List<ExplosionBarrel> _explosionBarrels = new List<ExplosionBarrel>();

        [SerializeField] private Transform[] _spawnPoints;
        [SerializeField] private ExplosionBarrel _template;
        [SerializeField] private VictoryScreen _victoryScreen;
        [SerializeField] private DefeatScreen _defeatScreen;

        private void OnEnable()
        {
            _victoryScreen.ResumeButtonClick += OnClear;
            _victoryScreen.BonusButtonClick += OnClear;
            _defeatScreen.BonusButtonClick += OnClear;
            _defeatScreen.RestartButtonClick += OnClear;
        }

        private void OnDisable()
        {
            _victoryScreen.ResumeButtonClick -= OnClear;
            _victoryScreen.BonusButtonClick -= OnClear;
            _defeatScreen.BonusButtonClick -= OnClear;
            _defeatScreen.RestartButtonClick -= OnClear;
        }

        public void Create()
        {
            for (int i = 0; i < _spawnPoints.Length; i++)
            {
                ExplosionBarrel barrel = Instantiate(_template, _spawnPoints[i]);
                _explosionBarrels.Add(barrel);
            }
        }

        public void OnClear()
        {
            foreach (var barrel in _explosionBarrels)
            {
                Destroy(barrel.gameObject);
            }

            _explosionBarrels.Clear();
        }
    }
}