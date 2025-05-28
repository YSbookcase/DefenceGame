using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitData : ScriptableObject
{
    public string Name;
    [TextArea] public string Description;
    public Sprite Icon;
    public GameObject Prefeb;
 
}
