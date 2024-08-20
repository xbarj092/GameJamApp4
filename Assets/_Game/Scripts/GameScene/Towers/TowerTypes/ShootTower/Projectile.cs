using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _rotateSpeed = 200f;

    private Transform _target;
    private float _damage;

    public void Init(float damage, GameObject target)
    {
        _damage = damage;
        _target = target.transform;
        ObjectSpawner.Instance.ReturnObjectWithDelay(PoolType.Projectile, this, 50);

        Vector2 direction = (_target.position - transform.position).normalized;
        _rb.velocity = direction * _speed;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        _target.GetComponent<EnemyBehavior>().OnEnemyKilled += OnEnemyKilled;
    }

    private void OnEnemyKilled(EnemyBehavior behavior)
    {
        if (_target != null)
        {
            _target.GetComponent<EnemyBehavior>().OnEnemyKilled -= OnEnemyKilled;
            _target = null;
        }
    }

    private void FixedUpdate()
    {
        if (_target == null)
        {
            _rb.velocity = transform.up * _speed;
            return;
        }

        Vector2 direction = (_target.position - transform.position).normalized;
        float rotateAmount = Vector3.Cross(direction, transform.up).z;
        _rb.angularVelocity = -rotateAmount * _rotateSpeed;
        _rb.velocity = transform.up * _speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && collision.TryGetComponent(out Health health))
        {
            health.DealDamage(_damage);
            ObjectSpawner.Instance.ReturnObject(PoolType.Projectile, this);
        }
    }
}
