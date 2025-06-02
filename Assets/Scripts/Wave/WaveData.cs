using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "WaveData", menuName = "Wave/WaveData", order = 1)]
public class WaveData : ScriptableObject
{
    public List<GameObject> enemyPrefabs; // 여러 종류 대응
    public int enemyCount;
    public float spawnInterval;
}
