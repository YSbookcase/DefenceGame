using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCherryBombData", menuName = "Unit Data/Cherry Bomb")]
public class CherryBombData : UnitData
{
    [Tooltip("���� ���� ������")]
    public float explosionRadius = 2f;

    [Tooltip("���� ������")]
    public int explosionDamage = 999;
}
