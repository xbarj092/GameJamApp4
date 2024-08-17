using UnityEngine;

public class TowerBase<TInstance, TStats> : MonoBehaviour, ITowerBase
    where TInstance : TowerInstanceBase
    where TStats : TowerScriptable
{
    [SerializeField] private Material _material;
    [SerializeField] private GameObject _ghostTower;

    public bool Highlighting;
    public TInstance Instance;
    public TStats Stats;

    public void Upgrade()
    {
    }

    public bool IsMaxLevel() => true;

    public void Highlight()
    {
        if (!Highlighting)
        {
            Highlighting = true;
        }
    }

    public void Lowlight()
    {
        if (Highlighting)
        {
            Highlighting = false;
        }
    }

    public GameObject GetGhostTower()
    {
        return _ghostTower;
    }

    public GameObject GetTowerObject()
    {
        return gameObject;
    }
}
