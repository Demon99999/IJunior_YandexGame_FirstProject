using UnityEngine;
using UnityEngine.AI;

namespace UnitLogic
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class IdleState : UnitState
    {
        private NavMeshAgent _navMeshAgent;

        private void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _navMeshAgent.enabled = false;
            _navMeshAgent.updateRotation = true;
        }
    }
}