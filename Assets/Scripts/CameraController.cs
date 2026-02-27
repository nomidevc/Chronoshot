using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
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
    }
}
