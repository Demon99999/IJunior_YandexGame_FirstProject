using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(HealthContainer))]
public class StrongPoint : MonoBehaviour
{
    private HealthContainer _healthContainer;
    private bool _isAlive;
    
    public event UnityAction Died;
    public event UnityAction Damaged;
    
    public HealthContainer HealthContainer => _healthContainer;
    
    private void Awake()
    {
        _healthContainer = GetComponent<HealthContainer>();
    }

    private void OnEnable()
    {
        _healthContainer.Died += OnDied;
    }

    private void OnDisable()
    {
        _healthContainer.Died -= OnDied;
    }

    public bool IsAlive()
    {
        if (_healthContainer.Health <= 0)
        {
            return _isAlive = false;
        }
        else
        {
            return _isAlive = true;
        }
    }

    public void ApplyDamage(float damage)
    {
        _healthContainer.TakeDamage((int)damage);
        Damaged?.Invoke();
    }

    private void OnDied()
    {
        Died?.Invoke();
    }
}
