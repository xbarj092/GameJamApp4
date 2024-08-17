using System.Collections.Generic;

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

    public TowerUpgrade HighestTowerUpgrade(TowerShoot tower)
    {
        TowerUpgrade highestLevelUpgradeTower = null;
        int level = 0;
        foreach (TowerUpgrade upgradeTower in _upgradeTowers)
        {
            if (upgradeTower.UpgradeRangeChecker.Towers.Contains(tower) && upgradeTower.Instance.Level > level)
            {
                highestLevelUpgradeTower = upgradeTower;
            }
        }

        return highestLevelUpgradeTower;
    }
}
