using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSunflowerData", menuName = "Unit Data/Sunflower")]
public class SunflowerData : UnitData
{
    [Tooltip("생산 주기 (초)")]
    public float produceInterval = 10f;

    [Tooltip("생산할 돈/태양 수치")]
    public int produceAmount = 25;
}
