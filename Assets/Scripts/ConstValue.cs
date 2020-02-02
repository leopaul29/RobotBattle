using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ConstValue
{
    public static readonly Color ButtonBlue = new Color(0, 140f/255f, 255f/255f);
    public static readonly Color ButtonRed = new Color(250f/255f, 25f/255f, 25f/255f);
    public static readonly Color ButtonGray = new Color(187f/255f, 187f/255f, 187f/255f);
    public static readonly Color ButtonGreen = new Color(3f/255f, 255f/255f, 0f/255f);

    public static readonly float BrokenWeaponDamageRate = 0.5f;
    public static readonly float BrokenBodyDamageRate = 1.5f;

    public static readonly int AttackCriticalRate = 10;
    public static readonly int AttackFumbleRate = 7;
    public static readonly int RepairCriticalRate = 0;
    public static readonly int RepairFumbleRate = 0;
    
    public static readonly float CriticalDamageRate = 2.0f;

    //public static readonly float EnergyRecoveryRate = 0.00666f;
    public static readonly float EnergyRecoveryRate = 3f;
}
