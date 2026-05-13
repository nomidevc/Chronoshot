using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] Transform _actionVirtualCamera;

    void Start()
    {
        BaseAction.OnAnyActionStarted += BaseAction_OnAnyActionStarted;
        BaseAction.OnAnyActionCompleted += BaseAction_OnAnyActionCompleted;
    }
    private void BaseAction_OnAnyActionStarted(object sender, EventArgs e)
    {
        switch (sender)
        {
            case ShootAction shootAction:
                SetShootActionVirtualCamPosition(shootAction);
                ShowActionVirtualCamera();
                break;
        }
    }

    private void BaseAction_OnAnyActionCompleted(object sender, EventArgs e)
    {
        switch (sender)
        {
            case ShootAction shootAction:
                HideActionVirtualCamera();
                break;
        }
    }

    private void SetShootActionVirtualCamPosition(ShootAction shootAction)
    {
        Unit shootingUnit = shootAction.GetUnit();
        Unit targetUnit = shootAction.GetTargetedUnit();
        
        Vector3 virtualCamHeigth = Vector3.up * 1.7f;
        Vector3 shootDir = (targetUnit.GetWorldPosition() - shootingUnit.GetWorldPosition()).normalized;
        float shoulderOffsetAmount = 0.5f;
        Vector3 shoulderOffset = Quaternion.Euler(0, 90, 0) * shootDir * shoulderOffsetAmount;
        
        Vector3 actionCameraPosition = 
            shootingUnit.GetWorldPosition() + virtualCamHeigth + shootDir * -1f + shoulderOffset;
        
        _actionVirtualCamera.transform.position = actionCameraPosition;
        _actionVirtualCamera.transform.LookAt(targetUnit.GetWorldPosition() + virtualCamHeigth);
    }

    private void HideActionVirtualCamera()
    {
        _actionVirtualCamera.gameObject.SetActive(false);
    }
    
    private void ShowActionVirtualCamera()
    {
        _actionVirtualCamera.gameObject.SetActive(true);
    }
}
