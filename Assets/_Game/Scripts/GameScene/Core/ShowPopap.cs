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
            if (TutorialManager.Instance.IsTutorialPlaying(TutorialID.Core) ||
                TutorialManager.Instance.IsTutorialPlaying(TutorialID.Upgrades))
            {
                TutorialEvents.OnPlayerNearCoreInvoke();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if(collision.CompareTag("Player")) {
            _toShow.SetActive(false);
        }
    }
}
