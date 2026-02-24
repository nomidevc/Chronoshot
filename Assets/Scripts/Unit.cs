using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private Vector3 m_targetPosition;

    void Update()
    {
        MoveToTargetPosition();
        if (Input.GetMouseButtonDown(0))
        { 
            SetTargetPosition(MouseWorld.GetMouseWorldPosition());
        }

    }
    private void MoveToTargetPosition()
    {
        float stoppingDistance = 0.1f;
        if(Vector3.Distance(transform.position, m_targetPosition) > stoppingDistance)
        {
            Vector3 moveDirection = (m_targetPosition - transform.position).normalized;
            float moveSpeed = 5f;
            transform.position += moveDirection * Time.deltaTime * moveSpeed;
        }
    }

    private void SetTargetPosition(Vector3 targetPosition)
    {
        m_targetPosition = targetPosition;
    }
}
