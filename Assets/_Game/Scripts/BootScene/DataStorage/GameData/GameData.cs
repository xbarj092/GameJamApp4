using System;
using UnityEngine;

[Serializable]
public class GameData
{
    [SerializeField] private EnemyScaleData _enemyScaleData;
    public EnemyScaleData EnemyScaleData
    {
        get => _enemyScaleData;
        set
        {

        }
    }
}
