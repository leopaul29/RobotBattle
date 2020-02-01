using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageBuilder
{
    public const string TurnStartMessage = "プレイヤー{0}のターン！";
    private const string AttacktMessage = "プレイヤー{0}の攻撃！";

    private const string AttackRightArmMessage = "右の武器で攻撃した！";
//    public const string TurnStartMessage = "プレイヤー{0}のターン！";
//    public const string TurnStartMessage = "プレイヤー{0}のターン！";

    public string GetAttackMessage(int currentPlayer, BattleManager.BattleCommandType battleCommandType)
    {
        var result = string.Empty;
        switch (battleCommandType)
        {
            case BattleManager.BattleCommandType.AttackRightArm:
                result = string.Format(AttacktMessage, currentPlayer) + Environment.NewLine + AttackRightArmMessage;
                break;
        }

        return result;
    }
}
