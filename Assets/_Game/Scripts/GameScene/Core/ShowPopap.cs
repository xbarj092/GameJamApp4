using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPopap : MonoBehaviour
{
    [SerializeField] private GameObject _toShow;

    private void Awake() {
        _toShow.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("Player")) {
            _toShow.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if(collision.CompareTag("Player")) {
            _toShow.SetActive(false);
        }
    }
}
