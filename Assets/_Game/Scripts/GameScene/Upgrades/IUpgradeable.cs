using UnityEngine;

public interface ITowerBase
{
    void Upgrade();
    bool IsMaxLevel();
    void Highlight();
    void Lowlight();
    GameObject GetGhostTower();
    GameObject GetTowerObject();
}
