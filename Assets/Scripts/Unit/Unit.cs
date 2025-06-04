using DesignPattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Unit : PooledObject, IDamagable
{

    [SerializeField] private UnitData testDataForEditorOnly;
    private bool _isInitialized = false;
    public Tile currentTile { get; set; }



    protected UnitData _data;
    protected int _currentHp;

    public Faction faction;



    private void Start()
    {
//#if UNITY_EDITOR
//        if (!_isInitialized && testDataForEditorOnly != null)
//        {
//            Init(testDataForEditorOnly);
//            Debug.Log($"[Unit] 테스트용 데이터로 Init 실행: {_data.unitName}");
//        }
//#endif
    }


    private void Update()
    {

    }


    public virtual void Init(UnitData data)
    {
        Debug.Log("[Unit] Init 호출됨");
        _data = data;
        _currentHp = data.maxHp;
        //Debug.Log($"[Init] {data.unitName} 체력 초기화: {_currentHp}");
        _isInitialized = true;
    }


    public virtual void TakeDamage(int damage)
    {
        _currentHp -= damage;
        //Debug.Log($"[Unit] 피해 {damage} → 남은 체력 {_currentHp}");

        if (_currentHp <= 0)
            Die();
    }

    protected virtual void Die()
    {
        Debug.Log($"[Unit] 사망: {gameObject.name}");

        // 공격 대상에서 빠져야 함
        //UnitManager.Instance?.RemoveUnit(this);

        // 타일 비우기
        if (currentTile != null)
        {
            currentTile.isOccupied = false;
            currentTile = null;
        }



        // 콜라이더 꺼서 더 이상 물리 반응 없게
        foreach (var col in GetComponentsInChildren<Collider>())
            col.enabled = false;

        // 필요 시 이펙트나 사운드
        // PlayDeathEffect();

        // 일정 시간 뒤 오브젝트 제거 또는 풀로 반환
        Destroy(gameObject, 1.5f);

        // 또는 풀링 시스템 쓸 경우엔 그냥 아래 처리
        // gameObject.SetActive(false);
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