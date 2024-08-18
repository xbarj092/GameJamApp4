using UnityEngine;

public interface ITowerBase
{
    void Upgrade();
    bool IsMaxLevel();
    bool IsInteractable();
    void Highlight();
    void Lowlight();
    GameObject GetGhostTower();
    GameObject GetTowerObject();
}
