using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    public List<EnemyBehavior> Enemies;

    /*private void Start() {
        Enemies.ForEach((e) => e.SetTargetPoint(Core.GetRandomAccessPoint(e.transform.position), GoToCore));
    }

    private void GoToCore(EnemyBehavior enemy) {
        enemy.SetTargetPoint(Core.transform.position);
    }*/
}
