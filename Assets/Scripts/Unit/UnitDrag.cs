using System;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Collider),typeof(Animator),typeof(Collider))]
public class UnitDrag : MonoBehaviour
{
    private const string Fly = "Fly";
    private const string Idle = "Idle";

    [SerializeField] private LayerMask _draggingLayer;

    private Vector3 _savePosition;
    private Collider _collider;
    private Vector3 _defaultScale;
    private Vector3 _dragScale = new Vector3(0.5f, 0.5f, 0.5f);
    private bool _combining;

    private Cell _savePoint;
    private Animator _animator;

    public bool Dragging { get; private set; }
    public Unit Unit { get; private set; }
    public bool IsAvailableForTransfer { get; private set; }

    public event Action OnDragging;

    private void Start()
    {
        _collider = GetComponent<Collider>();
        Unit = GetComponent<Unit>();
        _animator = GetComponent<Animator>();
    }

    private void OnMouseDown()
    {
        _defaultScale = transform.localScale;
        transform.localScale = _dragScale;
        _savePosition = transform.localPosition;
        Dragging = true;
        OnDragging?.Invoke();
        _collider.isTrigger = false;
        _animator.Play(Fly);
    }

    private void OnMouseUp()
    {
        Dragging = false;
        OnDragging?.Invoke();
        _collider.isTrigger = true;
        _animator.Play(Idle);

        if (IsAvailableForTransfer)
        {
            if (_savePoint != null)
            {
                Transfer(_savePoint);
                _savePoint = null;
            }
        }

        if (!_combining)
        {
            transform.localPosition = _savePosition;
            transform.localScale = _defaultScale;
        }
    }

    private void Transfer(Cell cell)
    {
        Unit.Point.ChangeValue();
        transform.parent = cell.transform;
        transform.position = cell.transform.position;
        transform.localScale = _defaultScale;
        Unit.Point = cell;
        Unit.Point.ChangeValue();
    }

    private void OnMouseDrag()
    {
        if (Dragging)
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, float.MaxValue,
                _draggingLayer))
            {
                transform.position = hit.point;

                if (hit.collider.GameObject().TryGetComponent(out Cell point))
                {
                    if (point.IsEmployed == false)
                    {
                        IsAvailableForTransfer = true;
                        _savePoint = point;

                    }
                    else
                    {
                        IsAvailableForTransfer = false;
                    }
                }
            }
            else
            {
                transform.localPosition = _savePosition;
            }
        }
    } 

    public void ChangeCombineValue()
    {
        _combining = true;
    }
}
