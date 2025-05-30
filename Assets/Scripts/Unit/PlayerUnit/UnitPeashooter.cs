using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPeashooter : Unit
{
    private float attackDelay;
    private float range;
    private float timer;

    public override void Init(UnitData data)
    {
        Debug.Log("[UnitPeashooter] Init »£√‚µ ");
        base.Init(data);
     
        if (data is PeashooterData peashooter)
        {
            attackDelay = peashooter.attackDelay;
            range = peashooter.range;

            // Weapon º≥¡§
            UnitWeapon weapon = GetComponent<UnitWeapon>();
            if (weapon != null)
            {
                weapon.SetWeaponStats(
                    peashooter.attackPower,
                    peashooter.bulletSpeed,
                    peashooter.attackDelay,
                    peashooter.range
                );
             
            }

           
        }
    }


}