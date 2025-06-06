using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieUnit : Unit
{
    [SerializeField] private float moveSpeed = 1.5f;
    [SerializeField] private float attackRange = 0.6f;
    [SerializeField] private float attackDelay = 1.5f;
    [SerializeField] private float laneTolerance = 1f; // 라인 허용 오차 (Z축)
    [SerializeField] private float detectRange = 2f;

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
                return; // 공격 중이므로 이동 중단
            }

            // 타겟이 뒤에 있거나 너무 멀면 무시
            if (IsTargetOutOfRange())
            {
                target = null;
            }
            else
            {
                return;
            }
        }

        MoveForward();
    }


    private void MoveForward()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    private void FindTargetInSameLane()
    {
        Unit[] allUnits = FindObjectsOfType<Unit>();

        foreach (var unit in allUnits)
        {
            if (unit == this || unit.faction == this.faction || !unit.IsAlive())
                continue;

            float zDiff = Mathf.Abs(transform.position.z - unit.transform.position.z);
            float xDiff = transform.position.x - unit.transform.position.x;

            if (zDiff <= laneTolerance && xDiff >= 0f && xDiff <= detectRange)
            {
                target = unit;
                break;
            }
        }
    }

    private bool IsTargetInRange()
    {
        float xDist = transform.position.x - target.transform.position.x;
        //Debug.Log($"[거리 체크] xDist = {xDist}");
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
            Debug.Log($"[Zombie] {target.GetUnitName()} 공격함");

            if (!target.IsAlive())
            {
                target = null; // 사망 처리
            }
        }
    }
}

