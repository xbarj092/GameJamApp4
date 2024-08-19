using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Coin : MonoBehaviour {

    private Rigidbody2D _rb;
    [SerializeField] private float _speed = 3f;
    [SerializeField] private float _acceleration = 1f;

    private void Awake() {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("PlayerInteraction")) {

            StartCoroutine(MoveTo(collision.transform));
        }
    }

    IEnumerator MoveTo(Transform location) {
        float currentSpeed = _speed;

        while (Vector2.Distance(location.position, transform.position) > 0.1f)
        {
            currentSpeed += _acceleration * Time.deltaTime;
            _rb.velocity = currentSpeed * (location.position - transform.position).normalized;
            yield return null;
        }

        if (LocalDataStorage.Instance != null) {
            CurrencyData data = LocalDataStorage.Instance.PlayerData.CurrencyData;
            data.Coins += 1;
            LocalDataStorage.Instance.PlayerData.CurrencyData = data;
        }

        AudioManager.Instance.Play(SoundType.CoinPickUp);
        ObjectSpawner.Instance.ReturnObject(PoolType.Coin, this);

        if (TutorialManager.Instance.IsTutorialPlaying(TutorialID.Upgrades))
        {
            TutorialEvents.OnCoinPickedUpInvoke(this);
        }
    }
}
