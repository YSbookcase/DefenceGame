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
            Invoke(nameof(Explode), 1.0f); // 1�� �� ����
        }
    }

    private void Explode()
    {
        // ���� �� �� Ž�� �� ������
        Debug.Log("[CherryBomb] ���� �߻�");
        Collider[] hits = Physics.OverlapSphere(transform.position, 2.0f);
        foreach (var hit in hits)
        {
            if (hit.TryGetComponent(out IInteraction target))
            {
                target.IInteraction(gameObject, 999);
            }
        }
        Destroy(gameObject); // �Ǵ� ReturnPool()
    }
}
