using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

namespace Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class EnemyAudio : MonoBehaviour
    {
        [SerializeField] private AudioClip[] _audioClips;
        [SerializeField] private AudioMixerGroup _audioMixerGroup;

        private AudioSource _audioSource;
        private int _lastClipIndex = -1;
        private float _delayMax = 15f;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.outputAudioMixerGroup = _audioMixerGroup;
            StartCoroutine(PlayRandomClipWithDelay());
        }

        private IEnumerator PlayRandomClipWithDelay()
        {
            float delay = Random.Range(0, _delayMax);
            yield return new WaitForSeconds(delay);
            PlayRandomClip();

            while (_audioSource.isPlaying)
            {
                yield return null;
            }

            StartCoroutine(PlayRandomClipWithDelay());
        }

        private void PlayRandomClip()
        {
            int randomIndex = GetRandomClipIndex();

            if (randomIndex == _lastClipIndex)
            {
                randomIndex = GetRandomClipIndex();
            }

            AudioClip randomClip = _audioClips[randomIndex];
            _audioSource.clip = randomClip;
            _audioSource.Play();
            _lastClipIndex = randomIndex;
        }

        private int GetRandomClipIndex()
        {
            return Random.Range(0, _audioClips.Length);
        }
    }
}