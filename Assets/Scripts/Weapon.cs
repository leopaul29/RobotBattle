using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Weapon
{
    private readonly int _damage;
    public int BrokenPoint;
    private readonly int _defaultBrokenPoint;
    public bool IsBroken { get; private set; }

    public int DamageValue => IsBroken ? (int)(_damage * ConstValue.BrokenWeaponDamageRate) : _damage;

    public class WeaponParameter
    {
        public int Damage;
        public int BrokenPoint;
    }

    public  Weapon(WeaponParameter weaponParameter)
    {
        _damage = weaponParameter.Damage;
        _defaultBrokenPoint = BrokenPoint = weaponParameter.BrokenPoint;
    }
    public void Break()
    {
        IsBroken = true;
    }

    public void Repair()
    {
        IsBroken = false;
        BrokenPoint = _defaultBrokenPoint;
    }
}
