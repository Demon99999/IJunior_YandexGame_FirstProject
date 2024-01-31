using UnityEngine;

namespace EnemyLogic
{
    public class DistanceTransition : EnemyTransition
    {
        [SerializeField] private float _transitionRange;
        [SerializeField] private float _rangedSpread;

        private void Start()
        {
            _transitionRange += Random.Range(-_rangedSpread, _rangedSpread);
        }

        private void Update()
        {
            if (EnemyTarget != null)
            {
                if (Vector3.Distance(Target.position, transform.position) < _transitionRange)
                    NeedTransit = true;
            }
        }
    }
}