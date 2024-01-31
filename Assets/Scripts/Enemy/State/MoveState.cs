using UnityEngine;
using UnityEngine.AI;

namespace EnemyLogic
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class MoveState : EnemyState
    {
        private const string Run = "Run";
        private const float SpeedStart = 0.01f;
        private const float SpeedEnd = 0f;

        private NavMeshAgent _agent;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        private void OnEnable()
        {
            Animator.SetFloat(Run, SpeedStart);
            _agent.enabled = true;
        }

        private void OnDisable()
        {
            Animator.SetFloat(Run, SpeedEnd);
            _agent.enabled = false;
        }

        private void Update()
        {
            if (EnemyTarget != null)
            {
                _agent.SetDestination(Target.position);
            }
        }
    }
}