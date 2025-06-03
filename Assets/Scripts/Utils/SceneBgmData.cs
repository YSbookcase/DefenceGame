using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "SceneBgmData", menuName = "Audio/SceneBgmData")]
public class SceneBgmData : ScriptableObject
{
    public string sceneName;
    public List<AudioClip> bgmList;
}
