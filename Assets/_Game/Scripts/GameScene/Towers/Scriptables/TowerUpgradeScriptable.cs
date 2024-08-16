using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New upgrade tower", menuName = "Scriptable/TowerUpgrade")]
public class TowerUpgradeScriptable : TowerScriptable
{
    public UpgradeTowerType UpgradeTowerType;

    public List<float> StatValues = new();
    public List<float> UpgradePrices = new();
}
