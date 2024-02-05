using System;
using System.Collections;
using UnityEngine;

namespace EnemyLogic
{
    [RequireComponent(typeof(Rigidbody), typeof(Animator), typeof(Material))]
    public class DieState : EnemyState
    {
        private const string Die = "Die";

        [SerializeField] private float _fadeTime = 3.0f;
        [SerializeField] private ParticleSystem _particleDied;
        [SerializeField] private Material _material;

        private Material _enemyMaterial;
        private float _delayBeforeDeath = 0.5f;
        private float _startValue = 1.0f;
        private float _endValue = 0.0f;

        private Animator _animator;
        private Rigidbody _rigidbody;

        public event Action Died;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _rigidbody = GetComponent<Rigidbody>();
            _enemyMaterial = GetComponentInChildren<Renderer>().material;
        }

        public void DieEnemy(Rigidbody attachedBody, int force)
        {
            Vector3 impactDirection = (attachedBody.position - transform.position).normalized;
            _animator.SetTrigger(Die);
            _rigidbody.AddForce(impactDirection * force, ForceMode.Impulse);
            StartCoroutine(FadeOut());
            Died?.Invoke();
        }

        private IEnumerator FadeOut()
        {
            Color enemyColor = _material.color;
            float elapsedTime = 0.0f;

            while (elapsedTime < _fadeTime)
            {
                float alpha = Mathf.Lerp(_startValue, _endValue, elapsedTime / _fadeTime);
                enemyColor.a = alpha;
                _enemyMaterial.color = enemyColor;
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            Instantiate(_particleDied, transform.position, transform.rotation, transform);
            Destroy(gameObject, _delayBeforeDeath);
        }
    }
}