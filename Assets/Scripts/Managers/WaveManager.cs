using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPattern;
using UnityEngine.SceneManagement;

public class WaveManager : Singleton<WaveManager>
{
    [SerializeField] private List<WaveData> waveList;

    [SerializeField] private float[] lineZPositions = { 15f, 12f, 9f, 6f, 3f };
    [SerializeField] private float spawnX = 27f;
    [SerializeField] private float spawnY = 0f;

    private int currentWave = 0;
    private bool isSpawning = false;


    private void Start()
    {
        //if (SceneManager.GetActiveScene().name == "MainGame") // 메인 게임 씬 이름으로 교체
        //{
        //    StartWaves(); // 또는 WaveManager.Instance.StartWaves();
        //}
    }

    public void StartWaves()
    {
        Debug.Log("StartWaves 호출됨");
        Debug.Log($"waveList.Count = {waveList.Count}");
        if (!isSpawning)
            StartCoroutine(SpawnWaveCoroutine());
    }

    private IEnumerator SpawnWaveCoroutine()
    {
        isSpawning = true;

        while (currentWave < waveList.Count)
        {
            WaveData wave = waveList[currentWave];
            Debug.Log($"웨이브 {currentWave + 1} 시작!");

            for (int i = 0; i < wave.enemyCount; i++)
            {
                SpawnRandomEnemyLine(wave);
                yield return new WaitForSeconds(wave.spawnInterval);
            }

            currentWave++;
            yield return new WaitForSeconds(3f);
        }

        isSpawning = false;
        Debug.Log("모든 웨이브 완료");
    }

    private void SpawnRandomEnemyLine(WaveData wave)
    {
      
        int lineIndex = Random.Range(0, lineZPositions.Length);
        float z = lineZPositions[lineIndex];
        Vector3 spawnPos = new Vector3(spawnX, spawnY, z);

        // 몬스터 종류 선택
        int enemyIndex = Random.Range(0, wave.enemyPrefabs.Count);
        GameObject prefab = wave.enemyPrefabs[enemyIndex];

        Instantiate(prefab, spawnPos, Quaternion.identity);
        Debug.Log($"Enemy 종류 개수: {wave.enemyPrefabs.Count}");

    }
}
