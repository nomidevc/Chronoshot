using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;

    private CinemachineTransposer m_cinemachineTransposer;
    private const float MIN_FOLLOW_OFFSET_Y = 2f;
    private const float MAX_FOLLOW_OFFSET_Y = 15f;
    
    void Update()
    {
        Vector3 inputVector = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.W))
        {
            inputVector.z = +1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputVector.z = -1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputVector.x = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputVector.x = +1;
        }
        
        Vector3 moveVector = transform.forward * inputVector.z + transform.right * inputVector.x;
        float moveSpeed = 10f;
        transform.position += moveVector * (Time.deltaTime * moveSpeed);
        
        Vector3 rotationVector = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.Q))
        {
            rotationVector.y = -1;
        }
        if (Input.GetKey(KeyCode.E))
        {
            rotationVector.y = +1;
        }
        float rotationSpeed = 50f;
        transform.eulerAngles += rotationVector * (Time.deltaTime * rotationSpeed);
        
        m_cinemachineTransposer = _virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        Vector3 zoomVector = m_cinemachineTransposer.m_FollowOffset;
        float zoomAmount = 1f;
        if(Input.mouseScrollDelta.y > 0)
        {
            zoomVector.y -= zoomAmount;
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            zoomVector.y += zoomAmount;
        }
        zoomVector.y = Mathf.Clamp(zoomVector.y, MIN_FOLLOW_OFFSET_Y, MAX_FOLLOW_OFFSET_Y);
        float zoomLerpSpeed = 50f;
        m_cinemachineTransposer.m_FollowOffset = 
            Vector3.Lerp(m_cinemachineTransposer.m_FollowOffset, zoomVector, Time.deltaTime * zoomLerpSpeed);

    }
}
