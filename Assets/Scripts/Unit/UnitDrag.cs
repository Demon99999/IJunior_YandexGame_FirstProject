using System;
using GameLogic;
using Unity.VisualScripting;
using UnityEngine;

namespace UnitLogic
{
    [RequireComponent(typeof(Collider), typeof(UnitTransfer))]
    public class UnitDrag : MonoBehaviour
    {
        [SerializeField] private LayerMask _draggingLayer;

        private UnitTransfer _unitTransfer;
        private Vector3 _savePosition;
        private Collider _collider;
        private Vector3 _defaultScale;
        private Vector3 _dragScale = new Vector3(0.5f, 0.5f, 0.5f);
        private bool _combining;

        private Cell _savePoint;

        public event Action OnDragging;

        public bool Dragging { get; private set; }

        public bool IsAvailableForTransfer { get; private set; }

        private void Start()
        {
            _unitTransfer = GetComponent<UnitTransfer>();
            _collider = GetComponent<Collider>();
        }

        private void OnMouseDown()
        {
            _defaultScale = transform.localScale;
            transform.localScale = _dragScale;
            _savePosition = transform.localPosition;
            Dragging = true;
            OnDragging?.Invoke();
            _collider.isTrigger = false;
        }

        private void OnMouseUp()
        {
            Dragging = false;
            OnDragging?.Invoke();
            _collider.isTrigger = true;

            if (IsAvailableForTransfer)
            {
                if (_savePoint != null)
                {
                    _unitTransfer.Displacement(_savePoint);
                    transform.localScale = _defaultScale;
                    _savePoint = null;
                }
            }

            if (!_combining)
            {
                transform.localPosition = _savePosition;
                transform.localScale = _defaultScale;
            }
        }

        private void OnMouseDrag()
        {
            if (Dragging)
            {
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, float.MaxValue, _draggingLayer))
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
}