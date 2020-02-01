using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    }

    public int Hp { get; set; }
    private int _player;
    private int _bodyBrokenPoint;
    private int _defaultBodyBrokenPoint;
    private Weapon _rightWeapon;
    private Weapon _leftWeapon;
    public bool IsBodyBroken { get; private set; }

    public Robot(RobotParameter robotParameter)
    {
        Hp = robotParameter.Hp;
        _bodyBrokenPoint = robotParameter.BodyBrokenPoint;
        _defaultBodyBrokenPoint = robotParameter.BodyBrokenPoint;
        _rightWeapon = robotParameter.RightWeapon;
        _leftWeapon = robotParameter.LeftWeapon;
    }
    public AttackResult Attack(BattleManager.BattleCommandType battleCommandType, Robot defenderRobot)
    {
        var damage = CalculateDamage(battleCommandType, defenderRobot);
        var attackResult = CalculateCriticalDamage(damage);
        var isJustBodyBroken = defenderRobot.Damage(attackResult);
        
        var weapon = battleCommandType == BattleManager.BattleCommandType.AttackRightArm ? _rightWeapon : _leftWeapon;
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
                        break;
                    case BattleManager.BattleCommandType.RepairLeftArm:
                        _leftWeapon.Repair();
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
        IsBodyBroken = false;
        _bodyBrokenPoint = _defaultBodyBrokenPoint;
    }
}

