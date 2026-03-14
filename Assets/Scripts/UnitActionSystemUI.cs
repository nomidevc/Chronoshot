using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitActionSystemUI : MonoBehaviour
{
    [SerializeField] private Transform _actionButtonPrefab;
    [SerializeField] private Transform _actionButtonContainer;

    void Start()
    {
        UnitActionSystem.Instance.OnSelectedUnitChanged += UnitActionSystem_OnSelectedUnitChanged;
        
        CreatActionButtonUI();
    }
    private void UnitActionSystem_OnSelectedUnitChanged(object sender, EventArgs e)
    {
        CreatActionButtonUI();
    }

    private void CreatActionButtonUI()
    {
        foreach (Transform actionButton in _actionButtonContainer)    
        {
            Destroy(actionButton.gameObject);
        }
        
        Unit selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();
        foreach (BaseAction baseAction in selectedUnit.GetBaseActionArray())
        {
            Transform actionButton = Instantiate(_actionButtonPrefab, _actionButtonContainer);
            ActionButtonUI actionButtonUI = actionButton.GetComponent<ActionButtonUI>();
            actionButtonUI.SetBaseAction(baseAction);
        }
    }
}
