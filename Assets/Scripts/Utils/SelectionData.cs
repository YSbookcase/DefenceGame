using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/SelectionData")]
public class SelectionData : ScriptableObject
{
    public List<UnitData> selectedUnits;
}
