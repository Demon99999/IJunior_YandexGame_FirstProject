using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(UnitDrag))]
public class Unit : MonoBehaviour
{
    [SerializeField] private UnitCard _nextCard;
    [SerializeField] private UnitCard _unitCard;
    [SerializeField] private ParticleSystem _particleSystem;
    
    private Vector3 _savePosition;
    private Unit _saveUnit;
    private Vector3 _target;
    private int _maxUnitGrade = 5;

    public UnitDrag Drag { get; private set; }
    public Cell Point { get; set; }
    public UnitCard Card => _unitCard;
    public Vector3 Target => _target;

    public event UnityAction Moving;
    
    private void Start()
    {
        _savePosition = transform.localPosition;
        Drag = GetComponent<UnitDrag>();
        Point = transform.GetComponentInParent<Cell>();
    }

    public void Init(Vector3 target)
    {
        _target = target;
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
