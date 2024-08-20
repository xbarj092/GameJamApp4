using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuSpawner : MonoBehaviour
{
    [SerializeField] private PoolType poolType;
    [SerializeField] private List<Transform> posToSpawn;
    [SerializeField] private Transform TargetPosition;

    private IEnumerator Start() {
        int posIndex = 0;

        while(true) {
            EnemyBehavior enemy = ObjectSpawner.Instance.GetObject<EnemyBehavior>(poolType);
            enemy.transform.position = posToSpawn[posIndex].position + Random.Range(-3, 3) * Vector3.up;
            enemy.SetTargetPoint(TargetPosition.position);
            enemy.transform.position = new Vector3(enemy.transform.position.x, enemy.transform.position.y, 0);
            yield return new WaitForSeconds(3);
            posIndex = (posIndex+1)%posToSpawn.Count;
        }
    }
}
