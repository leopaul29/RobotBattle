using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private Button player1AttackRightArmButton;
    [SerializeField] private Button player1AttackLeftArmButton;
    [SerializeField] private Button player1RepairRightArmButton;
    [SerializeField] private Button player1RepairLeftArmButton;
    [SerializeField] private Button player1RepairBodyButton;
    
    [SerializeField] private Button player2AttackRightArmButton;
    [SerializeField] private Button player2AttackLeftArmButton;
    [SerializeField] private Button player2RepairRightArmButton;
    [SerializeField] private Button player2RepairLeftArmButton;
    [SerializeField] private Button playerRepairBodyButton;

    [SerializeField] private Text textBox;
    
    private int _currentPlayer = 1;
    private BattleState _currentBattleState = BattleState.BattleStart;
    
    private enum BattleState
    {
        BattleStart,
        TurnStart,
        CommandWaiting,
        CommandResult,
        BattleEnd,
    }

    private void Update()
    {
        switch (_currentBattleState)
        {
            case BattleState.BattleStart:
                break;
            case BattleState.TurnStart:
                break;
            case BattleState.CommandWaiting:
                break;
            case BattleState.CommandResult:
                break;
            case BattleState.BattleEnd:
                break;
        }
    }

    private void StartTurn()
    {
        textBox.text = string.Format(ConstValue.TurnStartMessage, _currentPlayer);
        _currentBattleState = BattleState.CommandWaiting;
    }
}
