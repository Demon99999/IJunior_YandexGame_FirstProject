using System;
using UnityEngine;

namespace EnemyLogic
{
    [RequireComponent(typeof(HealthContainer))]
    public class EnemyCollision : MonoBehaviour, IDamageable
    {
        private Enemy _enemy;
        private HealthContainer _healthContainer;

        public event Action<EnemyCollision> Died;

        private void Awake()
        {
            _healthContainer = GetComponent<HealthContainer>();
            _enemy = GetComponentInParent<Enemy>();
        }

        private void OnEnable()
        {
            _healthContainer.Died += OnDied;
        }

        private void OnDisable()
        {
            _healthContainer.Died -= OnDied;
        }

        public bool IsAlive(Rigidbody rigidbody, int damage, int force)
        {
            if (_enemy.CurrentState != _enemy.DieState)
            {
                _enemy.ApplayDamage(rigidbody, damage, force);
                return true;
            }

            return false;
        }

        protected void OnDied()
        {
            enabled = false;
            Died?.Invoke(this);
        }
    }
}