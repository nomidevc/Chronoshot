using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitActionSystemUI : MonoBehaviour
{
    [SerializeField] private Transform _actionButtonPrefab;
    [SerializeField] private Transform _actionButtonContainer;
    
    private List<ActionButtonUI> m_actionButtonUIArray = new List<ActionButtonUI>();

    void Start()
    {
        UnitActionSystem.Instance.OnSelectedUnitChanged += UnitActionSystem_OnSelectedUnitChanged;
        UnitActionSystem.Instance.OnSelectedActionChanged += UnitActionSystem_OnSelectedActionChanged;
        
        CreatActionButtonUI();
        UpdateSelectedActionVisual();
    }
    private void UnitActionSystem_OnSelectedActionChanged(object sender, EventArgs e)
    {
        UpdateSelectedActionVisual();
    }
    private void UnitActionSystem_OnSelectedUnitChanged(object sender, EventArgs e)
    {
        CreatActionButtonUI();
        UpdateSelectedActionVisual();
    }

    private void CreatActionButtonUI()
    {
        foreach (Transform actionButton in _actionButtonContainer)    
        {
            Destroy(actionButton.gameObject);
        }
        m_actionButtonUIArray.Clear();
        
        Unit selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();
        foreach (BaseAction baseAction in selectedUnit.GetBaseActionArray())
        {
            Transform actionButton = Instantiate(_actionButtonPrefab, _actionButtonContainer);
            ActionButtonUI actionButtonUI = actionButton.GetComponent<ActionButtonUI>();
            actionButtonUI.SetBaseAction(baseAction);
            
            m_actionButtonUIArray.Add(actionButtonUI);
        }
    }

    private void UpdateSelectedActionVisual()
    {
        foreach (ActionButtonUI actionButtonUI in m_actionButtonUIArray)
        {
            actionButtonUI.UpdateActiveVisual();
        }
    }
}
