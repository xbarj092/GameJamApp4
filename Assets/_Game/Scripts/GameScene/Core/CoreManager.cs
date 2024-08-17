using UnityEngine;

public class CoreManager : MonoSingleton<CoreManager> 
{
    [SerializeField] SizeIncrease sizeIncrease;
    [SerializeField] float _maxHealth;
    [SerializeField] Health _healthSystem;

    public void UpdateCore() { }

    private void OnEnable() {
        _healthSystem.OnDeath.AddListener(GameOver);
        _healthSystem.OnHealthChange.AddListener(UpdateUI);
    }

    private void OnDisable() {
        _healthSystem.OnDeath.RemoveListener(GameOver);
        _healthSystem.OnHealthChange.RemoveListener(UpdateUI);
    }

    private void UpdateUI(float damage) { 
        //Todo
    }

    private void GameOver() { 
        //Todo
    }

    private void FixedUpdate()
    {
        sizeIncrease.UpdateCameraSize();
        sizeIncrease.UpdateCoreSize();
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
