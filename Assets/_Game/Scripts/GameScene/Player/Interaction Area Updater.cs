using UnityEngine;

public class InteractionAreaUpdater : MonoBehaviour
{
    [SerializeField] private float scaleIncrease;

    public void IncreaseArea(float increaseAmount)
    {
        var newScale = transform.localScale.x + increaseAmount;
        transform.localScale = new Vector2(newScale, newScale);
    }

    public float GetAreaRadius() => transform.localScale.x/2;
}
