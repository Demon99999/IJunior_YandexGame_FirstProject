using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ExplosionBarrel : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _radius;
    [SerializeField] private int _forse;
    [SerializeField] private ParticleSystem _particle;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Explode()
    {
        var particle = Instantiate(_particle,transform.position,Quaternion.identity);

        Collider[] colliders = Physics.OverlapSphere(transform.position, _radius);

        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent(out EnemyCollision enemy))
            {
                Rigidbody rb = enemy.GetComponent<Rigidbody>();

                if (rb !=null)
                {
                    rb.AddExplosionForce(_forse,transform.position,_radius);
                }

                enemy.ApplayDamage(_rigidbody, _damage, _forse);
            }
        }

        Destroy(gameObject);
        Destroy(particle.gameObject,1f);
    }
}
