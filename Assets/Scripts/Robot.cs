using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;

public class Robot
{
    public enum ResultType
    {
        Critical,
        Normal,
        Fumble,
    }
    public class AttackResult
    {
        public ResultType ResultType;
        public int Damage;
        public bool IsJustWeaponBroken;
        public bool IsJustBodyBroken;
    }
    
    public class RobotParameter
    {
        public int Player;
        public int Hp;
        public int BodyBrokenPoint;
        public Weapon RightWeapon;
        public Weapon LeftWeapon;
        public int EnergyToRepairBody;
    }

    public int Hp { get; set; }
    public bool IsDead => Hp <= 0;
    private int _player;
    private int _bodyBrokenPoint;
    private readonly int _defaultBodyBrokenPoint;
    private readonly Weapon _rightWeapon;
    private readonly Weapon _leftWeapon;
    private readonly int _energyToRepairBody;
    public float Energy { get; private set; }
    public bool IsBodyBroken { get; private set; }

    public Robot(RobotParameter robotParameter)
    {
        _player = robotParameter.Player;
        Hp = robotParameter.Hp;
        _bodyBrokenPoint = robotParameter.BodyBrokenPoint;
        _defaultBodyBrokenPoint = robotParameter.BodyBrokenPoint;
        _rightWeapon = robotParameter.RightWeapon;
        _leftWeapon = robotParameter.LeftWeapon;
        _energyToRepairBody = robotParameter.EnergyToRepairBody;
        Observable.EveryUpdate().Subscribe(_ =>
        {
            Energy = Math.Min((Energy + Time.deltaTime * ConstValue.EnergyRecoveryRate), 100f);
        });
    }
    public AttackResult Attack(BattleManager.BattleCommandType battleCommandType, Robot defenderRobot)
    {
        var damage = CalculateDamage(battleCommandType, defenderRobot);
        var attackResult = CalculateCriticalDamage(damage);
        var isJustBodyBroken = defenderRobot.Damage(attackResult);
        
        var weapon = battleCommandType == BattleManager.BattleCommandType.AttackRightArm ? _rightWeapon : _leftWeapon;
        Energy = Math.Max(0, Energy - weapon.EnergyToAttack);
        
        var isJustWeaponBroken = false;
        if (!weapon.IsBroken)
        {
            isJustWeaponBroken = DrawLots(weapon.BrokenPoint);
            if (isJustWeaponBroken)
            {
                weapon.Break();
            }
            else
            {
                weapon.BrokenPoint--;
            }
        }

        attackResult.IsJustWeaponBroken = isJustWeaponBroken;
        attackResult.IsJustBodyBroken = isJustBodyBroken;
        return attackResult;
    }

    private AttackResult CalculateCriticalDamage(int damage)
    {
        var result = new AttackResult();

        switch (DrawLotsCritical())
        {
            case ResultType.Critical:
                result.ResultType = ResultType.Critical;
                result.Damage = (int) (damage * ConstValue.CriticalDamageRate);
                break;
            case ResultType.Normal:
                result.ResultType = ResultType.Normal;
                result.Damage = damage;
                break;
            case ResultType.Fumble:
                result.ResultType = ResultType.Fumble;
                result.Damage = 0;
                break;
        }
        return result;
    }

    private ResultType DrawLotsCritical()
    {
        var value = Random.Range(1, 101);
        if (value <= ConstValue.AttackCriticalRate)
        {
            return ResultType.Critical;
        }
        else if (value <= ConstValue.AttackCriticalRate + ConstValue.AttackFumbleRate)
        {
            return ResultType.Fumble;
        }

        return ResultType.Normal;
    }
    public int CalculateDamage(BattleManager.BattleCommandType battleCommandType, Robot defenderRobot)
    {
        var weapon = battleCommandType == BattleManager.BattleCommandType.AttackRightArm ? _rightWeapon : _leftWeapon;
        var damage = weapon.DamageValue;
        damage = (int) (defenderRobot.IsBodyBroken ? damage * ConstValue.BrokenBodyDamageRate : damage);
        return damage;
    }
    
    private bool Damage(AttackResult attackResult)
    {
        if (attackResult.ResultType == ResultType.Fumble)
        {
            return false;
        }
        Hp = Math.Max(0, Hp - attackResult.Damage);
        if (IsBodyBroken)
        {
            return false;
        }
        
        IsBodyBroken = DrawLots(_bodyBrokenPoint);
        if (!IsBodyBroken)
        {
            _bodyBrokenPoint--;
        }

        return IsBodyBroken;
    }

    private bool DrawLots(int point)
    {
        return Random.Range(1, point + 1) == 1;
    }

    public ResultType Repair(BattleManager.BattleCommandType battleCommandType)
    {
        switch (DrawLotsRepairCritical())
        {
            case ResultType.Critical:
                _rightWeapon.Repair();
                _leftWeapon.Repair();
                RepairBody();
                return ResultType.Critical;
            case ResultType.Normal:
                switch (battleCommandType)
                {
                    case BattleManager.BattleCommandType.RepairRightArm:
                        _rightWeapon.Repair();
                        Energy = Math.Max(0, Energy - _rightWeapon.EnergyToRepair);
                        break;
                    case BattleManager.BattleCommandType.RepairLeftArm:
                        _leftWeapon.Repair();
                        Energy = Math.Max(0, Energy - _leftWeapon.EnergyToRepair);
                        break;
                    case BattleManager.BattleCommandType.RepairBody:
                        RepairBody();
                        break;
                }
                return ResultType.Normal;
            case ResultType.Fumble:
                return ResultType.Fumble;
        }
        return ResultType.Normal;
    }

    private ResultType DrawLotsRepairCritical()
    {
        var value = Random.Range(1, 101);
        if (value <= ConstValue.RepairCriticalRate)
        {
           return ResultType.Critical;
        }
        else if (value <= ConstValue.RepairCriticalRate + ConstValue.RepairFumbleRate)
        {
           return ResultType.Fumble;
        }

        return ResultType.Normal;
    }

    private void RepairBody()
    {
        Energy = Math.Max(0, Energy - _energyToRepairBody);
        IsBodyBroken = false;
        _bodyBrokenPoint = _defaultBodyBrokenPoint;
    }

    public bool CanExecuteCommand(BattleManager.BattleCommandType battleCommandType)
    {
        switch (battleCommandType)
        {
            case BattleManager.BattleCommandType.AttackRightArm:
                return _rightWeapon.EnergyToAttack <= Energy;
            case BattleManager.BattleCommandType.AttackLeftArm:
                return _leftWeapon.EnergyToAttack <= Energy ;
            case BattleManager.BattleCommandType.RepairRightArm:
                return _rightWeapon.EnergyToRepair <= Energy;
            case BattleManager.BattleCommandType.RepairLeftArm:
                return _leftWeapon.EnergyToRepair <= Energy;
            case BattleManager.BattleCommandType.RepairBody:
                return _energyToRepairBody <= Energy;
            default:
                return false;
        }
    }
}

