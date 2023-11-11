using UnityEngine;

public abstract class UnitTransition : MonoBehaviour
{
    [SerializeField] private UnitState _targetState;

    public UnitState TargetState => _targetState;

    public bool NeedTransit { get; protected set; }

    private void OnEnable()
    {
        NeedTransit = false;
    }
}
