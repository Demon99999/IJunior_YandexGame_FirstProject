using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private int _force;
    [SerializeField] private float _radius;
    [SerializeField] private ParticleSystem _particle;
    [SerializeField] private ParticleSystem _particleHit;

    private ParticleSystem _particleSystem;
    private int _damage;
    private Rigidbody _rigidbody;
    private float _delayDestroy = 7;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 direction = transform.forward;
        _rigidbody.velocity = direction.normalized * _speed;
        Destroy(gameObject, _delayDestroy);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out EnemyCollision enemy))
        {
            ApplyDamageToEnemy(enemy);
        }

        if (other.TryGetComponent(out ExplosionBarrel barrel))
        {
            barrel.Explode();
        }

        Destroy(gameObject);
    }

    public void Initialize(int damage, ParticleSystem system)
    {
        _damage = damage;
        _particleSystem = system;
        Instantiate(_particleSystem, transform);
        _particleSystem.Play();
    }

    private void ApplyDamageToEnemy(EnemyCollision enemy)
    {
        if (enemy.ApplayDamage(_rigidbody, _damage, _force))
        {
            if (_particle != null)
            {
                _particle.transform.position = transform.position;
                _particle.Play();
            }

            var particle = Instantiate(_particle);
            Destroy(gameObject);
            Destroy(particle.gameObject,1f);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //
        if (_particleHit != null)
        {
            _particleHit.transform.position = transform.position;
            _particleHit.Play();
        }

        var particle = Instantiate(_particleHit);
        Destroy(gameObject);
        Destroy(particle.gameObject, 1f);

        Collider[] colliders2 = Physics.OverlapSphere(transform.position, _radius);

        foreach (Collider collider in colliders2)
        {
            if (collider.TryGetComponent(out EnemyCollision enemyCollision))
            {
                ApplyDamageToEnemy(enemyCollision);
            }

            if (collider.TryGetComponent(out ExplosionBarrel explosionBarrel))
            {
                explosionBarrel.Explode();
            }
        }
        //

        //if (other.TryGetComponent(out EnemyCollision enemy))
        //{
        //    if (enemy != null)
        //    {
        //        Collider[] colliders = Physics.OverlapSphere(transform.position, _radius);

        //        foreach (Collider collider in colliders)
        //        {
        //            if (collider.TryGetComponent(out EnemyCollision enemyCollision))
        //            {
        //                ApplyDamageToEnemy(enemyCollision);
        //            }

        //            if (collider.TryGetComponent(out ExplosionBarrel explosionBarrel))
        //            {
        //                explosionBarrel.Explode();
        //            }
        //        }
        //    }
        //}
    }
}
