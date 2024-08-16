using System;

[Serializable]
public class TowerInstanceShoot : TowerInstanceBase
{
    public float Damage;
    public float Range;
    public float AttackSpeed;

    public TowerInstanceShoot(float damage, float range, float attackSpeed)
    {
        Damage = damage;
        Range = range;
        AttackSpeed = attackSpeed;
    }
}
