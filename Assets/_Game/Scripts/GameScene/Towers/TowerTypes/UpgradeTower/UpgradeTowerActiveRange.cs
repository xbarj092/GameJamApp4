using System.Collections.Generic;
using UnityEngine;

public class UpgradeTowerActiveRange : MonoBehaviour
{
    [SerializeField] private TowerUpgrade _upgradeTower;

    private List<TowerShoot> _towers = new();
    public List<TowerShoot> Towers => _towers;

    public void UpgradeTowers(int upgradeLevel)
    {
        foreach (TowerShoot tower in _towers)
        {
            tower.Upgrade(_upgradeTower, upgradeLevel);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out TowerShoot towerShoot) && !_towers.Contains(towerShoot))
        {
            towerShoot.UpdateLevel(_upgradeTower, _upgradeTower.Instance.Level);
            towerShoot.EffectHandler.SetFX(_upgradeTower.Stats.UpgradeTowerType, true);
            _towers.Add(towerShoot);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out TowerShoot towerShoot) && _towers.Contains(towerShoot))
        {
            _towers.Remove(towerShoot);
            towerShoot.UpdateLevel(_upgradeTower, towerShoot.Stats.BaseLevel, true);
            towerShoot.EffectHandler.SetFX(_upgradeTower.Stats.UpgradeTowerType, false);
        }
    }
}
