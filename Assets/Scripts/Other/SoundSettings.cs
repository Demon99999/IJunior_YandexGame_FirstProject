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

                SetVolumeMusic(_musicSlider.value);
                SetVolumeEffects(_effectsSlider.value);
            }
            else
            {
                _musicSlider.value = _defaultVolume;
                _effectsSlider.value = _defaultVolume;

                SetVolumeMusic(_defaultVolume);
                SetVolumeEffects(_defaultVolume);

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
            PlayerPrefs.SetFloat(MusicVolume, volume);
            PlayerPrefs.Save();
            SetVolumeMusic(volume);
        }

        public void OnSetEffectsSlider(float volume)
        {
            _effectsSlider.value = volume;
            PlayerPrefs.SetFloat(EffectsVolume, volume);
            PlayerPrefs.Save();
            SetVolumeEffects(volume);
        }

        private void SetVolumeMusic(float volume)
        {
            _audioMixer.SetFloat(MusicVolume, Mathf.Lerp(_startValue, _endValue, volume));
        }

        private void SetVolumeEffects(float volume)
        {
            _audioMixer.SetFloat(EffectsVolume, Mathf.Lerp(_startValue, _endValue, volume));
        }
    }
}