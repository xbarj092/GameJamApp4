using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Scriptable/Enemy")]
public class EnemyInfo : ScriptableObject
{
    [field: SerializeField] public float Speed { get; private set; }
    [field: SerializeField] public float Health { get; private set; }
    [field: SerializeField] public int CoinAmount { get; private set; }
    [field: SerializeField] public int CoreDamage { get; private set; }
}
