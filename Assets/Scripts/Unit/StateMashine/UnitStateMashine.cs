using UnityEngine;

namespace UnitLogic
{
    [RequireComponent(typeof(Animator))]
    public class UnitStateMashine : MonoBehaviour
    {
        [SerializeField] private IdleState _firstState;

        private UnitState _currentState;
        private Animator _animator;
        private Vector3 _target;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            _currentState = _firstState;
            _currentState.Enter(_animator, _target);
        }

        private void Update()
        {
            _target = GetComponent<Unit>().Target;

            if (_currentState == null)
            {
                return;
            }

            UnitState nextState = _currentState.GetNext();

            if (nextState != null)
            {
                Transit(nextState);
            }
        }

        public void Reset()
        {
            _currentState.Exit();
            _currentState = _firstState;
            _currentState.Enter(_animator, _target);
            Transit(_firstState);
        }

        public void Init(Vector3 target)
        {
            _target = target;
        }

        private void Transit(UnitState nextState)
        {
            if (_currentState != null)
            {
                _currentState.Exit();
            }

            _currentState = nextState;

            if (_currentState != null)
            {
                _currentState.Enter(_animator, _target);
            }
        }
    }
}