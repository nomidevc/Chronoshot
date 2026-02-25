using System;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private Animator _unitAnimator;
    
    private Vector3 m_targetPosition;

    void Awake()
    {
        m_targetPosition = transform.position;
    }

    void Update()
    {
        MoveToTargetPosition();
    }

    private void MoveToTargetPosition()
    {
        float stoppingDistance = 0.1f;
        if(Vector3.Distance(transform.position, m_targetPosition) > stoppingDistance)
        {
            _unitAnimator.SetBool("IsWalking", true);
            
            Vector3 moveDirection = (m_targetPosition - transform.position).normalized;
            float moveSpeed = 5f;
            transform.position += moveDirection * (Time.deltaTime * moveSpeed);
            
            float rotationSpeed = 10f;
            transform.forward = Vector3.Lerp(transform.forward, moveDirection, rotationSpeed * Time.deltaTime);
        }
        else
        {
            _unitAnimator.SetBool("IsWalking", false);
        }
    }

    public void SetTargetPosition(Vector3 targetPosition)
    {
        m_targetPosition = targetPosition;
    }
}
