using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionBusyUI : MonoBehaviour
{
    [SerializeField] private Transform _actionBusyTransform;

    void Start()
    {
        UnitActionSystem.Instance.OnBusyChanged += UnitActionSystem_OnBusyChanged;
        HideBusyUI();
    }
    private void UnitActionSystem_OnBusyChanged(object sender, bool isBusy)
    {
        if (isBusy)
        {
            ShowBusyUI();
        }
        else
        {
            HideBusyUI();
        }
    }

    private void ShowBusyUI()
    {
        _actionBusyTransform.gameObject.SetActive(true);
    }
    
    private void HideBusyUI()
    {
        _actionBusyTransform.gameObject.SetActive(false);
    }
}
