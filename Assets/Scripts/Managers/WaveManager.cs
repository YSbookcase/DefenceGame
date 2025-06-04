using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPattern;
using UnityEngine.SceneManagement;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private List<WaveData> waveList;

    [SerializeField] private float[] lineZPositions = { 15f, 12f, 9f, 6f, 3f };
    [SerializeField] private float spawnX = 27f;
    [SerializeField] private float spawnY = 0f;

    private int currentWave = 0;
    private bool isSpawning = false;

    private Coroutine waveCoroutine;

    public void StartWaves()
    {
        Debug.Log("StartWaves ȣ���");
        Debug.Log($"waveList.Count = {waveList.Count}");

        if (!isSpawning)
        {
            waveCoroutine = StartCoroutine(SpawnWaveCoroutine());
        }
    }

    public void StopWaves()
    {
        if (waveCoroutine != null)
        {
            StopCoroutine(waveCoroutine);
            waveCoroutine = null;
            Debug.Log("[WaveManager] ���̺� �ߴܵ�");
        }

        isSpawning = false;
    }

    public void ResetState()
    {
        StopWaves(); // ��� �ڷ�ƾ ����
        currentWave = 0;
        Debug.Log("[WaveManager] ���� �ʱ�ȭ �Ϸ�");
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
        StartCoroutine(CheckVictoryCondition());
    }

    private void SpawnRandomEnemyLine(WaveData wave)
    {
        int lineIndex = Random.Range(0, lineZPositions.Length);
        float z = lineZPositions[lineIndex];
        Vector3 spawnPos = new Vector3(spawnX, spawnY, z);
        Quaternion rotation = Quaternion.Euler(0, -90, 0);

        // ���� ���� ����
        int enemyIndex = Random.Range(0, wave.enemyPrefabs.Count);
        GameObject prefab = wave.enemyPrefabs[enemyIndex];

        // ����
        GameObject go = Instantiate(prefab, spawnPos, rotation);
        Debug.Log($"Enemy ���� ����: {wave.enemyPrefabs.Count}");

        // Init ȣ��
        ZombieUnit unit = go.GetComponent<ZombieUnit>();
        if (unit != null && wave.enemyDataList != null && wave.enemyDataList.Count > enemyIndex)
        {
            unit.Init(wave.enemyDataList[enemyIndex]);
        }
        else
        {
            Debug.LogWarning($"[WaveManager] Init ������: enemyIndex={enemyIndex}, ������ ���� Ȯ�� �ʿ�");
        }
    }

    private IEnumerator CheckVictoryCondition()
    {
        while (true)
        {
            ZombieUnit[] enemies = FindObjectsOfType<ZombieUnit>();

            if (enemies.Length == 0)
            {
                Debug.Log("[WaveManager] ��� �� ���ŵ� �� �¸� ó��");
                GameManager.Instance.Victory();
                yield break;
            }

            yield return new WaitForSeconds(1f); // �ֱ������� �˻�
        }
    }


}
