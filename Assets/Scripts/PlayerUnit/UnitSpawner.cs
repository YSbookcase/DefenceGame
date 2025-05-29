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
        Unit unit = go.GetComponent<Unit>();
        unit.Init(data);

        if (data is PeashooterData peashooter)
        {
            Debug.Log($"ÃÑ¾Ë ¼Óµµ: {peashooter.bulletSpeed}");
        }


    }
}
