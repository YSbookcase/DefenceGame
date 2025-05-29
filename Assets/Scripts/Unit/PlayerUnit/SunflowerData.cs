using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSunflowerData", menuName = "Unit Data/Sunflower")]
public class SunflowerData : UnitData
{
    [Tooltip("���� �ֱ� (��)")]
    public float produceInterval = 10f;

    [Tooltip("������ ��/�¾� ��ġ")]
    public int produceAmount = 25;
}
