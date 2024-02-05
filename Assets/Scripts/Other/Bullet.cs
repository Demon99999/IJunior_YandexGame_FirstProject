using EnemyLogic;
using UnityEngine;

namespace GameLogic
{
    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private int _force;
        [SerializeField] private float _radius;
        [SerializeField] private ParticleSystem _particlesBlood;
        [SerializeField] private ParticleSystem _particlesHits;

        private ParticleSystem _particlesSparks;
        private int _damage;
        private Rigidbody _rigidbody;
        private float _delayDestroy = 7f;
        private float _delayDestroyParticle = 1f;

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
            CheckHitCollider(other);
            Destroy(gameObject);
        }

        private void OnTriggerStay(Collider other)
        {
            if (_particlesHits != null)
            {
                _particlesHits.transform.position = transform.position;
                _particlesHits.Play();
            }

            var particle = Instantiate(_particlesHits);
            Destroy(gameObject);
            Destroy(particle.gameObject, _delayDestroyParticle);

            Collider[] colliders = Physics.OverlapSphere(transform.position, _radius);

            foreach (Collider collider in colliders)
            {
                CheckHitCollider(collider);
            }
        }

        private void CheckHitCollider(Collider collider)
        {
            if (collider.TryGetComponent(out EnemyCollision enemy))
            {
                ApplyDamageToEnemy(enemy);
            }

            if (collider.TryGetComponent(out ExplosionBarrel barrel))
            {
                barrel.Explode();
            }
        }

        public void Initialize(int damage, ParticleSystem particleSystem)
        {
            _damage = damage;
            _particlesSparks = particleSystem;
            Instantiate(_particlesSparks, transform);
            _particlesSparks.Play();
        }

        private void ApplyDamageToEnemy(EnemyCollision enemy)
        {
            if (enemy.IsAlive(_rigidbody, _damage, _force))
            {
                if (_particlesBlood != null)
                {
                    _particlesBlood.transform.position = transform.position;
                    _particlesBlood.Play();
                }

                var particle = Instantiate(_particlesBlood);
                Destroy(gameObject);
                Destroy(particle.gameObject, _delayDestroyParticle);
            }
        }
    }
}