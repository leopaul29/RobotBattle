using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Weapon
{
    public int Damage { get; private set; }
    public int BrokenPoint;
    private int _defaultBrokenPoint;
    public bool IsBroken { get; private set; }

    public class WeaponParameter
    {
        public int Damage;
        public int BrokenPoint;
    }

    public  Weapon(WeaponParameter weaponParameter)
    {
        Damage = weaponParameter.Damage;
        _defaultBrokenPoint = BrokenPoint = weaponParameter.BrokenPoint;
    }

    public void Break()
    {
        IsBroken = true;
        BrokenPoint = _defaultBrokenPoint;
    }
}
