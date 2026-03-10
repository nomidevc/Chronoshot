using UnityEngine;

public class SpinAction : BaseAction
{
    private float m_rotationAmount;
    
    void Update()
    {
        if(!m_isActive) return;
        
        float rotationAmountPerFrame = 360f * Time.deltaTime; // Rotate 360 degrees per second
        transform.eulerAngles += new Vector3(0, rotationAmountPerFrame, 0);
        m_rotationAmount += rotationAmountPerFrame;
        if(m_rotationAmount >= 360f) 
        {
            StopSpin();
        }
    }
    
    public void StartSpin()
    {
        m_isActive = true;
        m_rotationAmount = 0f;
    }
    
    public void StopSpin()
    { 
        m_isActive = false;
        m_rotationAmount = 0f;
    }
}
