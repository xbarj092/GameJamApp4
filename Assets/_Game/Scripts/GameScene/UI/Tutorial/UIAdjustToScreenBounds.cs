using UnityEngine;

public class UIAdjustToScreenBounds : MonoBehaviour
{
    private RectTransform _canvasRect;
    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        AdjustPositionToScreenBounds();
    }

    void Update()
    {
        AdjustPositionToScreenBounds();
    }

    private void AdjustPositionToScreenBounds()
    {
        if (_canvasRect == null)
        {
            _canvasRect = FindObjectOfType<Canvas>().GetComponent<RectTransform>();
        }

        Vector2 size = rectTransform.rect.size;
        Vector3 position = rectTransform.localPosition;

        float minX = -_canvasRect.rect.width / 2 + size.x / 2;
        float maxX = _canvasRect.rect.width / 2 - size.x / 2;
        float minY = -_canvasRect.rect.height / 2 + size.y / 2;
        float maxY = _canvasRect.rect.height / 2 - size.y / 2;

        position.x = Mathf.Clamp(position.x, minX, maxX);
        position.y = Mathf.Clamp(position.y, minY, maxY);

        rectTransform.localPosition = position;
    }
}
