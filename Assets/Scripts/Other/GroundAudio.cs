using UnityEngine;

public class GroundAudio : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _calmClip;
    [SerializeField] private AudioClip _fightClip;

    [SerializeField] private EnemyHandler _enemyHandler;
    [SerializeField] private HealthContainer _healthContainer;

    private void Start()
    {
        On�almClip();
    }

    private void OnEnable()
    {
        _enemyHandler.AllEnemiesKilled += On�almClip;
        _healthContainer.Died += On�almClip;
    }

    private void OnDisable()
    {
        _enemyHandler.AllEnemiesKilled -= On�almClip;
        _healthContainer.Died -= On�almClip;
    }

    public void On�almClip()
    {
        _audioSource.clip = _calmClip;
        _audioSource.Play();
    }

    public void OnFightClip()
    {
        _audioSource.clip = _fightClip;
        _audioSource.Play();
    }
}
