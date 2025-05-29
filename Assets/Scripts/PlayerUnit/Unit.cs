using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Unit : MonoBehaviour
{

    private UnitData _data;
    private int _currentHp;

    public void Init(UnitData data)
    {
        _data = data;
        _currentHp = data.maxHp;
    }

    private void Update()
    {
        // 공격, 탐지 등 구현 가능
    }

    public void TakeDamage(int damage)
    {
        _currentHp -= damage;
        if (_currentHp <= 0)
            Die();
    }

    private void Die()
    {
        // ReturnPool(), Destroy, 이펙트 등
        gameObject.SetActive(false);
    }

}
