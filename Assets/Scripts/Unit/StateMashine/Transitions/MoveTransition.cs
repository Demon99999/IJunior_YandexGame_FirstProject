using UnityEngine;

namespace UnitLogic
{
    [RequireComponent(typeof(Unit))]
    public class MoveTransition : UnitTransition
    {
        private Unit _unit;

        private void Awake()
        {
            _unit = GetComponent<Unit>();
        }

        private void OnEnable()
        {
            _unit.Moving += OnMoving;
        }

        private void OnDisable()
        {
            _unit.Moving -= OnMoving;
        }

        private void OnMoving()
        {
            NeedTransit = true;
        }
    }
}