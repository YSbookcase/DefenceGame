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
        Quaternion rotation = Quaternion.Euler(0, 90, 0);
        GameObject go = Instantiate(data.unitPrefab, pos, rotation);
        Unit unit = go.GetComponent<Unit>();
        unit.Init(data);
    }
}
