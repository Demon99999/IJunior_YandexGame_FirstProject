using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody),typeof(Animator),typeof(Material))]
public class DieState : EnemyState
{
    private const string Die = "Die";

    [SerializeField] private float _fadeTime = 3.0f;
    [SerializeField] private ParticleSystem _particleDied;
    [SerializeField] private Material _material;

    private Material _enemyMaterial;
    private float _delayBeforeDeath = 0.5f;

    private Animator _animator;
    private Rigidbody _rigidbody;

    public event UnityAction Died;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _enemyMaterial = GetComponentInChildren<Renderer>().material;
    }

    public void ApplyDamage(Rigidbody attachedBody, int force)
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
        Vector3 position = transform.position;

        while (elapsedTime < _fadeTime)
        {
            float alpha = Mathf.Lerp(1.0f, 0.0f, elapsedTime / _fadeTime);
            enemyColor.a = alpha;
            _enemyMaterial.color = enemyColor;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Instantiate(_particleDied, transform.position, transform.rotation,transform);
        Destroy(gameObject,_delayBeforeDeath);
    }
}
