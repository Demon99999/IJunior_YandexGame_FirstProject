using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent),typeof(Unit))]
public class ShootState : UnitState
{
    private const string Shoots = "Shoot";
    private const string ShootBullet = "ShootGun";

    [SerializeField] private Bullet _bullet;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private Transform _partToRotate;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private ParticleSystem _particleSystem;

    private Unit _unit;
    private float _currentTime = 0;
    private AimCursor _cursor;
    private NavMeshAgent _navMeshAgent;

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _unit = GetComponent<Unit>();
        _navMeshAgent.enabled = false;
        Animator.Play(Shoots);
    }

    private void Update()
    {
        _currentTime += Time.deltaTime;

        Vector3 forward = _cursor.transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(forward);
        _partToRotate.rotation = Quaternion.Slerp(_partToRotate.rotation, lookRotation, Time.deltaTime * _rotationSpeed);

        if (_currentTime >= _unit.Card.AttackSpeed)
        {
            Shoot();
            _currentTime = 0;
        }
    }

    public void Init(AimCursor aimCursor)
    {
        _cursor = aimCursor;
    }

    private void Shoot()
    {
        Bullet bullet = Instantiate(_bullet, _shootPoint.position, _shootPoint.rotation);
        bullet.Initialize(_unit.Card.Damage, _unit.Card.ParticleSystem);
        _particleSystem.Play();
        Animator.SetTrigger(ShootBullet);
    }
}
