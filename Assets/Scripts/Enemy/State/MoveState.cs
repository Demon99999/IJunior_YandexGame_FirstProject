using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MoveState : EnemyState
{
    private const string Run = "Run";

    private NavMeshAgent _agent;
    private Vector3 _target;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        Animator.SetFloat(Run, 0.01f);
        _agent.enabled = true;
    }

    private void OnDisable()
    {
        Animator.SetFloat(Run, 0);
        _agent.enabled = false;
    }

    private void Update()
    {
        if (StrongPoint != null)
        {
            _agent.SetDestination(Target.position);
        }
    }
}
