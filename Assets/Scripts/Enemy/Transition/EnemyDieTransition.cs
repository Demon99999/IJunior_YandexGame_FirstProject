using UnityEngine;

[RequireComponent(typeof(HealthContainer))]
public class EnemyDieTransition : EnemyTransition
{
    private HealthContainer _healthContainer;

    private void Awake()
    {
        _healthContainer = GetComponent<HealthContainer>();
    }

    private void Update()
    {
        if (_healthContainer != null)
        {
            if (_healthContainer.Health <=0)
            {
                NeedTransit = true;
            }
        }
    }
}
