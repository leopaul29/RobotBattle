using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
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
    [SerializeField] private Button player2RepairBodyButton;

    [SerializeField] private Text player1Text;
    [SerializeField] private Text player2Text;
    
    private readonly MessageBuilder _messageBuilder = new MessageBuilder();
    private Robot _player1Robot;
    private Robot _player2Robot;
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

    public enum BattleCommandType
    {
        AttackRightArm,
        AttackLeftArm,
        RepairRightArm,
        RepairLeftArm,
        RepairBody,
    }

    private void Start()
    {
        player1AttackRightArmButton.OnClickAsObservable().Subscribe(x => OnClickButton(BattleCommandType.AttackRightArm, 1));
        player1AttackLeftArmButton.OnClickAsObservable().Subscribe(x => OnClickButton(BattleCommandType.AttackLeftArm, 1));
        player1RepairRightArmButton.OnClickAsObservable().Subscribe(x => OnClickButton(BattleCommandType.RepairRightArm, 1));
        player1RepairLeftArmButton.OnClickAsObservable().Subscribe(x => OnClickButton(BattleCommandType.RepairLeftArm, 1));
        player1RepairBodyButton.OnClickAsObservable().Subscribe(x => OnClickButton(BattleCommandType.RepairBody, 1));
        
        player2AttackRightArmButton.OnClickAsObservable().Subscribe(x => OnClickButton(BattleCommandType.AttackRightArm, 2));
        player2AttackLeftArmButton.OnClickAsObservable().Subscribe(x => OnClickButton(BattleCommandType.AttackLeftArm, 2));
        player2RepairRightArmButton.OnClickAsObservable().Subscribe(x => OnClickButton(BattleCommandType.RepairRightArm, 2));
        player2RepairLeftArmButton.OnClickAsObservable().Subscribe(x => OnClickButton(BattleCommandType.RepairLeftArm, 2));
        player2RepairBodyButton.OnClickAsObservable().Subscribe(x => OnClickButton(BattleCommandType.RepairBody, 2));
        var weaponParameter = new Weapon.WeaponParameter
        {
            Damage = 10,
            BrokenPoint = 2
        };
        var robotParameter = new Robot.RobotParameter
        {
            Hp = 100,
            BodyBrokenPoint = 3,
            RightWeapon = new Weapon(weaponParameter),
            LeftWeapon = new Weapon(weaponParameter),
        };
        _player1Robot = new Robot(robotParameter);
        _player2Robot = new Robot(robotParameter);
    }

    private void Update()
    {
        switch (_currentBattleState)
        {
            case BattleState.BattleStart:
                _currentBattleState = BattleState.TurnStart;
                break;
            case BattleState.TurnStart:
                StartTurn();
                break;
            case BattleState.CommandWaiting:
                break;
            case BattleState.CommandResult:
                _currentPlayer = _currentPlayer == 1 ? 2 : 1; 
                _currentBattleState = BattleState.TurnStart;
                break;
            case BattleState.BattleEnd:
                break;
        }
    }

    private void StartTurn()
    {
        var playerText = _currentPlayer == 1 ? player1Text : player2Text;
        playerText.text = string.Format(MessageBuilder.TurnStartMessage, _currentPlayer);
        _currentBattleState = BattleState.CommandWaiting;
    }

    private void OnClickButton(BattleCommandType battleCommandType, int player)
    {
        if (_currentBattleState != BattleState.CommandWaiting)
        {
            return;
        }
        
        if((_currentPlayer == 1 && player == 2) || (_currentPlayer == 2 && player == 1 ))
        {
            return;
        }
        
        _currentBattleState = BattleState.CommandResult;
        
        var attackerRobot = _currentPlayer == 1 ? _player1Robot : _player2Robot;
        var defenderRobot = _currentPlayer == 1 ? _player2Robot : _player1Robot;

        var playerText = _currentPlayer == 1 ? player1Text : player2Text;
        switch (battleCommandType)
        {
            case BattleCommandType.AttackRightArm:
            case BattleCommandType.AttackLeftArm:
                var attackResult = attackerRobot.Attack(battleCommandType, defenderRobot);
                playerText.text = _messageBuilder.GetAttackMessage(_currentPlayer, battleCommandType, attackResult);
                break;
            case BattleCommandType.RepairRightArm:
            case BattleCommandType.RepairLeftArm:
            case BattleCommandType.RepairBody:
                attackerRobot.Repair(battleCommandType);
                playerText.text = _messageBuilder.GetRepairMessage(battleCommandType);
                break;
        }
    }
}
