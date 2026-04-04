using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public Action OnDieAction;
    
    [SerializeField] private int _healthAmount = 100;

    public void TakeDamage(int damageAmount)
    {
        _healthAmount -= damageAmount;

        if (_healthAmount < 0)
        {
            _healthAmount = 0;
            Die();
        }
        
        Debug.Log(_healthAmount);
    }
    
    private void Die()
    {
        OnDieAction?.Invoke();
    }
}
