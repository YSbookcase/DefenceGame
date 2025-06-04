using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "WaveData", menuName = "Wave/WaveData", order = 1)]
public class WaveData : ScriptableObject
{
    public List<GameObject> enemyPrefabs;
    public List<UnitData> enemyDataList; // 각 프리팹에 대응하는 유닛 데이터
    public int enemyCount;
    public float spawnInterval;
}
