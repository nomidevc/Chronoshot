using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    [SerializeField] private TrailRenderer _bulletTrailRenderer;
    [SerializeField] private Transform _bulletHitImpactPrefab;
    
    private Vector3 m_targetPosition;
    private float m_bulletSpeed = 200f;
    
    public void SetTargetPosition(Vector3 targetPosition)
    {
        m_targetPosition = targetPosition;
    }

    void Update()
    {
        Vector3 moveDirection = (m_targetPosition - transform.position).normalized;
        
        float distanceBeforeMove = Vector3.Distance(transform.position, m_targetPosition);
        transform.position += moveDirection * (m_bulletSpeed * Time.deltaTime);
        float distanceAfterMove = Vector3.Distance(transform.position, m_targetPosition);
        
        if (distanceAfterMove > distanceBeforeMove)
        {
            transform.position = m_targetPosition;
            
            _bulletTrailRenderer.transform.parent = null;
            
            Destroy(gameObject);
            
            Instantiate(_bulletHitImpactPrefab, m_targetPosition, Quaternion.identity);
        }
    }
}
