using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitRagdollSpawner : MonoBehaviour
{
    [SerializeField] private Transform _unitRagdollPrefab;
    
    private HealthSystem m_healthSystem;

    void Awake()
    {
        m_healthSystem = GetComponent<HealthSystem>();
    }

    void Start()
    {
        m_healthSystem.OnDieAction += HealthSystem_OnDie;
    }
    private void HealthSystem_OnDie()
    {
        Transform unitRagdollTransform = Instantiate(_unitRagdollPrefab, transform.position, transform.rotation);
        
    }
}
