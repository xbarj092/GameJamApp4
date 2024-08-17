using System;
using UnityEngine;

public class MouseInputHandler : IInteractionHandler
{
    private LayerMask _layerMask;

    public event Action<ITowerBase> OnTowerPickedUp;
    public event Action OnTowerPlaced;
    public event Action<ITowerBase> OnTowerHighlighted;

    public MouseInputHandler(LayerMask layerMask)
    {
        _layerMask = layerMask;
    }

    public void HandleInteraction()
    {
        if (Camera.main != null)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            HandleMouseClick(mousePosition);
            HandleMouseHover(mousePosition);
        }
    }

    private void HandleMouseClick(Vector2 mousePosition)
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, 2f, _layerMask);
            if (true) // can place
            {
                OnTowerPlaced?.Invoke();
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, 2f, _layerMask);
            if (hit.collider != null && hit.collider.gameObject.TryGetComponent(out ITowerBase tower))
            {
                OnTowerPickedUp?.Invoke(tower);
            }
        }
    }

    private void HandleMouseHover(Vector2 mousePosition)
    {
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, 2f, _layerMask);
        if (hit.collider != null && hit.collider.gameObject.TryGetComponent(out ITowerBase tower))
        {
            OnTowerHighlighted?.Invoke(tower);
        }
    }
}
