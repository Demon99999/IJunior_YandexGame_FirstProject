using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class StateMove : UnitState
{
    private const string Run = "Run";

    [SerializeField] private float _moveSpeed;

    private NavMeshAgent _navMeshAgent;
    private Animator _animator;

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.enabled = true;
        _navMeshAgent.updateRotation = false;
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (Target != null)
        {
            _navMeshAgent.SetDestination(Target);
            transform.rotation = Quaternion.LookRotation(_navMeshAgent.velocity.normalized);
            _animator.Play(Run);
        }
    }
}
