using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;

    private CinemachineTransposer m_cinemachineTransposer;
    private Vector3 m_followOffset;
    private const float MIN_FOLLOW_OFFSET_Y = 2f;
    private const float MAX_FOLLOW_OFFSET_Y = 15f;

    void Start()
    {
        m_cinemachineTransposer = _virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        m_followOffset = m_cinemachineTransposer.m_FollowOffset;
    }

    void Update()
    {
        HandleCameraMovement();

        HandleCameraRotation();

        HandleCameraZoom();

    }
    private void HandleCameraZoom()
    {

        float zoomAmount = 1f;
        if(Input.mouseScrollDelta.y > 0)
        {
            m_followOffset.y -= zoomAmount;
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            m_followOffset.y += zoomAmount;
        }
        
        m_followOffset.y = Mathf.Clamp(m_followOffset.y, MIN_FOLLOW_OFFSET_Y, MAX_FOLLOW_OFFSET_Y);
        float zoomLerpSpeed = 50f;
        m_cinemachineTransposer.m_FollowOffset = 
            Vector3.Lerp(m_cinemachineTransposer.m_FollowOffset, m_followOffset, Time.deltaTime * zoomLerpSpeed);
    }
    private void HandleCameraRotation()
    {

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
    private void HandleCameraMovement()
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
    }
}
