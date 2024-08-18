using UnityEngine;

public interface ITowerBase
{
    void Upgrade();
    bool IsMaxLevel();
    bool IsInteractable();
    void IsPickedUp(bool pickedUp);
    void Highlight();
    void Lowlight();
    GameObject GetGhostTower();
    GameObject GetTowerObject();
}
