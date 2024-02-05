using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace EnemyLogic
{
    [RequireComponent(typeof(HealthContainer))]
    public class EnemyTarget : MonoBehaviour
    {
        [SerializeField] private Transform[] _pointsAttack;

        private HealthContainer _healthContainer;

        public event Action Died;

        public event Action Damaged;

        public HealthContainer HealthContainer => _healthContainer;

        private void Awake()
        {
            _healthContainer = GetComponent<HealthContainer>();
        }

        private void OnEnable()
        {
            _healthContainer.Died += OnDied;
        }

        private void OnDisable()
        {
            _healthContainer.Died -= OnDied;
        }

        public bool IsAlive()
        {
            if (_healthContainer.Health <= 0)
            {
                return false;
            }

            return true;
        }

        public void ApplyDamage(float damage)
        {
            _healthContainer.TakeDamage((int)damage);
            Damaged?.Invoke();
        }

        public Transform GetPoint()
        {
            int indexPoint = Random.Range(0, _pointsAttack.Length);
            return _pointsAttack[indexPoint];
        }

        private void OnDied()
        {
            Died?.Invoke();
        }
    }
}