using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AimCursor : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;

    private SpriteRenderer _spriteRenderer;
    private Transform cameraPos;

    private bool _isCursorEnable = false;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        cameraPos = Camera.main.transform;
        _spriteRenderer.enabled = false;
    }

    private void Update()
    {
        if (_isCursorEnable)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (!Physics.Raycast(ray, out hit, Mathf.Infinity, _layerMask))
            {
                _spriteRenderer.enabled = false;
            }
            else
            {
                transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                _spriteRenderer.enabled = true;
            }
        }
    }

    public void CursorOn(bool value)
    {
        _spriteRenderer.enabled = value;
        _isCursorEnable = value;
    }
}
