using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [field: SerializeField] public UnityEvent<EnemyBehavior> OnEnemySpawn;

    [SerializeField] private AnimationCurve IdleSpawnTime;
    [SerializeField] private AnimationCurve IdleEliteSpawnTime;
    [SerializeField] private AnimationCurve NumberEnemyToSpawn;
    [SerializeField] private AnimationCurve NumberEliteEnemyToSpawn;

    [SerializeField] private EnemyBehavior _tutorialEnemyPrefab;
    [SerializeField] private EnemyBehavior _enemyPrefab;
    [SerializeField] private EnemyBehavior _eliteEnemyPrefab;
    private Vector2 _sceneSize;

    private Camera cam;

    private int _burstCount = 0;
    
    IEnumerator IdleSpawn(EnemyBehavior prefab, AnimationCurve timing, float startTime = 0) {
        while(true) {
            yield return new WaitForSeconds(timing.Evaluate(LocalDataStorage.Instance.PlayerData.PlayerStats.TimeAlive - startTime));
            SpawnEnemy(Vector2.up * _sceneSize.y + Vector2.right * Random.Range(_sceneSize.x, -_sceneSize.x), prefab);
            yield return new WaitForSeconds(timing.Evaluate(LocalDataStorage.Instance.PlayerData.PlayerStats.TimeAlive - startTime));
            SpawnEnemy(Vector2.down * _sceneSize.y + Vector2.right * Random.Range(_sceneSize.x, -_sceneSize.x), prefab);
            yield return new WaitForSeconds(timing.Evaluate(LocalDataStorage.Instance.PlayerData.PlayerStats.TimeAlive - startTime));
            SpawnEnemy(Vector2.left * _sceneSize.x + Vector2.up * Random.Range(_sceneSize.y, -_sceneSize.y), prefab);
            yield return new WaitForSeconds(timing.Evaluate(LocalDataStorage.Instance.PlayerData.PlayerStats.TimeAlive - startTime));
            SpawnEnemy(Vector2.right * _sceneSize.x + Vector2.up * Random.Range(_sceneSize.y, -_sceneSize.y), prefab);
        }
    }

    IEnumerator BurstSpawn(EnemyBehavior prefab, AnimationCurve amount) {
        while(true) {
            yield return new WaitForSeconds(20f);
            for(int i = 0; i < 3; i++) { 
                for(int j = 0; j < amount.Evaluate(_burstCount)/3; j++) {
                    SpawnEnemy(Vector2.up * _sceneSize.y + Vector2.right * Random.Range(_sceneSize.x, -_sceneSize.x), prefab);
                }
                yield return new WaitForSeconds(0.3f);
            }
            if(_burstCount > 6)
                SpawnEnemy(Vector2.up * _sceneSize.y + Vector2.right * Random.Range(_sceneSize.x, -_sceneSize.x), _eliteEnemyPrefab);
            
            if(_burstCount == 12)
                StartCoroutine(IdleSpawn(_eliteEnemyPrefab, IdleEliteSpawnTime, LocalDataStorage.Instance.PlayerData.PlayerStats.TimeAlive));

            if(_burstCount == 18) { 
                StartCoroutine(BurstSpawn(_eliteEnemyPrefab, NumberEliteEnemyToSpawn));
                _burstCount = 0;
                break;
            }

            _burstCount++;
        }
    }

    private void Awake() {
        cam = Camera.main;
        UpdateScreenSize();
    }

    private void Start() {
        if(TutorialManager.Instance.TutorialCompleted) {
            StartSpawning();
        }
    }

    private void OnEnable()
    {
        TutorialEvents.OnTutorialCompleted += StartSpawning;
        TutorialEvents.OnEnemySpawned += (pos) => SpawnEnemy(pos, _tutorialEnemyPrefab);
    }

    private void OnDisable()
    {
        TutorialEvents.OnEnemySpawned -= (pos) => SpawnEnemy(pos, _tutorialEnemyPrefab);
        TutorialEvents.OnTutorialCompleted -= StartSpawning;
    }

    private void Update() {
        if (TutorialManager.Instance.TutorialCompleted)
        {
            UpdateScreenSize();
        }
    }

    private void StartSpawning()
    {
        StartCoroutine(IdleSpawn(_enemyPrefab, IdleSpawnTime));
        StartCoroutine(BurstSpawn(_enemyPrefab, NumberEnemyToSpawn));
    }

    private void UpdateScreenSize() {
        _sceneSize.y = cam.orthographicSize * 2f * 1.1f;
        _sceneSize.x = _sceneSize.y * cam.aspect;
        _sceneSize /= 2;
    } 

    private void SpawnEnemy(Vector2 position, EnemyBehavior pref) {
        EnemyBehavior enemy = Instantiate(pref, position, Quaternion.identity, null);
        OnEnemySpawn.Invoke(enemy);
    }
}
