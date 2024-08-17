using UnityEngine;

public class PlayerInRangeChecker : MonoBehaviour
{
    public bool IsPlayerInRange;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            IsPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            IsPlayerInRange = false;
        }
    }
}
