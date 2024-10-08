using UnityEngine;

[CreateAssetMenu(fileName = "New base tower", menuName = "Scriptable/TowerBase")]
public class TowerScriptable : ScriptableObject
{
    public TowerType TowerType;

    public string Name;
    public string FriendlyID;
    public string Description;

    public int BaseLevel;
    public int MaxLevel;

    public Sprite Icon;
}
