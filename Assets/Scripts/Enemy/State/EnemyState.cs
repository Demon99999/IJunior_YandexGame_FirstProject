using UnityEngine;

public class EnemyState : MonoBehaviour
{
    [SerializeField] private EnemyTransition[] _transitions;

    public StrongPoint StrongPoint { get; private set; }
    public Animator Animator { get; private set; }
    public Rigidbody Rigidbody { get; private set; }
    public EnemyTransition[] Transitions => _transitions;
    public Transform Target;

    public void Enter(StrongPoint strongPoint, Animator animator, Rigidbody rigidbody, Transform target)
    {
        if (enabled == false)
        {
            StrongPoint = strongPoint;
            Animator = animator;
            Rigidbody = rigidbody;
            Target = target;

            enabled = true;

            foreach (var transition in _transitions)
            {
                transition.enabled = true;
                transition.Init(StrongPoint, Target);
            }
        }
    }

    public EnemyState GetNextState()
    {
        foreach (var transition in _transitions)
        {
            if (transition.NeedTransit)
            {
                return transition.TargetState;
            }
        }

        return null;
    }

    public void Exit()
    {
        if (enabled == true)
        {
            foreach (var transition in _transitions)
            {
                transition.enabled = false;
            }
        }

        enabled = false;
    }
}
