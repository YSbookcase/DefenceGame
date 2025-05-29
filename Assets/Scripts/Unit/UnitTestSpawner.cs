using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTestSpawner : MonoBehaviour
{
    [SerializeField] private UnitData testUnit;
    [SerializeField] private Vector3 spawnPosition = new Vector3(0, 0, 0);

    private void Start()
    {
        Spawn(testUnit, spawnPosition);
    }

    private void Spawn(UnitData data, Vector3 pos)
    {
        Quaternion rotation;

        if (data is PeashooterData)
            rotation = Quaternion.Euler(0, 90, 0); // 오른쪽을 향하게 (Peashooter가 왼쪽을 쏘게 하기 위해)
       
        else
            rotation = Quaternion.identity;

        GameObject go = Instantiate(data.unitPrefab, pos, rotation);
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
