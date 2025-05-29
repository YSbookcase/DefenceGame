using DesignPattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Unit : PooledObject, IDamagable
{
    
    private UnitData _data;
    private int _currentHp;

    public virtual void Init(UnitData data)
    {
        Debug.Log("[Unit] Init ȣ���");
        _data = data;
        _currentHp = data.maxHp;
    }

    private void Update()
    {
        // ����, Ž�� �� ���� ����
    }

    public void TakeDamage(int damage)
    {
        _currentHp -= damage;
        if (_currentHp <= 0)
            Die();
    }

    private void Die()
    {
        // ReturnPool(), Destroy, ����Ʈ ��
        gameObject.SetActive(false);
    }

    public string GetUnitName()
    {
        return _data != null ? _data.unitName : "(unknown)";
    }

    public int GetAttackPower()
    {
        return _data != null ? _data.attackPower : 0;
    }



}
