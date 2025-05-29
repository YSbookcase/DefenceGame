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
            // Ÿ�� ���� �� �Ѿ� �߻� (��: BulletPool���� ������)
            Debug.Log("[Peashooter] ���� ����");
        }
    }
}