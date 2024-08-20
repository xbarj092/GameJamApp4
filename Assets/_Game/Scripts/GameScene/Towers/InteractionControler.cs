using System;
using UnityEngine;

public class InteractionControler : MonoBehaviour
{
    public bool Interactable;
    public bool _interactable
    {
        get => Interactable;
        private set
        {
            Interactable = value;
            if (value == false)
            {
                OnTowerOutOfRangeInvoke();
            }
        }
    }

    public event Action OnTowerOutOfRange;

    private void OnTowerOutOfRangeInvoke()
    {
        OnTowerOutOfRange?.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("PlayerInteraction")) {
            _interactable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if(collision.CompareTag("PlayerInteraction")) {
            _interactable = false;
        }
    }
}
