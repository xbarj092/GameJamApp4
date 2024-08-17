using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemySpawner))]
public class EnemiesManager : MonoBehaviour
{
    private EnemySpawner _spawner;

    private void Awake() {
        _spawner = GetComponent<EnemySpawner>();
    }

    private void OnEnable() {
        _spawner.OnEnemySpawn.AddListener(SetTargetPoint);
    }

    private void SetTargetPoint(EnemyBehavior enemy) {
        enemy.SetTargetPoint(CoreManager.Instance.GetRandomPointOnCircle(enemy.transform.position), (e) => e.SetTargetPoint(Vector3.zero)); //CoreManager.GetRandomAccessPoint, CorePosition
    }
}
