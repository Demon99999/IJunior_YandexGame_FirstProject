using UI;
using UnityEngine;

namespace GameLogic
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class AimCursor : MonoBehaviour
    {
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private float _offSetX = 2f;
        [SerializeField] private VictoryScreen _victoryScreen;
        [SerializeField] private DefeatScreen _defeatScreen;
        [SerializeField] private BattleScreen _battleScreen;

        private SpriteRenderer _spriteRenderer;

        private bool _isCursorEnable = false;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.enabled = false;
        }

        private void OnEnable()
        {
            _victoryScreen.ResumeButtonClick += OnCursorOff;
            _victoryScreen.BonusButtonClick += OnCursorOff;
            _defeatScreen.RestartButtonClick += OnCursorOff;
            _defeatScreen.BonusButtonClick += OnCursorOff;
            _battleScreen.PlayButtonClick += OnCursorOn;
        }

        private void OnDisable()
        {
            _victoryScreen.ResumeButtonClick -= OnCursorOff;
            _victoryScreen.BonusButtonClick -= OnCursorOff;
            _defeatScreen.RestartButtonClick -= OnCursorOff;
            _defeatScreen.BonusButtonClick -= OnCursorOff;
            _battleScreen.PlayButtonClick -= OnCursorOn;
        }

        private void Update()
        {
            if (_isCursorEnable)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                MoveAim(ray);

                if (Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);
                    Ray rayTouch = Camera.main.ScreenPointToRay(touch.position);

                    MoveAim(rayTouch);
                }
            }
        }

        private void OnCursorOn()
        {
            _spriteRenderer.enabled = true;
            _isCursorEnable = true;
        }

        private void OnCursorOff()
        {
            _spriteRenderer.enabled = false;
            _isCursorEnable = false;
        }

        private void MoveAim(Ray ray)
        {
            RaycastHit hit;

            if (!Physics.Raycast(ray, out hit, Mathf.Infinity, _layerMask))
            {
                _spriteRenderer.enabled = false;
            }
            else
            {
                transform.position = new Vector3(hit.point.x - _offSetX, hit.point.y, hit.point.z);
                _spriteRenderer.enabled = true;
            }
        }
    }
}