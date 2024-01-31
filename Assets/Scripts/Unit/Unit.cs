using System;
using GameLogic;
using UnityEngine;

namespace UnitLogic
{
    [RequireComponent(typeof(UnitDrag))]
    public class Unit : MonoBehaviour
    {
        [SerializeField] private UnitCard _nextCard;
        [SerializeField] private UnitCard _unitCard;
        [SerializeField] private ParticleSystem _particleSystem;

        private Cell _point;
        private Unit _saveUnit;
        private Vector3 _target;
        private int _maxUnitGrade = 5;

        public event Action Moving;

        public UnitDrag Drag { get; private set; }

        public Cell Point => _point;

        public UnitCard Card => _unitCard;

        public Vector3 Target => _target;

        private void Start()
        {
            Drag = GetComponent<UnitDrag>();
            _point = transform.GetComponentInParent<Cell>();
        }

        public void Init(Vector3 target)
        {
            _target = target;
        }

        public void InitPoint(Cell point)
        {
            _point = point;
        }

        public void Move()
        {
            Moving?.Invoke();
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.TryGetComponent(out Unit unit))
            {
                if (_unitCard.Variety == unit.Card.Variety)
                {
                    if (_unitCard.Grade == unit.Card.Grade)
                    {
                        if (unit.Drag.Dragging)
                        {
                            if (unit._unitCard.Grade < _maxUnitGrade)
                            {
                                _saveUnit = unit;
                                unit.Drag.OnDragging += DragEnd;
                            }
                        }
                    }
                }
            }
        }

        private void OnTriggerExit(Collider collider)
        {
            if (collider.gameObject.TryGetComponent(out Unit unit))
            {
                if (unit == _saveUnit)
                {
                    unit.Drag.OnDragging -= DragEnd;
                    _saveUnit = null;
                }
            }
        }

        private void DragEnd()
        {
            Drag.ChangeCombineValue();
            Instantiate(_nextCard.Template, Point.transform);
            _saveUnit.Point.ChangeValue();
            _particleSystem.Play();
            Destroy(_saveUnit.gameObject);
            Destroy(this.gameObject);
        }
    }
}