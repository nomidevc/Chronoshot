using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public Action OnDieAction;
    public Action OnHealthChangeAction;
    
    [SerializeField] private int _healthAmount = 100;
    
    private int m_healthAmountMax;

    void Awake()
    {
        m_healthAmountMax = _healthAmount;
    }

    public void TakeDamage(int damageAmount)
    {
        _healthAmount -= damageAmount;

        if (_healthAmount < 0)
        {
            _healthAmount = 0;
            Die();
        }
        
        OnHealthChangeAction?.Invoke();
    }
    
    private void Die()
    {
        OnDieAction?.Invoke();
    }
    
    public float GetHealthNormalized()
    {
        return (float)_healthAmount / m_healthAmountMax;
    }
}
