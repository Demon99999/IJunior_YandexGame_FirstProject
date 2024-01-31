using EnemyLogic;
using UI;
using UnityEngine;

namespace Audio
{
    public class GroundAudio : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _calmClip;
        [SerializeField] private AudioClip _fightClip;

        [SerializeField] private EnemyHandler _enemyHandler;
        [SerializeField] private HealthContainer _healthContainer;
        [SerializeField] private BattleScreen _battleScreen;

        private void Start()
        {
            OnÑalmClip();
        }

        private void OnEnable()
        {
            _enemyHandler.AllEnemiesKilled += OnÑalmClip;
            _healthContainer.Died += OnÑalmClip;
            _battleScreen.PlayButtonClick += OnFightClip;
        }

        private void OnDisable()
        {
            _enemyHandler.AllEnemiesKilled -= OnÑalmClip;
            _healthContainer.Died -= OnÑalmClip;
            _battleScreen.PlayButtonClick -= OnFightClip;
        }

        private void OnÑalmClip()
        {
            _audioSource.clip = _calmClip;
            _audioSource.Play();
        }

        private void OnFightClip()
        {
            _audioSource.clip = _fightClip;
            _audioSource.Play();
        }
    }
}