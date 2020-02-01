using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageBuilder
{
    public const string TurnStartMessage = "プレイヤー{0}のターン！";
    private const string AttackMessage = "プレイヤー{0}の攻撃！";
    private const string AttackWeaponMessage = "{0}の武器で攻撃した！";
    private const string AttackDamageMessage = "{0}のダメージ！";
    private const string AttackNoDamageMessage = "ダメージを与えられなかった！";
    private const string AttackWeaponBrokenMessage = "自分の武器が壊れた！";
    private const string AttackBodyBrokenMessage = "相手のボディが壊れた！";
    
//    public const string TurnStartMessage = "プレイヤー{0}のターン！";
//    public const string TurnStartMessage = "プレイヤー{0}のターン！";

    public string GetAttackMessage(int currentPlayer, BattleManager.BattleCommandType battleCommandType, Robot.AttackResult attackResult)
    {
        var result = string.Empty;
        switch (battleCommandType)
        {
            case BattleManager.BattleCommandType.AttackRightArm:
            case BattleManager.BattleCommandType.AttackLeftArm:
                result = string.Format(AttackMessage, currentPlayer) 
                    + Environment.NewLine
                    +  string.Format(AttackWeaponMessage, battleCommandType == BattleManager.BattleCommandType.AttackRightArm ? "右" : "左")
                    + Environment.NewLine
                    + (attackResult.Damage == 0 ? AttackNoDamageMessage : string.Format(AttackDamageMessage, attackResult.Damage))
                    + (attackResult.IsJustWeaponBroken ? Environment.NewLine + AttackWeaponBrokenMessage : string.Empty)
                    + (attackResult.IsJustBodyBroken ? Environment.NewLine + AttackBodyBrokenMessage : string.Empty )
                    ;
                break;
        }

        return result;
    }
}
