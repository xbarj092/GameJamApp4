using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using EasyButtons;

[RequireComponent(typeof(EnemyMovement), typeof(Health), typeof(SpriteRenderer))]
public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] private EnemyInfo _infoTemplate;
    [SerializeField] private Coin _CoinPrefab;
    
    private Health _health;
    private SpriteRenderer _renderer;
    private EnemyMovement _movement;

    private void Awake() {
        _health = GetComponent<Health>();
        _movement = GetComponent<EnemyMovement>();
        _renderer = GetComponent<SpriteRenderer>();
    }
    private void OnEnable() {
        _movement.Speed = _infoTemplate.Speed;
        _health.SetMaxHealth(_infoTemplate.Health);

        _health.OnHealthChange.AddListener(ChangeHealthProgress);
        _health.OnDeath.AddListener(Death);
    }

    private void OnDisable() {
        _health.OnHealthChange.RemoveListener(ChangeHealthProgress);
        _health.OnDeath.RemoveListener(Death);
    }

    public void ChangeHealthProgress(float damage) {
        _renderer.material.SetFloat("_DamageProgress", damage/_health.MaxHealth);
    }

    public void SetTargetPoint(Vector3 target, UnityAction<EnemyBehavior> affterArrive = null) {
        _movement.SetTargetPoint(target, () => {if(affterArrive != null) affterArrive(this);});
    }

    private void Death() {
        Instantiate(_CoinPrefab, transform.position, Quaternion.identity, null);
        Destroy(gameObject);
    }

    [Button]
    public void HitEnemy(float dmg) {
        _health.DealDamage(dmg);
    }

    [Button]
    public void SetPoint(Vector3 target) {
        _movement.SetTargetPoint(target);
    }
}
