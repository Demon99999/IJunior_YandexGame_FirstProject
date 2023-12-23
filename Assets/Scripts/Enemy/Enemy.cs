using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator),typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyState _firstState;
    [SerializeField] private DieState _dieState;
    
    private EnemyState _currentState;
    private Animator _animator;
    private Rigidbody _rigidbody;
    private HealthContainer _healthContainer;
    private StrongPoint _targetPoint;
    private Transform _target;

    public event UnityAction<Enemy> Died;

    public EnemyState CurrentState => _currentState;
    public EnemyState DieState => _dieState;

    private void OnEnable()
    {
        _healthContainer.Died += OnEnemyDied;
    }

    private void OnDisable()
    {
        _healthContainer.Died -= OnEnemyDied;
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _healthContainer = GetComponentInChildren<HealthContainer>();
    }

    private void Start()
    {
        _currentState = _firstState;
        _currentState.Enter(_targetPoint, _animator, _rigidbody, _target);
        _dieState.Enter(_targetPoint, _animator, _rigidbody, _target);
    }

    private void Update()
    {
        foreach (var transition in _currentState.Transitions)
        {
            transition.enabled = true;
            transition.Init(_targetPoint, _target);
        }

        if (_currentState == null)
            return;

        EnemyState nextState = _currentState.GetNextState();

        if (nextState != null)
            Transit(nextState);

        if (_healthContainer.Health <= 0 && _currentState != _dieState)
        {
            Transit(_dieState);
        }
    }

    public void Init(StrongPoint targetPoint, Transform target)
    {
        _targetPoint = targetPoint;
        _target = target;
    }

    public void ApplayDamage(Rigidbody rigidbody, int damage, int force)
    {
        if (_currentState != _dieState)
        {
            _healthContainer.TakeDamage(damage);

            if (_healthContainer.Health <= 0)
            {
                Transit(_dieState);
                _dieState.ApplyDamage(rigidbody, force);
            }
        }
    }

    private void OnEnemyDied()
    {
        Died?.Invoke(this);
        enabled = false;
    }

    private void Transit(EnemyState nextState)
    {
        if (_currentState != null)
            _currentState.Exit();

        _currentState = nextState;

        if (_currentState != null)
            _currentState.Enter(_targetPoint, _animator, _rigidbody, _target);
    }
}
