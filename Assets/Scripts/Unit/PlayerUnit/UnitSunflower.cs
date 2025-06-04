using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSunflower : Unit
{
    [SerializeField] private GameObject sunEnergyPrefab;
    [SerializeField] private Transform sunSpawnPoint; // 인스펙터에 연결

    public override void Init(UnitData data)
    {
        base.Init(data);
        if (data is SunflowerData sunflower)
        {
            Debug.Log("[Unit] Init 호출됨");
            StartCoroutine(ProduceSun(sunflower.produceInterval, sunflower.produceAmount));
        }
        else
            {
                Debug.LogWarning("[Sunflower] Init 실패 - 데이터 캐스팅 실패");
            }
        
    }

    private IEnumerator ProduceSun(float interval, int amount)
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);

            Vector3 spawnPos = sunSpawnPoint.position;

            // 디버그: 유닛 위치와 SpawnPoint 위치 시각화
            //Debug.DrawLine(transform.position, spawnPos, Color.red, 5f);
            //Debug.Log($"[Sunflower] Unit Pos: {transform.position}, SunSpawnPoint Pos: {spawnPos}");


            GameObject sun = Instantiate(sunEnergyPrefab, spawnPos, Quaternion.identity);

            var sr = sun.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.sortingLayerName = "SunEnergy";
                sr.sortingOrder = 100;
            }

            sun.transform.forward = Camera.main.transform.forward;


            var energy = sun.GetComponent<SunEnergy>();
            if (energy != null)
            {
                energy.SetValue(amount);
            }
            //Debug.Log($"[Sunflower] 태양 생성됨! +{amount}");
        }
    }


}