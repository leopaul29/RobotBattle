using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Robot
{
    public enum Result
    {
        Critical,
        Normal,
        Fumble,
    }
    public class AttackResult
    {
        public Result Result;
        public int Damage;
        public bool IsJustWeaponBroken;
        public bool IsJustBodyBroken;
    }
    
    public class RobotParameter
    {
        public int Hp;
        public int BodyBrokenPoint;
        public Weapon RightWeapon;
        public Weapon LeftWeapon;
    }

    private int _hp;
    private int _bodyBrokenPoint;
    private int _defaultBodyBrokenPoint;
    private Weapon _rightWeapon;
    private Weapon _leftWeapon;
    public bool isBodyBroken { get; private set; }

    public Robot(RobotParameter robotParameter)
    {
        _hp = robotParameter.Hp;
        _bodyBrokenPoint = robotParameter.BodyBrokenPoint;
        _defaultBodyBrokenPoint = robotParameter.BodyBrokenPoint;
        _rightWeapon = robotParameter.RightWeapon;
        _leftWeapon = robotParameter.LeftWeapon;
    }
    public AttackResult Attack(BattleManager.BattleCommandType battleCommandType, Robot defenderRobot)
    {
        var weapon = battleCommandType == BattleManager.BattleCommandType.AttackRightArm ? _rightWeapon : _leftWeapon;
        var damage = weapon.Damage;

        var isJustWeaponBroken = false;
        if (!weapon.IsBroken)
        {
            isJustWeaponBroken = DrawLots(weapon.BrokenPoint);
            if (isJustWeaponBroken)
            {
                weapon.Break();
            }
        }

        var isJustBodyBroken = defenderRobot.Damage(damage);
        return new AttackResult
        {
            Damage = damage,
            Result = Result.Normal,
            IsJustWeaponBroken = isJustWeaponBroken,
            IsJustBodyBroken = isJustBodyBroken
        };
    }

    public bool Damage(int damage)
    {
        _hp = Math.Max(0, _hp - damage);
        if (isBodyBroken)
        {
            return false;
        }
        
        isBodyBroken = DrawLots(_bodyBrokenPoint);
        if (!isBodyBroken)
        {
            _bodyBrokenPoint--;
        }

        return isBodyBroken;
    }

    private bool DrawLots(int point)
    {
        return Random.Range(1, point + 1) == 1;
    }

    public void Repair(BattleManager.BattleCommandType battleCommandType)
    {
        switch (battleCommandType)
        {
            case BattleManager.BattleCommandType.RepairRightArm:
                _rightWeapon.Repair();
                break;
            case BattleManager.BattleCommandType.RepairLeftArm:
                _leftWeapon.Repair();
                break;
            case BattleManager.BattleCommandType.RepairBody:
                isBodyBroken = false;
                _bodyBrokenPoint = _defaultBodyBrokenPoint;
                break;
        }
    }
}
