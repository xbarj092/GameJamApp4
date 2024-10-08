using System;
using UnityEngine;

public class MouseInputHandler : IInteractionHandler
{
    private PlayerInteractions _playerInteractions;
    private LayerMask _layerMask;

    public event Action<ITowerBase> OnTowerPickedUp;
    public event Action OnTowerPlaced;
    public event Action<ITowerBase> OnTowerHighlighted;
    public event Action OnTowerLowlighted;

    public MouseInputHandler(PlayerInteractions playerInteractions, LayerMask layerMask)
    {
        _playerInteractions = playerInteractions;
        _layerMask = layerMask;
    }

    public void HandleInteraction()
    {
        if (Camera.main != null && TutorialManager.Instance.CanPlayerPickTowers && ScreenManager.Instance.ActiveGameScreen == null)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            HandleMouseClick(mousePosition);
            //HandleMouseHover(mousePosition);
        }
    }

    private void HandleMouseClick(Vector2 mousePosition)
    {
        if(HandlePickUpAction(mousePosition)) return;
        if(HandlePlaceAction()) return;

    }

    private bool HandlePickUpAction(Vector2 mousePosition)
    {
        /*if (IsInRangeOfPlayer())
        {*/
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, 2f, _layerMask);
            if (hit.collider != null && hit.collider.CompareTag("TowerInteraction"))
            {
                ITowerBase tower = hit.collider.gameObject.GetComponentInParent<ITowerBase>();
                if (tower != null && tower.IsInteractable())
                {
                    OnTowerPickedUp?.Invoke(tower);
                    return true;
                }
            }
        //}
        return false;
    }

    private bool HandlePlaceAction()
    {
        OnTowerPlaced?.Invoke();
        return true;
    }

    public void HandleMouseHover(Vector2 mousePosition)
    {
        if (TutorialManager.Instance.CanPlayerPickTowers)
        {
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, 2f, _layerMask);
            if (hit.collider != null && hit.collider.CompareTag("TowerInteraction"))
            {
                ITowerBase tower = hit.collider.gameObject.GetComponentInParent<ITowerBase>();
                if (tower != null && tower.IsInteractable())
                {
                    OnTowerHighlighted?.Invoke(tower);
                }
            }
            else
            {
                OnTowerLowlighted?.Invoke();
            }
        }
    }

    private bool IsInRangeOfPlayer()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        Vector3 playerPosition = _playerInteractions.transform.position;

        Vector3 direction = mousePosition - playerPosition;
        float distance = direction.magnitude;

        return distance <= _playerInteractions.MaxRange;
    }
}
