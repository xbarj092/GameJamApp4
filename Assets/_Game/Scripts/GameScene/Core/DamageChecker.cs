using System;
using UnityEngine;

public class DamageChecker : MonoBehaviour
{
    public event Action<float> OnDamageTaken;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out EnemyBehavior enemy))
        {
            OnDamageTaken?.Invoke(enemy.Info.CoreDamage);
            enemy.Death();
        }
    }
}
