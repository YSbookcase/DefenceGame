using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCherryBomb : Unit
{
    public override void Init(UnitData data)
    {
        base.Init(data);
        if (data is CherryBombData cherry)
        {
            Invoke(nameof(Explode), 1.0f); // 1초 후 폭발
        }
    }

    private void Explode()
    {
        // 범위 내 적 탐지 후 데미지
        Debug.Log("[CherryBomb] 폭발 발생");
        Collider[] hits = Physics.OverlapSphere(transform.position, 2.0f);
        foreach (var hit in hits)
        {
            if (hit.TryGetComponent(out IInteraction target))
            {
                target.IInteraction(gameObject, 999);
            }
        }
        Destroy(gameObject); // 또는 ReturnPool()
    }
}
