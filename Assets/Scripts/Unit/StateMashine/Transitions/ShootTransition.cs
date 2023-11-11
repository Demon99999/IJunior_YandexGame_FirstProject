using UnityEngine;

[RequireComponent(typeof(Unit))]
public class ShootTransition : UnitTransition
{
    private Vector3 _target;

    private void Start()
    {
        _target = GetComponent<Unit>().Target;
    }

    private void Update()
    {
        if (_target != null)
        {
            if (Vector3.Distance(transform.position, _target) < 0.5f)
            {
                NeedTransit = true;
            }
        }
    }
}
