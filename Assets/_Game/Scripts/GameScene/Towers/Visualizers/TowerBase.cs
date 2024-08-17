using UnityEngine;

public class TowerBase<TInstance, TStats> : MonoBehaviour, IUpgradeable 
    where TInstance : TowerInstanceBase
    where TStats : TowerScriptable
{
    public TInstance Instance;
    public TStats Stats;

    public void Upgrade()
    {
    }

    public bool IsMaxLevel() => true;
}
