using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieUnit : Unit
{
    [SerializeField] private float moveSpeed = 1.5f;
    [SerializeField] private float attackRange = 1.0f;
    [SerializeField] private float attackDelay = 1.5f;
    [SerializeField] private float laneTolerance = 0.1f; // ���� ��� ���� (Y��)

    private float attackTimer;
    private Unit target;

    private void Update()
    {
        attackTimer += Time.deltaTime;

        if (target == null || !target.IsAlive())
        {
            FindTargetInSameLane();
        }

        if (target != null)
        {
            if (IsTargetInRange())
            {
                TryAttack();
                return; // ���� ���̹Ƿ� �̵� �ߴ�
            }

            // Ÿ���� �ڿ� �ְų� �ʹ� �ָ� ����
            if (IsTargetOutOfRange())
            {
                target = null;
            }
        }

        MoveForward();
    }


    private void MoveForward()
    {
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
    }

    private void FindTargetInSameLane()
    {
        Unit[] allUnits = FindObjectsOfType<Unit>();

        foreach (var unit in allUnits)
        {
            if (unit == this || unit.faction == this.faction || !unit.IsAlive())
                continue;

            float yDiff = Mathf.Abs(transform.position.y - unit.transform.position.y);
            float xDiff = transform.position.x - unit.transform.position.x;

            if (yDiff <= laneTolerance && xDiff >= 0f && xDiff <= attackRange)
            {
                target = unit;
                break;
            }
        }
    }

    private bool IsTargetInRange()
    {
        float xDist = transform.position.x - target.transform.position.x;
        return xDist >= 0f && xDist <= attackRange;
    }

    private bool IsTargetOutOfRange()
    {
        float xDist = transform.position.x - target.transform.position.x;
        return xDist < 0f || xDist > attackRange;
    }

    private void TryAttack()
    {
        if (attackTimer >= attackDelay)
        {
            Attack();
            attackTimer = 0f;
        }


    }



    private void Attack()
    {
    
        if (target != null && target.IsAlive())
        {
            target.TakeDamage(GetAttackPower());
            Debug.Log($"[Zombie] {target.GetUnitName()} ������");

            if (!target.IsAlive())
            {
                target = null; // ��� ó��
            }
        }
    }
}

