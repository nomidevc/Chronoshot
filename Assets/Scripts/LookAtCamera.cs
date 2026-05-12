using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    [SerializeField] private bool _invert;
    
    private Transform m_mainCameraTransform;

    void Awake()
    {
        m_mainCameraTransform = Camera.main.transform;
    }

    void LateUpdate()
    {
        if (_invert)
        {
            Vector3 dirToCamera = (m_mainCameraTransform.position - transform.position).normalized;
            transform.LookAt(transform.position - dirToCamera);
        }
        else this.transform.LookAt(m_mainCameraTransform);
    }
}
