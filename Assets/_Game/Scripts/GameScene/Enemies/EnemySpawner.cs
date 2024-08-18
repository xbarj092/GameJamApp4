using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [field: SerializeField] public UnityEvent<EnemyBehavior> OnEnemySpawn;

    [SerializeField] private float IdleSpawnTime;

    [SerializeField] private EnemyBehavior _enemyPrefab;
    private Vector2 _sceneSize;

    private Camera cam;

    IEnumerator IdleSpawn() {
        while(true) { 
            SpawnEnemy(Vector2.up * _sceneSize.y + Vector2.right * Random.Range(_sceneSize.x, -_sceneSize.x));
            yield return new WaitForSeconds(IdleSpawnTime);
            SpawnEnemy(Vector2.down * _sceneSize.y + Vector2.right * Random.Range(_sceneSize.x, -_sceneSize.x));
            yield return new WaitForSeconds(IdleSpawnTime);
            SpawnEnemy(Vector2.left * _sceneSize.x + Vector2.up * Random.Range(_sceneSize.y, -_sceneSize.y));
            yield return new WaitForSeconds(IdleSpawnTime);
            SpawnEnemy(Vector2.right * _sceneSize.x + Vector2.up * Random.Range(_sceneSize.y, -_sceneSize.y));
            yield return new WaitForSeconds(IdleSpawnTime);
        }
    }

    IEnumerator BurstSpawn() {
        while(true) {
            yield return new WaitForSeconds(20f);
            for(int i = 0; i < 3; i++) { 
                for(int j = 0; j < 1; j++) {
                    SpawnEnemy(Vector2.up * _sceneSize.y + Vector2.right * Random.Range(_sceneSize.x, -_sceneSize.x));
                }
                yield return new WaitForSeconds(0.2f);
            }
            
            yield return new WaitForSeconds(20f);
            for(int i = 0; i < 3; i++) {
                for(int j = 0; j < 2; j++) {
                    SpawnEnemy(Vector2.down * _sceneSize.y + Vector2.right * Random.Range(_sceneSize.x, -_sceneSize.x));
                }
                yield return new WaitForSeconds(0.2f);
            }
            
            yield return new WaitForSeconds(20f);
            for(int i = 0; i < 3; i++) {
                for(int j = 0; j < 3; j++) {
                    SpawnEnemy(Vector2.left * _sceneSize.x + Vector2.up * Random.Range(_sceneSize.y, -_sceneSize.y));
                }
                yield return new WaitForSeconds(0.2f);
            }
            
            yield return new WaitForSeconds(20f);
            for(int i = 0; i < 3; i++) {
                for(int j = 0; j < 4; j++) {
                    SpawnEnemy(Vector2.right * _sceneSize.x + Vector2.up * Random.Range(_sceneSize.y, -_sceneSize.y));
                }
                yield return new WaitForSeconds(0.2f);
            }
        }
    }

    private void Awake() {
        cam = Camera.main;
        UpdateScreenSize();

        if (TutorialManager.Instance.TutorialCompleted)
        {
            StartSpawning();
        }
    }

    private void OnEnable()
    {
        TutorialEvents.OnTutorialCompleted += StartSpawning;
        TutorialEvents.OnEnemySpawned += SpawnEnemy;
    }

    private void OnDisable()
    {
        TutorialEvents.OnTutorialCompleted -= StartSpawning;
        TutorialEvents.OnEnemySpawned -= SpawnEnemy;
    }

    private void Update() {
        if (TutorialManager.Instance.TutorialCompleted)
        {
            UpdateScreenSize();
        }
    }

    private void StartSpawning()
    {
        StartCoroutine(IdleSpawn());
        StartCoroutine(BurstSpawn());
    }

    private void UpdateScreenSize() {
        _sceneSize.y = cam.orthographicSize * 2f * 1.1f;
        _sceneSize.x = _sceneSize.y * cam.aspect;
        _sceneSize /= 2;
    } 

    private void SpawnEnemy(Vector2 position) {
        EnemyBehavior enemy = Instantiate(_enemyPrefab, position, Quaternion.identity, null);
        OnEnemySpawn.Invoke(enemy);
    }
}
