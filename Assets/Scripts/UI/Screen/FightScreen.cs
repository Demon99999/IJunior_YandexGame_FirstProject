using UnityEngine;
using UnityEngine.UI;

public class FightScreen : Screen
{
    [SerializeField] private HealthContainer _healthContainer;
    [SerializeField] private Slider _slider;
    [SerializeField] private EnemyHandler _enemyHandler;

    private void OnEnable()
    {
        _enemyHandler.AllEnemiesKilled += CloseScreen;

        _healthContainer.MaxHealthChanged += OnMaxHealthChanged;
        _healthContainer.HealthChanged += OnChangeHealth;
    }

    private void OnDisable()
    {
        _enemyHandler.AllEnemiesKilled -= CloseScreen;

        _healthContainer.HealthChanged -= OnChangeHealth;
        _healthContainer.MaxHealthChanged -= OnMaxHealthChanged;
    }

    private void Start()
    {
        _slider.maxValue = _healthContainer.MaxHealth;
        _slider.value = _slider.maxValue;
    }

    public void OnMaxHealthChanged()
    {
        _slider.value = _slider.maxValue;
    }

    private void OnChangeHealth(int health)
    {
        _slider.value = health;
    }
}
