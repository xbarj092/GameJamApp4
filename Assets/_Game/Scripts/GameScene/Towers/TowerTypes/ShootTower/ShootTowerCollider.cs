using UnityEngine;

public class ShootTowerCollider : MonoBehaviour
{
    [SerializeField] private TowerShoot _tower;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            _tower.FocusOnEnemy(collision.gameObject);
        }
    }
}
