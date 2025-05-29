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
        Debug.Log("[UnitPeashooter] Init 호출됨");
        base.Init(data);
     
        if (data is PeashooterData peashooter)
        {
            attackDelay = peashooter.attackDelay;
            range = peashooter.range;

            // Weapon 설정
            UnitWeapon weapon = GetComponent<UnitWeapon>();
            if (weapon != null)
            {
                weapon.SetWeaponStats(
                    peashooter.attackPower,
                    peashooter.bulletSpeed,
                    peashooter.attackDelay
                );
                weapon.StartShooting();
            }

            StartCoroutine(AttackRoutine()); // 선택적
        }
    }

    private IEnumerator AttackRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(attackDelay);
            // 타겟 감지 후 총알 발사 (예: BulletPool에서 꺼내기)
            Debug.Log("[Peashooter] 공격 실행");
        }
    }
}