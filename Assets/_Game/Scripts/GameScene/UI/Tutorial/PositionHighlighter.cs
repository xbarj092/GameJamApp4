using UnityEngine;

public class PositionHighlighter : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;

    public void HighlightPosition(Vector3 position, float scale = 1)
    {
        _renderer.enabled = true;
        transform.position = position;
        transform.localScale *= scale;
    }

    public void LowlightPosition()
    {
        _renderer.enabled = false;
    }
}
