using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ActionButtonUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _actionNameText;
    
    private BaseAction m_baseAction;

    public void SetBaseAction(BaseAction baseAction)
    {
        m_baseAction = baseAction;
        SetActionNameText();
    }
    
    private void SetActionNameText()
    {
        _actionNameText.text = m_baseAction.GetActionName().ToUpper();
    }
}
