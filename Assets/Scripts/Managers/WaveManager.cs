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
        Debug.Log("StartWaves 호출됨");
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
            Debug.Log("[WaveManager] 웨이브 중단됨");
        }

        isSpawning = false;
    }

    public void ResetState()
    {
        StopWaves(); // 모든 코루틴 정지
        currentWave = 0;
        Debug.Log("[WaveManager] 상태 초기화 완료");
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
        StartCoroutine(CheckVictoryCondition());
    }

    private void SpawnRandomEnemyLine(WaveData wave)
    {
        int lineIndex = Random.Range(0, lineZPositions.Length);
        float z = lineZPositions[lineIndex];
        Vector3 spawnPos = new Vector3(spawnX, spawnY, z);
        Quaternion rotation = Quaternion.Euler(0, -90, 0);

        // 몬스터 종류 선택
        int enemyIndex = Random.Range(0, wave.enemyPrefabs.Count);
        GameObject prefab = wave.enemyPrefabs[enemyIndex];

        // 생성
        GameObject go = Instantiate(prefab, spawnPos, rotation);
        Debug.Log($"Enemy 종류 개수: {wave.enemyPrefabs.Count}");

        // Init 호출
        ZombieUnit unit = go.GetComponent<ZombieUnit>();
        if (unit != null && wave.enemyDataList != null && wave.enemyDataList.Count > enemyIndex)
        {
            unit.Init(wave.enemyDataList[enemyIndex]);
        }
        else
        {
            Debug.LogWarning($"[WaveManager] Init 생략됨: enemyIndex={enemyIndex}, 데이터 연결 확인 필요");
        }
    }

    private IEnumerator CheckVictoryCondition()
    {
        while (true)
        {
            ZombieUnit[] enemies = FindObjectsOfType<ZombieUnit>();

            if (enemies.Length == 0)
            {
                Debug.Log("[WaveManager] 모든 적 제거됨 → 승리 처리");
                GameManager.Instance.Victory();
                yield break;
            }

            yield return new WaitForSeconds(1f); // 주기적으로 검사
        }
    }


}
