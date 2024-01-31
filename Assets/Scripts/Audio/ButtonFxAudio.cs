using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Audio
{
    [RequireComponent(typeof(Button))]
    public class ButtonFxAudio : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private AudioSource _audioSource;

        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_button.interactable)
                _audioSource.Play();
        }
    }
}