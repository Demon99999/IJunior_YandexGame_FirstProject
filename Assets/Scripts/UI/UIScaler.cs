using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIScaler : MonoBehaviour
    {
        [SerializeField] private float _minMatch = 0.4f;
        [SerializeField] private float _maxMatch = 1f;
        [SerializeField] private float _aspectRatioThreshold = 16f / 9f;

        private CanvasScaler _canvasScaler;
        private float _resolutionX = 1920f;
        private float _resolutionY = 1080f;

        private void Awake()
        {
            _canvasScaler = GetComponent<CanvasScaler>();
        }

        private void Start()
        {
            _canvasScaler.referenceResolution = new Vector2(_resolutionX, _resolutionY);
            _canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
        }

        private void Update()
        {
            UpdateCanvasScaler();
        }

        private void UpdateCanvasScaler()
        {
            float currentAspectRatio = (float)UnityEngine.Screen.width / UnityEngine.Screen.height;

            if (currentAspectRatio >= _aspectRatioThreshold)
                _canvasScaler.matchWidthOrHeight = _maxMatch;
            else
                _canvasScaler.matchWidthOrHeight = _minMatch;
        }
    }
}