using System;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private float _currentHealth;
    public float CurrentHealth => _currentHealth;

    public event Action OnHit;
    public event Action OnDie;
    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(float amount)
    {
        _currentHealth  = Mathf.Clamp(_currentHealth - amount, 0, _maxHealth);

        OnHit?.Invoke();
        if (_currentHealth == 0)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        _currentHealth = Mathf.Clamp(_currentHealth + amount, 0, _maxHealth);
    }

    private void Die()
    {
        OnDie?.Invoke();
        this.enabled = false;
    }

    public float GetHealthParts()
    {
        return _currentHealth / _maxHealth;
    }
}