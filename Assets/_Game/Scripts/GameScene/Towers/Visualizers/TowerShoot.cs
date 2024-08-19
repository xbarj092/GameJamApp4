using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TowerShoot : TowerBase<TowerInstanceShoot, TowerShootScriptable>
{
    [SerializeField] private CircleCollider2D _enemyChecker;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private float _rotationSpeed = 5f;
    [SerializeField] private LayerMask _enemyLayer;

    private List<TowerUpgrade> _upgradingTowers = new();
    private GameObject _target;

    public VFXHandler EffectHandler;

    private void Awake()
    {
        SetStats();
    }

    private void Update()
    {
        if (_pickedUp)
        {
            if (_target != null)
            {
                CancelInvoke();
                _target = null;
            }

            return;
        }

        if (_target == null || !IsTargetInRange())
        {
            FindNewTarget();
        }
        else
        {
            RotateTowardsTarget();
        }
    }

    private void SetStats()
    {
        Instance = new TowerInstanceShoot(Stats.Damage, Stats.Range,
            Stats.AttackSpeed, Stats.BaseLevel);
        _enemyChecker.radius = Stats.Range;
    }

    public void FocusOnEnemy(GameObject enemy)
    {
        if (_target == null)
        {
            CancelInvoke();
            _target = enemy;
            _target.GetComponent<EnemyBehavior>().OnEnemyKilled += OnEnemyKilled;
            InvokeRepeating(nameof(ShootEnemy), 0.5f, 1 / Instance.StatValues[UpgradeTowerType.AttackSpeed].Value);
        }
    }

    private void ShootEnemy()
    {
        if (_target == null)
        {
            return;
        }

        AudioManager.Instance.Play(SoundType.TowerShoot);
        Projectile projectile = ObjectSpawner.Instance.GetObject<Projectile>(PoolType.Projectile);
        projectile.transform.position = _spawnPoint.position;
        projectile.Init(Instance.StatValues[UpgradeTowerType.Damage].Value, _target);
    }

    private void RotateTowardsTarget()
    {
        if (_target == null) 
        {
            return;
        }

        Vector2 direction = (_target.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * _rotationSpeed);
    }

    private void FindNewTarget()
    {
        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(transform.position, Instance.StatValues[UpgradeTowerType.Range].Value, _enemyLayer).Where(enemy => enemy.isActiveAndEnabled).ToArray();

        GameObject closestEnemy = null;
        float shortestDistance = Mathf.Infinity;

        foreach (Collider2D enemyCollider in enemiesInRange)
        {
            float distanceToEnemy = Vector2.Distance(transform.position, enemyCollider.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                closestEnemy = enemyCollider.gameObject;
            }
        }

        if (closestEnemy != null)
        {
            FocusOnEnemy(closestEnemy);
        }
        else
        {
            CancelInvoke();
        }
    }

    private void OnEnemyKilled(EnemyBehavior enemy)
    {
        _target.GetComponent<EnemyBehavior>().OnEnemyKilled -= OnEnemyKilled;
        _target = null;
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
            TowerUpgrade highestTowerUpgrade = TowerManager.Instance.HighestTowerUpgrade(this, upgradeTower.Stats.UpgradeTowerType);
            if (highestTowerUpgrade != null)
            {
                relevantUpgradeTower = highestTowerUpgrade;
                level = relevantUpgradeTower.Instance.Level;
            }
            else
            {
                ApplyStatValue(relevantUpgradeTower, level, GetBaseValue(relevantUpgradeTower.Stats.UpgradeTowerType));
                return;
            }
        }

        if (level >= Instance.StatValues[relevantUpgradeTower.Stats.UpgradeTowerType].Level || force)
        {
            float value = GetBaseValue(relevantUpgradeTower.Stats.UpgradeTowerType) * relevantUpgradeTower.Stats.StatValues[level - 1];
            ApplyStatValue(relevantUpgradeTower, level, value);
        }
    }

    private void ApplyStatValue(TowerUpgrade relevantUpgradeTower, int level, float value)
    {
        Instance.StatValues[relevantUpgradeTower.Stats.UpgradeTowerType] = new()
        {
            Level = level,
            Value = value
        };
    }

    private float GetBaseValue(UpgradeTowerType upgradeTowerType)
    {
        return upgradeTowerType switch
        {
            UpgradeTowerType.Damage => Stats.Damage,
            UpgradeTowerType.Range => Stats.Range,
            UpgradeTowerType.AttackSpeed => Stats.AttackSpeed,
            _ => 0,
        };
    }

    private bool IsTargetInRange() => Vector2.Distance(gameObject.transform.position, _target.transform.position) <= Instance.StatValues[UpgradeTowerType.Range].Value + 1;

    public bool IsMaxLevel(UpgradeTowerType upgradeType) => Instance.StatValues[upgradeType].Level >= Stats.MaxLevel;
}
