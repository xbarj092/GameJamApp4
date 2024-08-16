using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New base tower", menuName = "Scriptable/TowerBase")]
public class TowerScriptable : ScriptableObject
{
    public TowerType TowerType;

    public string Name;
    public string FriendlyID;
    public string Description;
    public int MaxLevel;

    public Sprite Icon;
}
