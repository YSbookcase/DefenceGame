using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "WaveData", menuName = "Wave/WaveData", order = 1)]
public class WaveData : ScriptableObject
{
    public List<GameObject> enemyPrefabs; // ���� ���� ����
    public int enemyCount;
    public float spawnInterval;
}
