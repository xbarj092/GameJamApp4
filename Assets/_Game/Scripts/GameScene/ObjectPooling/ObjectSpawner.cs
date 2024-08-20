using UnityEngine;

public class ObjectSpawner : MonoSingleton<ObjectSpawner>
{
    [SerializeField] private PoolHandler _poolHandler;

    [Header("Prefabs")]
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private EnemyBehavior _enemyPrefab;
    [SerializeField] private EnemyBehavior _eliteEnemyPrefab;
    [SerializeField] private EnemyBehavior _tutorialEnemyPrefab;
    [SerializeField] private EnemyBehavior _menuEnemyPrefab;
    [SerializeField] private Coin _coinPrefab;

    private void Start()
    {
        if(_projectilePrefab != null) _poolHandler.CreatePool(PoolType.Projectile, _projectilePrefab, 100, transform);
        if(_enemyPrefab != null) _poolHandler.CreatePool(PoolType.Enemy, _enemyPrefab, 100, transform);
        if(_coinPrefab != null) _poolHandler.CreatePool(PoolType.Coin, _coinPrefab, 100, transform);
        if(_eliteEnemyPrefab != null) _poolHandler.CreatePool(PoolType.EliteEnemy, _eliteEnemyPrefab, 50, transform);
        if(_menuEnemyPrefab != null) _poolHandler.CreatePool(PoolType.MenuEnemy, _menuEnemyPrefab, 50, transform);
        if(_tutorialEnemyPrefab != null) _poolHandler.CreatePool(PoolType.TutorialEnemy, _tutorialEnemyPrefab, 1, transform);
    }

    public T GetObject<T>(PoolType type) where T : Component
    {
        return _poolHandler.GetObject<T>(type);
    }

    public void ReturnObject<T>(PoolType type, T obj) where T : Component
    {
        _poolHandler.ReturnObject(type, obj);
    }

    public void ReturnObjectWithDelay<T>(PoolType type, T obj, float delay) where T : Component
    {
        _poolHandler.ReturnObjectWithDelay(type, obj, delay);
    }
}
