using DesignPattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Unit : PooledObject, IDamagable
{

    [SerializeField] private UnitData testDataForEditorOnly;
    private bool _isInitialized = false;




    protected UnitData _data;
    protected int _currentHp;

    public Faction faction;



    private void Start()
    {
#if UNITY_EDITOR
        if (!_isInitialized && testDataForEditorOnly != null)
        {
            Init(testDataForEditorOnly);
            Debug.Log($"[Unit] 테스트용 데이터로 Init 실행: {_data.unitName}");
        }
#endif
    }


    private void Update()
    {
        // 공격, 탐지 등 구현 가능
    }


    public virtual void Init(UnitData data)
    {
        Debug.Log("[Unit] Init 호출됨");
        _data = data;
        _currentHp = data.maxHp;
        Debug.Log($"[Init] {data.unitName} 체력 초기화: {_currentHp}");
        _isInitialized = true;
    }


    public virtual void TakeDamage(int damage)
    {
        _currentHp -= damage;
        Debug.Log($"[Unit] 피해 {damage} → 남은 체력 {_currentHp}");

        if (_currentHp <= 0)
            Die();
    }

    protected virtual void Die()
    {
        Debug.Log($"[Unit] 사망: {gameObject.name}");
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

    public bool IsAlive()
    {
        return gameObject.activeInHierarchy && _currentHp > 0;
    }

}

public enum Faction
{
    Player,
    Enemy
}