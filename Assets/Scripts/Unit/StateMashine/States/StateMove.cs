using UnityEngine;
using UnityEngine.AI;

namespace UnitLogic
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class StateMove : UnitState
    {
        private const string Run = "Run";

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
                _animator.Play(Run);
            }
        }
    }
}