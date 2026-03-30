using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurnSystemUI : MonoBehaviour
{
    [SerializeField] private Button _endTurnButton;
    [SerializeField] private TextMeshProUGUI _turnCountText;
    [SerializeField] private Transform _enemyTurnPhaseUI;

    void Start()
    {
        TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;
        
        _endTurnButton.onClick.AddListener(() =>
        {
            TurnSystem.Instance.NextTurn();
        });
        UpdateTurnCountText();
        UpdateEnemyTurnPhaseText();
        UpdateEndButtonVisibility();
    }
    private void TurnSystem_OnTurnChanged(object sender, EventArgs e)
    {
        UpdateTurnCountText();
        UpdateEnemyTurnPhaseText();
        UpdateEndButtonVisibility();
    }

    private void UpdateTurnCountText()
    {
        _turnCountText.text = "Turn: " + TurnSystem.Instance.GetTurnCount();
    }

    private void UpdateEnemyTurnPhaseText()
    {
        _enemyTurnPhaseUI.gameObject.SetActive(!TurnSystem.Instance.IsPLayerTurn());
    }

    private void UpdateEndButtonVisibility()
    {
        _endTurnButton.gameObject.SetActive(TurnSystem.Instance.IsPLayerTurn());
    }
    
}
