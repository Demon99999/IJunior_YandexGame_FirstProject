using GameLogic;
using UnityEngine;

namespace UnitLogic
{
    [RequireComponent(typeof(Unit))]
    public class UnitTransfer : MonoBehaviour
    {
        public Unit Unit { get; private set; }

        private void Start()
        {
            Unit = GetComponent<Unit>();
        }

        public void Displacement(Cell cell)
        {
            Unit.Point.ChangeValue();
            transform.parent = cell.transform;
            transform.position = cell.transform.position;
            Unit.InitPoint(cell);
            Unit.Point.ChangeValue();
        }
    }
}