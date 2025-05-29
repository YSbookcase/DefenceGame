using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSunflower : Unit
{
    public override void Init(UnitData data)
    {
        base.Init(data);
        if (data is SunflowerData sunflower)
        {
            StartCoroutine(ProduceSun(sunflower.produceInterval, sunflower.produceAmount));
        }
    }

    private IEnumerator ProduceSun(float interval, int amount)
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            Debug.Log($"[Sunflower] 태양 {amount} 생산!");
            // 자원 매니저에 amount 전달
        }
    }
}