using UnityEngine;

[CreateAssetMenu(fileName = "New shoot tower", menuName = "Scriptable/TowerShoot")]
public class TowerShootScriptable : TowerScriptable
{
    public float Damage;
    public float Range;
    public float AttackSpeed;
    public Projectile Projectile;
}
