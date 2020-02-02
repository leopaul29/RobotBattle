using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private BattleCommandButton player1AttackRightArmButton;
    [SerializeField] private BattleCommandButton player1AttackLeftArmButton;
    [SerializeField] private BattleCommandButton player1RepairRightArmButton;
    [SerializeField] private BattleCommandButton player1RepairLeftArmButton;
    [SerializeField] private BattleCommandButton player1RepairBodyButton;
    
    [SerializeField] private BattleCommandButton player2AttackRightArmButton;
    [SerializeField] private BattleCommandButton player2AttackLeftArmButton;
    [SerializeField] private BattleCommandButton player2RepairRightArmButton;
    [SerializeField] private BattleCommandButton player2RepairLeftArmButton;
    [SerializeField] private BattleCommandButton player2RepairBodyButton;

    [SerializeField] private Text player1Text;
    [SerializeField] private Text player2Text;
    [SerializeField] private Text player1Hp;
    [FormerlySerializedAs("player2HP")] [SerializeField] private Text player2Hp;
    
    private readonly MessageBuilder _messageBuilder = new MessageBuilder();
    private Robot _player1Robot;
    private Robot _player2Robot;
//    private int _currentPlayer = 1;
//    private BattleState _currentBattleState = BattleState.BattleStart;

    private enum BattleState
    {
        BattleStart,
        DuringBattle,
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
        player1AttackRightArmButton.ButtonObject.OnClickAsObservable().Subscribe(x => OnClickButton(BattleCommandType.AttackRightArm, 1));
        player1AttackLeftArmButton.ButtonObject.OnClickAsObservable().Subscribe(x => OnClickButton(BattleCommandType.AttackLeftArm, 1));
        player1RepairRightArmButton.ButtonObject.OnClickAsObservable().Subscribe(x => OnClickButton(BattleCommandType.RepairRightArm, 1));
        player1RepairLeftArmButton.ButtonObject.OnClickAsObservable().Subscribe(x => OnClickButton(BattleCommandType.RepairLeftArm, 1));
        player1RepairBodyButton.ButtonObject.OnClickAsObservable().Subscribe(x => OnClickButton(BattleCommandType.RepairBody, 1));
        
        player2AttackRightArmButton.ButtonObject.OnClickAsObservable().Subscribe(x => OnClickButton(BattleCommandType.AttackRightArm, 2));
        player2AttackLeftArmButton.ButtonObject.OnClickAsObservable().Subscribe(x => OnClickButton(BattleCommandType.AttackLeftArm, 2));
        player2RepairRightArmButton.ButtonObject.OnClickAsObservable().Subscribe(x => OnClickButton(BattleCommandType.RepairRightArm, 2));
        player2RepairLeftArmButton.ButtonObject.OnClickAsObservable().Subscribe(x => OnClickButton(BattleCommandType.RepairLeftArm, 2));
        player2RepairBodyButton.ButtonObject.OnClickAsObservable().Subscribe(x => OnClickButton(BattleCommandType.RepairBody, 2));
        
        InitBattle();
    }

    private void Update()
    {
//        switch (_currentBattleState)
//        {
//            case BattleState.BattleStart:
//                InitBattle();
//                _currentBattleState = BattleState.DuringBattle;
//                break;
//            case BattleState.TurnStart:
//                StartTurn();
//                break;
//            case BattleState.CommandWaiting:
//                break;
//            case BattleState.CommandResult:
////                _currentPlayer = _currentPlayer == 1 ? 2 : 1; 
//                _currentBattleState = BattleState.TurnStart;
//                break;
//            case BattleState.BattleEnd:
//                break;
//        }
    }

    private void InitBattle()
    { 
        player1AttackRightArmButton.SetColor(ConstValue.ButtonBlue);
        player1AttackLeftArmButton.SetColor(ConstValue.ButtonBlue);
        player1RepairRightArmButton.SetColor(ConstValue.ButtonGray);
        player1RepairLeftArmButton.SetColor(ConstValue.ButtonGray);
        player1RepairBodyButton.SetColor(ConstValue.ButtonGray);

        player2AttackRightArmButton.SetColor(ConstValue.ButtonBlue);
        player2AttackLeftArmButton.SetColor(ConstValue.ButtonBlue);
        player2RepairRightArmButton.SetColor(ConstValue.ButtonGray);
        player2RepairLeftArmButton.SetColor(ConstValue.ButtonGray);
        player2RepairBodyButton.SetColor(ConstValue.ButtonGray);

        var rightWeaponParameter = new Weapon.WeaponParameter
        {
         Damage = 10,
         BrokenPoint = 4
        };
        var leftWeaponParameter = new Weapon.WeaponParameter
        {
         Damage = 15,
         BrokenPoint = 2
        };
        var robot1Parameter = new Robot.RobotParameter
        {
         Player = 1,
         Hp = 100,
         BodyBrokenPoint = 3,
         RightWeapon = new Weapon(rightWeaponParameter),
         LeftWeapon = new Weapon(leftWeaponParameter),
        };
        var robot2Parameter = new Robot.RobotParameter
        {
         Player = 2,
         Hp = 100,
         BodyBrokenPoint = 3,
         RightWeapon = new Weapon(rightWeaponParameter),
         LeftWeapon = new Weapon(leftWeaponParameter),
        };
        _player1Robot = new Robot(robot1Parameter);
        _player2Robot = new Robot(robot2Parameter);
        UpdateDamageValue();
    }


//    private void StartTurn()
//    {
//        var playerText = _currentPlayer == 1 ? player1Text : player2Text;
//        playerText.text = string.Format(MessageBuilder.TurnStartMessage, _currentPlayer);
//        UpdateDamageValue();
//        _currentBattleState = BattleState.CommandWaiting;
//    }

    private void UpdateDamageValue()
    {
        player1AttackRightArmButton.SetDamageText(_player1Robot.CalculateDamage(BattleCommandType.AttackRightArm, _player2Robot).ToString());
        player1AttackLeftArmButton.SetDamageText(_player1Robot.CalculateDamage(BattleCommandType.AttackLeftArm, _player2Robot).ToString());
        player2AttackRightArmButton.SetDamageText(_player2Robot.CalculateDamage(BattleCommandType.AttackRightArm, _player1Robot).ToString());
        player2AttackLeftArmButton.SetDamageText(_player2Robot.CalculateDamage(BattleCommandType.AttackLeftArm, _player1Robot).ToString());
    }

    private void OnClickButton(BattleCommandType battleCommandType, int player)
    {
//        if (_currentBattleState != BattleState.CommandWaiting)
//        {
//            return;
//        }
//        
//        if((_currentPlayer == 1 && player == 2) || (_currentPlayer == 2 && player == 1 ))
//        {
//            return;
//        }
//        
//        _currentBattleState = BattleState.CommandResult;
        
        var attackerRobot = player == 1 ? _player1Robot : _player2Robot;
        var defenderRobot = player == 1 ? _player2Robot : _player1Robot;

        var playerText = player == 1 ? player1Text : player2Text;
        BattleCommandButton battleCommandButton;
        switch (battleCommandType)
        {
            case BattleCommandType.AttackRightArm:
            case BattleCommandType.AttackLeftArm:
                var attackResult = attackerRobot.Attack(battleCommandType, defenderRobot);
                playerText.text = _messageBuilder.GetAttackMessage(player, battleCommandType, attackResult);
                ChangeHp(player, defenderRobot);
                ChangeButtonColorAfterAttack(battleCommandType, attackResult, player);
                break;
            case BattleCommandType.RepairRightArm:
            case BattleCommandType.RepairLeftArm:
            case BattleCommandType.RepairBody:
                var repairResult = attackerRobot.Repair(battleCommandType);
                playerText.text = _messageBuilder.GetRepairMessage(battleCommandType, repairResult);
                ChangeRepairButtonColor(repairResult, battleCommandType, player);
                break;
        }
        UpdateDamageValue();
    }

    private void ChangeHp(int player, Robot defenderRobot)
    {
        (player == 1 ? player2Hp : player1Hp).text = $"HP:{defenderRobot.Hp.ToString()}";
    }

    private void ChangeButtonColorAfterAttack(BattleCommandType battleCommandType, Robot.AttackResult attackResult,
        int player)
    {
        if (attackResult.IsJustWeaponBroken)
        {
            if (battleCommandType == BattleCommandType.AttackRightArm)
            {
                (player == 1 ? player1AttackRightArmButton : player2AttackRightArmButton).SetColor(ConstValue.ButtonRed);
                (player == 1 ? player1RepairRightArmButton : player2RepairRightArmButton).SetColor(ConstValue.ButtonGreen);
            }
            else
            {
                (player == 1 ? player1AttackLeftArmButton : player2AttackLeftArmButton).SetColor(ConstValue.ButtonRed);
                (player == 1 ? player1RepairLeftArmButton : player2RepairLeftArmButton).SetColor(ConstValue.ButtonGreen);
            }
        }

        if (attackResult.IsJustBodyBroken)
        {
            (player == 1 ? player2RepairBodyButton : player1RepairBodyButton).SetColor(ConstValue.ButtonGreen);
        }
    }

    private void ChangeRepairButtonColor(Robot.ResultType repairResult, BattleCommandType battleCommandType, int player)
    {
        switch (repairResult)
        {
            case Robot.ResultType.Critical:
                if (player == 1)
                {
                    player1RepairRightArmButton.SetColor(ConstValue.ButtonGray);
                    player1RepairLeftArmButton.SetColor(ConstValue.ButtonGray);
                    player1RepairBodyButton.SetColor(ConstValue.ButtonGray);
                    player1AttackLeftArmButton.SetColor(ConstValue.ButtonBlue);
                    player1AttackRightArmButton.SetColor(ConstValue.ButtonBlue);
                }
                else
                {
                    player2RepairRightArmButton.SetColor(ConstValue.ButtonGray);
                    player2RepairLeftArmButton.SetColor(ConstValue.ButtonGray);
                    player2RepairBodyButton.SetColor(ConstValue.ButtonGray);
                    player2AttackLeftArmButton.SetColor(ConstValue.ButtonBlue);
                    player2AttackRightArmButton.SetColor(ConstValue.ButtonBlue);
                }
                break;
            case Robot.ResultType.Normal:
                switch (battleCommandType)
                {
                    case BattleCommandType.RepairRightArm:
                        (player == 1 ? player1RepairRightArmButton : player2RepairRightArmButton).SetColor(ConstValue.ButtonGray);
                        (player == 1 ? player1AttackRightArmButton : player2AttackRightArmButton).SetColor(ConstValue.ButtonBlue);
                        break;
                    case BattleCommandType.RepairLeftArm:
                        (player == 1 ? player1RepairLeftArmButton : player2RepairLeftArmButton).SetColor(ConstValue.ButtonGray);
                        (player == 1 ? player1AttackLeftArmButton : player2AttackLeftArmButton).SetColor(ConstValue.ButtonBlue);
                        break;
                    case BattleCommandType.RepairBody:
                        (player == 1 ? player1RepairBodyButton : player2RepairBodyButton).SetColor(ConstValue.ButtonGray);
                        break;
                }
                break;
            case Robot.ResultType.Fumble:
                break;
        }
    }
}
