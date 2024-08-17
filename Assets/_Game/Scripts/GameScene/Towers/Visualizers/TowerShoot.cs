using System;
using System.Collections.Generic;
using UnityEngine;

public class TowerShoot : TowerBase<TowerInstanceShoot, TowerShootScriptable>
{
    [SerializeField] private CircleCollider2D _enemyChecker;
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private float _rotationSpeed = 5f;

    private List<TowerUpgrade> _upgradingTowers = new();
    private GameObject _target;

    private void Awake()
    {
        SetStats();
    }

    private void Update()
    {
        if (_target != null)
        {
            RotateTowardsTarget();
            CheckDistanceOfTarget();
        }
    }

    private void CheckDistanceOfTarget()
    {
        if (!IsTargetInRange())
        {
            CancelInvoke();
            _target = null;
        }
    }

    private void SetStats()
    {
        Instance = new TowerInstanceShoot(Stats.Damage, Stats.Range,
            Stats.AttackSpeed, Stats.BaseLevel);
        _enemyChecker.radius = Stats.Range;
    }

    public void FocusOnEnemy(GameObject gameObject)
    {
        if (_target == null)
        {
            CancelInvoke();
            _target = gameObject;
        }

        InvokeRepeating(nameof(ShootEnemy), 0.5f, 1 / Instance.StatValues[UpgradeTowerType.AttackSpeed].Value);
    }

    private void ShootEnemy()
    {
        Projectile projectile = Instantiate(_projectilePrefab, _spawnPoint.position, Quaternion.identity);
        projectile.Init(Instance.StatValues[UpgradeTowerType.Damage].Value, _target);
    }

    private void RotateTowardsTarget()
    {
        Vector2 direction = (_target.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * _rotationSpeed);
    }

    public void Upgrade(TowerUpgrade upgradeTower, int upgradeLevel)
    {
        Upgrade();

        if (!IsMaxLevel(upgradeTower.Stats.UpgradeTowerType))
        {
            UpdateLevel(upgradeTower, upgradeLevel);
        }
    }

    public void UpdateLevel(TowerUpgrade upgradeTower, int level, bool force = false)
    {
        TowerUpgrade relevantUpgradeTower = upgradeTower;

        if (force)
        {
            TowerUpgrade highestTowerUpgrade = TowerManager.Instance.HighestTowerUpgrade(this);
            if (highestTowerUpgrade != null)
            {
                relevantUpgradeTower = highestTowerUpgrade;
                level = relevantUpgradeTower.Instance.Level;
            }
        }

        if (level > Instance.StatValues[relevantUpgradeTower.Stats.UpgradeTowerType].Level || force)
        {
            Instance.StatValues[relevantUpgradeTower.Stats.UpgradeTowerType] = new()
            {
                Level = level,
                Value = relevantUpgradeTower.Stats.StatValues[level - 1]
            };
        }
    }

    private bool IsTargetInRange() => Vector2.Distance(gameObject.transform.position, _target.transform.position) <= Instance.StatValues[UpgradeTowerType.Range].Value + 1;

    public bool IsMaxLevel(UpgradeTowerType upgradeType) => Instance.StatValues[upgradeType].Level >= Stats.MaxLevel;
}
