using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UnitActionSystemUI : MonoBehaviour
{
    [SerializeField] private Transform _actionButtonPrefab;
    [SerializeField] private Transform _actionButtonContainer;
    [SerializeField] private TextMeshProUGUI _actionPointsText;
    
    private List<ActionButtonUI> m_actionButtonUIArray = new List<ActionButtonUI>();

    void Start()
    {
        UnitActionSystem.Instance.OnSelectedUnitChanged += UnitActionSystem_OnSelectedUnitChanged;
        UnitActionSystem.Instance.OnSelectedActionChanged += UnitActionSystem_OnSelectedActionChanged;
        UnitActionSystem.Instance.OnActionStarted += UnitActionSystem_OnActionStarted;
        Unit.OnAnyActionPointsChanged += Unit_OnAnyActionPointsChanged;
        
        UpdateActionPointsText();
        CreatActionButtonUI();
        UpdateSelectedActionVisual();
    }
    private void Unit_OnAnyActionPointsChanged(object sender, EventArgs e)
    {
        UpdateActionPointsText();
    }
    private void UnitActionSystem_OnActionStarted(object sender, EventArgs e)
    {
        UpdateActionPointsText();
    }
    private void UnitActionSystem_OnSelectedActionChanged(object sender, EventArgs e)
    {
        UpdateSelectedActionVisual();
    }
    private void UnitActionSystem_OnSelectedUnitChanged(object sender, EventArgs e)
    {
        UpdateActionPointsText();
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

    private void UpdateActionPointsText()
    {
        Unit selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();
        _actionPointsText.text = "Action Points: " + selectedUnit.GetRemainingActionPoints();
    }
}
