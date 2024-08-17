using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Scriptable/Enemy")]
public class EnemyInfo : ScriptableObject
{
    [field: SerializeField] public int Speed { get; private set; }
    [field: SerializeField] public int Health { get; private set; }
}
