using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitWorldUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _unitActionPointText;
    [SerializeField] private Image _unitHealthBar;
    [SerializeField] private HealthSystem _unitHealthSystem;
    [SerializeField] private Unit _unit;

    void Start()
    {
        Unit.OnAnyActionPointsChanged += Unit_OnAnyActionPointsChanged;
        _unitHealthSystem.OnHealthChangeAction += UnitHealthSys_OnHealthChange;
        
        UpdateUnitActionPointText();
        UpdateHealthBarImage();
    }
    private void Unit_OnAnyActionPointsChanged(object sender, EventArgs e)
    {
        UpdateUnitActionPointText();
    }

    private void UnitHealthSys_OnHealthChange()
    {
        UpdateHealthBarImage();
    }

    private void UpdateUnitActionPointText()
    {
        _unitActionPointText.text = _unit.GetRemainingActionPoints().ToString();
    }

    private void UpdateHealthBarImage()
    {
        _unitHealthBar.fillAmount = _unitHealthSystem.GetHealthNormalized();
    }
}
