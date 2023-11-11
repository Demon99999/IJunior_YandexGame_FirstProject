using UnityEngine;

public class EnemyTransition : MonoBehaviour
{
    [SerializeField] private EnemyState _targetState;

    public EnemyState TargetState => _targetState;
    protected StrongPoint StrongPoint { get; private set; }
    public bool NeedTransit { get; protected set; }

    protected virtual void OnEnable()
    {
        NeedTransit = false;
    }

    public void Init(StrongPoint strongPoint)
    {
        StrongPoint = strongPoint;
    }
}
