using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField] private UnitData[] unitDatas;

    public void SpawnUnit(int index, Vector3 position)
    {
        UnitData data = unitDatas[index];
        GameObject go = Instantiate(data.unitPrefab, position, Quaternion.identity);
        // Unit을 가져와서 다운캐스팅
        Unit unit = go.GetComponent<Unit>();

        if (unit is UnitPeashooter peashooterUnit)
        {
            peashooterUnit.Init(data); // 오버라이드된 Init() 호출됨
        }
        else
        {
            unit.Init(data); // 기본 Unit이면 기존 Init 호출
        }




    }
}
