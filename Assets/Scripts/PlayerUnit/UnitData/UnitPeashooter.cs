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
        base.Init(data);
        if (data is PeashooterData peashooter)
        {
            attackDelay = peashooter.attackDelay;
            range = peashooter.range;
            StartCoroutine(AttackRoutine());
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