using System.Collections.Generic;
using UnityEngine;

public abstract class UnitState : MonoBehaviour
{
    [SerializeField] private List<UnitTransition> _transitions;

    protected Animator Animator { get; private set; }
    protected Vector3 Target { get; set; }

    public void Enter(Animator animator,Vector3 target)
    {
        if (enabled == false)
        {
            Animator=animator;
            Target = target;
            enabled = true;

            foreach (var transition in _transitions)
            {
                transition.enabled = true;
            }
        }
    }

    public void Exit()
    {
        if (enabled == true)
        {
            foreach (var transition in _transitions)
            {
                transition.enabled = false;
            }

            enabled = false;
        }
    }

    public UnitState GetNext()
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
}
