using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class UnitData : ScriptableObject
{
   
    public string unitName;
    [TextArea] public string Description;
    public Sprite Icon;
    public int maxHp = 5;
    public int attackPower = 0;
    public float attackDelay =1;
    public float range =0.3f;
    public int cost = 25;
    public GameObject unitPrefab;
    public float coolTime = 0f;

}
