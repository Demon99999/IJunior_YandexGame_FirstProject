using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Audio
{
    public class SoundSettings : MonoBehaviour
    {
        private const string MusicVolume = "MusicVolume";
        private const string EffectsVolume = "EffectsVolume";

        [SerializeField] private Slider _musicSlider;
        [SerializeField] private Slider _effectsSlider;
        [SerializeField] private AudioMixer _audioMixer;

        private float _defaultVolume = 0.74f;
        private float _startValue = -80f;
        private float _endValue = 0;

        private void Start()
        {
            if (PlayerPrefs.HasKey(MusicVolume) && PlayerPrefs.HasKey(EffectsVolume))
            {
                _musicSlider.value = PlayerPrefs.GetFloat(MusicVolume);
                _effectsSlider.value = PlayerPrefs.GetFloat(EffectsVolume);

                SetVolume(_musicSlider.value, MusicVolume);
                SetVolume(_effectsSlider.value, EffectsVolume);
            }
            else
            {
                _musicSlider.value = _defaultVolume;
                _effectsSlider.value = _defaultVolume;

                SetVolume(_musicSlider.value, MusicVolume);
                SetVolume(_effectsSlider.value, EffectsVolume);

                PlayerPrefs.SetFloat(MusicVolume, _defaultVolume);
                PlayerPrefs.SetFloat(EffectsVolume, _defaultVolume);
                PlayerPrefs.Save();
            }
        }

        private void OnEnable()
        {
            _musicSlider.onValueChanged.AddListener(OnSetMusicSlider);
            _effectsSlider.onValueChanged.AddListener(OnSetEffectsSlider);
        }

        private void OnDisable()
        {
            _musicSlider.onValueChanged.RemoveListener(OnSetMusicSlider);
            _effectsSlider.onValueChanged.RemoveListener(OnSetEffectsSlider);
        }

        public void OnSetMusicSlider(float volume)
        {
            _musicSlider.value = volume;
            SetVolume(volume, MusicVolume);
        }

        public void OnSetEffectsSlider(float volume)
        {
            _effectsSlider.value = volume;
            SetVolume(volume, EffectsVolume);
        }

        private void SetVolume(float volume, string nameVolume)
        {
            PlayerPrefs.SetFloat(nameVolume, volume);
            PlayerPrefs.Save();
            _audioMixer.SetFloat(nameVolume, Mathf.Lerp(_startValue, _endValue, volume));
        }
    }
}