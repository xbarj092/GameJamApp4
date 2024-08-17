using UnityEngine;

public interface ITowerBase
{
    void Upgrade();
    bool IsMaxLevel();
    void Highlight();
    GameObject GetGhostTower();
    GameObject GetTowerObject();
}
