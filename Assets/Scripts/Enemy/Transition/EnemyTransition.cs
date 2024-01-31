using UnityEngine;

namespace EnemyLogic
{
    public class EnemyTransition : MonoBehaviour
    {
        [SerializeField] private EnemyState _targetState;

        public EnemyState TargetState => _targetState;

        public bool NeedTransit { get; protected set; }

        public Transform Target { get; protected set; }

        protected EnemyTarget EnemyTarget { get; private set; }

        protected virtual void OnEnable()
        {
            NeedTransit = false;
        }

        public void Init(EnemyTarget enemyTarget, Transform target)
        {
            EnemyTarget = enemyTarget;
            Target = target;
        }
    }
}