using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "WaveData", menuName = "Wave/WaveData", order = 1)]
public class WaveData : ScriptableObject
{
    public List<GameObject> enemyPrefabs;
    public List<UnitData> enemyDataList; // �� �����տ� �����ϴ� ���� ������
    public int enemyCount;
    public float spawnInterval;
}
