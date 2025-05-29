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

}
