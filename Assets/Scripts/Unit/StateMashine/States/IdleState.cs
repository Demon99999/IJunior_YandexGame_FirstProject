using EnemyLogic;
using UnityEngine;
using UnityEngine.AI;

namespace UnitLogic
{
    public class IdleState : UnitState
    {
        [SerializeField] private EnemyHandler _enemyHandler;

        private NavMeshAgent _navMeshAgent;

        private void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _navMeshAgent.enabled = false;
            _navMeshAgent.updateRotation = true;
        }
    }
}