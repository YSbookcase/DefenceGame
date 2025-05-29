using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCherryBombData", menuName = "Unit Data/Cherry Bomb")]
public class CherryBombData : UnitData
{
    [Tooltip("폭발 범위 반지름")]
    public float explosionRadius = 2f;

    [Tooltip("폭발 데미지")]
    public int explosionDamage = 999;
}
