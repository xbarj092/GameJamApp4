using UnityEngine;

public class TowerShoot : TowerBase
{
    [SerializeField] private CircleCollider2D _enemyChecker;
    [SerializeField] private TowerScriptable _towerStats;
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private float _rotationSpeed = 5f;

    private TowerInstanceShoot _towerInstance;
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
        if (!IsInRange())
        {
            CancelInvoke();
            _target = null;
        }
    }

    private void SetStats()
    {
        if (_towerStats is TowerShootScriptable shootScriptable)
        {
            _towerInstance = new TowerInstanceShoot(shootScriptable.Damage, shootScriptable.Range, shootScriptable.AttackSpeed);
            _enemyChecker.radius = shootScriptable.Range;
        }
    }

    public void FocusOnEnemy(GameObject gameObject)
    {
        if (_target == null)
        {
            CancelInvoke();
            _target = gameObject;
        }

        InvokeRepeating(nameof(ShootEnemy), 0.5f, 1 / _towerInstance.AttackSpeed);
    }

    private void ShootEnemy()
    {
        Projectile projectile = Instantiate(_projectilePrefab, _spawnPoint.position, Quaternion.identity);
        projectile.Init(_towerInstance.Damage, _target);
    }

    private bool IsInRange() => Vector2.Distance(gameObject.transform.position, _target.transform.position) <= _towerInstance.Range + 1;

    private void RotateTowardsTarget()
    {
        Vector2 direction = (_target.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * _rotationSpeed);
    }
}
