using System.Collections.Generic;
using System.Linq;

public class TowerManager : MonoSingleton<TowerManager>
{
    private List<TowerUpgrade> _upgradeTowers = new();

    public void RegisterUpgradeTower(TowerUpgrade tower)
    {
        if (!_upgradeTowers.Contains(tower))
        {
            _upgradeTowers.Add(tower);
        }
    }

    public void UnregisterUpgradeTower(TowerUpgrade tower)
    {
        if (_upgradeTowers.Contains(tower))
        {
            _upgradeTowers.Remove(tower);
        }
    }

    public TowerUpgrade HighestTowerUpgrade(TowerShoot tower, UpgradeTowerType upgradeTowerType)
    {
        TowerUpgrade highestLevelUpgradeTower = null;
        int level = 0;
        foreach (TowerUpgrade upgradeTower in _upgradeTowers.Where(tower => tower.Stats.UpgradeTowerType == upgradeTowerType))
        {
            if (upgradeTower.UpgradeRangeChecker.Towers.Contains(tower) && upgradeTower.Instance.Level > level)
            {
                highestLevelUpgradeTower = upgradeTower;
            }
        }

        return highestLevelUpgradeTower;
    }
}
