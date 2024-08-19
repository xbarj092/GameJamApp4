using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionControler : MonoBehaviour
{
    public bool Interactable { get; private set; } = false;

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("PlayerInteraction")) {
            Interactable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if(collision.CompareTag("PlayerInteraction")) {
            Interactable = false;
        }
    }
}
