using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class UnitData : ScriptableObject
{
   
    public string unitName;
    [TextArea] public string Description;
    public Sprite Icon;
    public int maxHp;
    public int attackPower;
    public float attackDelay;
    public float range;
    public GameObject unitPrefab;

}
