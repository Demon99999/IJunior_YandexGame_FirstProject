using System.Collections;
using UnityEngine;

public class AttackState : EnemyState
{
    private const string Attack = "Attack";

    [SerializeField] private float _attackForce;
    [SerializeField] private float _attackDelay;

    private Coroutine _attackCoroutine;

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
        var waitForSecounds = new WaitForSeconds(_attackDelay);
        StrongPoint.ApplyDamage(_attackForce);
        yield return waitForSecounds;

        if (StrongPoint.IsAlive())
            StartAttack();
    }
}
