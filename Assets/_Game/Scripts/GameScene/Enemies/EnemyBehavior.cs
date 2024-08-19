using UnityEngine;
using UnityEngine.Events;
using EasyButtons;
using System;

[RequireComponent(typeof(EnemyMovement), typeof(Health), typeof(SpriteRenderer))]
public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] private PoolType _poolType;
    [SerializeField] private EnemyInfo _infoTemplate;
    public EnemyInfo Info => _infoTemplate;

    private Health _health;
    private SpriteRenderer _renderer;
    private EnemyMovement _movement;

    public event Action<EnemyBehavior> OnEnemyKilled;

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

    public void Death(bool coreDeath = false) {
        if (TutorialManager.Instance.IsTutorialPlaying(TutorialID.Replacing))
        {
            TutorialEvents.OnEnemyKilledInvoke(coreDeath);
        }

        if (!coreDeath)
        {
            for (int i = 0; i < _infoTemplate.CoinAmount; i++)
            {
                Vector2 randomPoint = UnityEngine.Random.insideUnitCircle * 0.5f;
                Vector3 spawnPosition = new(transform.position.x + randomPoint.x, transform.position.y + randomPoint.y, transform.position.z);
                Coin coin = ObjectSpawner.Instance.GetObject<Coin>(PoolType.Coin);
                coin.transform.position = spawnPosition;
            }
        }

        OnEnemyKilled?.Invoke(this);
        ObjectSpawner.Instance.ReturnObject(_poolType, this);
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
