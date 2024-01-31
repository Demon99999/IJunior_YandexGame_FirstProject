using System.Collections;
using UnityEngine;

namespace EnemyLogic
{
    public class AttackState : EnemyState
    {
        private const string Attack = "Attack";

        [SerializeField] private float _attackForce;
        [SerializeField] private float _attackDelay;

        private Coroutine _attackCoroutine;
        private WaitForSeconds _waitForSecounds;

        private void OnEnable()
        {
            if (enabled)
                StartAttack();
        }

        private void OnDisable()
        {
            if (_attackCoroutine != null)
            {
                StopCoroutine(_attackCoroutine);
            }
        }

        private void Start()
        {
            _waitForSecounds = new WaitForSeconds(_attackDelay);
        }

        private void StartAttack()
        {
            if (_attackCoroutine != null)
            {
                StopCoroutine(_attackCoroutine);
            }

            _attackCoroutine = StartCoroutine(AttackCoroutine());
        }

        private IEnumerator AttackCoroutine()
        {
            Animator.SetTrigger(Attack);
            EnemyTarget.ApplyDamage(_attackForce);
            yield return _waitForSecounds;

            if (EnemyTarget.IsAlive())
                StartAttack();
        }
    }
}