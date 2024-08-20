using System;
using UnityEngine;

public interface ITowerBase
{
    event Action OnTowerOutOfRange;
    void Upgrade();
    bool IsMaxLevel();
    bool IsInteractable();
    void IsPickedUp(bool pickedUp);
    void Highlight();
    void Lowlight();
    GameObject GetGhostTower();
    GameObject GetTowerObject();
}
