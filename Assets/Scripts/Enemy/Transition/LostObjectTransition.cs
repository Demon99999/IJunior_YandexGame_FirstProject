using UnityEngine;

namespace EnemyLogic
{
    public class LostObjectTransition : EnemyTransition
    {
        private float _distanceTransition = 2f;

        private void Update()
        {
            if (EnemyTarget != null)
            {
                if (Vector3.Distance(transform.position, Target.position) > _distanceTransition)
                {
                    NeedTransit = true;
                }
            }
        }
    }
}