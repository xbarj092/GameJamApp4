using UnityEngine;

public class CoreManager : MonoSingleton<CoreManager>
{
    [SerializeField] SizeIncrease sizeIncrease;
    [SerializeField] float _maxHealth;
    [SerializeField] Health _healthSystem;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private DamageChecker _damageChecker;

    public void UpdateCore() { }

    private void OnEnable()
    {
        _damageChecker.OnDamageTaken += DamageCore;
        _healthSystem.SetMaxHealth(_maxHealth);
        _healthSystem.OnDeath.AddListener(GameOver);
        _healthSystem.OnHealthChange.AddListener(UpdateUI);
    }

    private void OnDisable()
    {
        _damageChecker.OnDamageTaken -= DamageCore;
        _healthSystem.OnDeath.RemoveListener(GameOver);
        _healthSystem.OnHealthChange.RemoveListener(UpdateUI);
    }

    private void UpdateUI(float damage)
    {
        _renderer.material.SetFloat("_Damage", damage / _healthSystem.MaxHealth);
    }

    private void DamageCore(float damage)
    {
        _healthSystem.DealDamage(damage);
    }

    private void GameOver() { 
        // maybe some core destroy animation beforehand?
        ScreenEvents.OnGameScreenOpenedInvoke(GameScreenType.GameOver);
    }

    private void FixedUpdate()
    {
        if (TutorialManager.Instance.TutorialCompleted)
        {
            sizeIncrease.UpdateCameraSize();
            sizeIncrease.UpdateCoreSize();
        }
    }

    public Vector2 GetRandomPointOnCircle(Vector2 EnemyPosition)
    {
        var angle = GetAngle(CoreManager.Instance.transform.position, EnemyPosition);
        angle += Random.Range(-sizeIncrease.GetSegmentSize(), sizeIncrease.GetSegmentSize());
        float radius = sizeIncrease.GetRadius();
        Vector2 direction = new Vector2(Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad));
        return direction.normalized * radius;
    }

    public static float GetAngle(Vector2 myPos, Vector2 EnemyPos)
    {
        Vector2 CircleCenter = myPos;
        float angle = Mathf.Atan2(EnemyPos.x - CircleCenter.x, EnemyPos.y - CircleCenter.y) * Mathf.Rad2Deg;
        return angle;
    }

    public void DestroyCore() { }
}
