using System;
using UnityEngine;

public class TutorialCollision : MonoBehaviour
{
    private bool _alreadyTriggered = false;

    public event Action OnTriggerEntered;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_alreadyTriggered && collision.gameObject.CompareTag("Player"))
        {
            _alreadyTriggered = true;
            OnTriggerEntered?.Invoke();
        }
    }
}
