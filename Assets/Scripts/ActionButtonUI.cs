using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionButtonUI : MonoBehaviour
{
    [SerializeField] private Button _actionButton;
    [SerializeField] private TextMeshProUGUI _actionNameText;
    
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
}
