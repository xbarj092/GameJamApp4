using AYellowpaper.SerializedCollections;
using System;

[Serializable]
public class TowerInstanceShoot : TowerInstanceBase
{
    public SerializedDictionary<UpgradeTowerType, StatValue> StatValues;

    public TowerInstanceShoot(float damage, float range, float attackSpeed, int level)
    {
        StatValues = new()
        {
            { UpgradeTowerType.Damage, new(level, damage) },
            { UpgradeTowerType.Range, new(level, range) },
            { UpgradeTowerType.AttackSpeed, new(level, attackSpeed) }
        };
    }
}
