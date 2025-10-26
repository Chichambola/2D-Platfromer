using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _currentHealth;

    public event Action<float, float> ValueChanged;

    public bool IsAlive => _currentHealth > 0;
    public float Value => _currentHealth;
    public float MaxValue => _maxHealth;

    private void OnValidate()
    {
        IsHealthMoreMaxHealth();
    }

    public void Heal(float healAmount)
    {
        if(healAmount > 0)
        {
            _currentHealth += healAmount;

            IsHealthMoreMaxHealth();

            ValueChanged?.Invoke(_currentHealth, _maxHealth);
        }
    }

    public void TakeDamage(float damage)
    {
        if (IsAlive && damage > 0)
        {
            _currentHealth -= damage;

            if (_currentHealth < 0)
                _currentHealth = 0;

            ValueChanged?.Invoke(_currentHealth, _maxHealth);
        }
    }

    private void IsHealthMoreMaxHealth()
    {
        if (_currentHealth > _maxHealth)
            _currentHealth = _maxHealth;
    }
}
