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
        //if (SceneManager.GetActiveScene().name == "MainGame") // ���� ���� �� �̸����� ��ü
        //{
        //    StartWaves(); // �Ǵ� WaveManager.Instance.StartWaves();
        //}
    }

    public void StartWaves()
    {
        Debug.Log("StartWaves ȣ���");
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
            Debug.Log($"���̺� {currentWave + 1} ����!");

            for (int i = 0; i < wave.enemyCount; i++)
            {
                SpawnRandomEnemyLine(wave);
                yield return new WaitForSeconds(wave.spawnInterval);
            }

            currentWave++;
            yield return new WaitForSeconds(3f);
        }

        isSpawning = false;
        Debug.Log("��� ���̺� �Ϸ�");
    }

    private void SpawnRandomEnemyLine(WaveData wave)
    {
      
        int lineIndex = Random.Range(0, lineZPositions.Length);
        float z = lineZPositions[lineIndex];
        Vector3 spawnPos = new Vector3(spawnX, spawnY, z);

        // ���� ���� ����
        int enemyIndex = Random.Range(0, wave.enemyPrefabs.Count);
        GameObject prefab = wave.enemyPrefabs[enemyIndex];

        Instantiate(prefab, spawnPos, Quaternion.identity);
        Debug.Log($"Enemy ���� ����: {wave.enemyPrefabs.Count}");

    }
}
