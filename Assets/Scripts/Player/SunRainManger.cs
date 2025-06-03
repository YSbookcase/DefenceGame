using UnityEngine;

public class SunRainManager : MonoBehaviour
{
    [SerializeField] private GameObject sunEnergyPrefab; // SunEnergy 프리팹
    [SerializeField] private float spawnInterval = 4f;
    [SerializeField] private float spawnY = 8f; // 하늘 높이
    [SerializeField] private float minX = -1f;
    [SerializeField] private float maxX = 25f;
    //[SerializeField] private float z = 8f;

    private bool isRaining = false;

    public void StartRain()
    {
        if (isRaining) return;

        isRaining = true;
        InvokeRepeating(nameof(SpawnSunEnergy), 0f, spawnInterval);
    }

    private void SpawnSunEnergy()
    {
        float x = Random.Range(minX, maxX);
        Vector3 spawnPos = new Vector3(x, spawnY, spawnY);
        Debug.Log($"[SunRainManager] SunEnergy 생성 위치: {spawnPos}");
        Quaternion rotation = Quaternion.Euler(45f, 0f, 0f);
        Instantiate(sunEnergyPrefab, spawnPos, rotation);
    }
}
