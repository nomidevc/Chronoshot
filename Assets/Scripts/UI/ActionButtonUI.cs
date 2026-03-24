using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionButtonUI : MonoBehaviour
{
    [SerializeField] private Button _actionButton;
    [SerializeField] private TextMeshProUGUI _actionNameText;
    [SerializeField] private Transform _selectedActionUi;
    
    private BaseAction m_baseAction;

    public void SetBaseAction(BaseAction baseAction)
    {
        m_baseAction = baseAction;
        SetActionNameText();
        
        _actionButton.onClick.AddListener(() =>
        {
            UnitActionSystem.Instance.SetSelectedAction(m_baseAction);
        });
    }
    
    private void SetActionNameText()
    {
        _actionNameText.text = m_baseAction.GetActionName().ToUpper();
    }

    public void UpdateActiveVisual()
    {
        BaseAction selectedBaseAction = UnitActionSystem.Instance.GetSelectedAction();
        _selectedActionUi.gameObject.SetActive(m_baseAction == selectedBaseAction);
    }
}
