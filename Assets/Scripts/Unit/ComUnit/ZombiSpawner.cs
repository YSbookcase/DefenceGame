using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [System.Serializable]
    public class SpawnData
    {
        public UnitData zombieData;
        public float spawnTime;
        public Vector3 spawnPosition;
    }

    [SerializeField] private SpawnData[] spawnSchedule;

    private float timer = 0f;
    private int spawnIndex = 0;

    private void Update()
    {
        timer += Time.deltaTime;

        // 스케줄에 따라 좀비 생성
        while (spawnIndex < spawnSchedule.Length && timer >= spawnSchedule[spawnIndex].spawnTime)
        {
            SpawnZombie(spawnSchedule[spawnIndex]);
            spawnIndex++;
        }
    }

    private void SpawnZombie(SpawnData data)
    {
        GameObject go = Instantiate(data.zombieData.unitPrefab, data.spawnPosition, Quaternion.identity);
        Unit unit = go.GetComponent<Unit>();

        if (unit is ZombieUnit zombie)
            zombie.Init(data.zombieData);
        else
            unit.Init(data.zombieData);
    }
}
