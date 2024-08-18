using System;
using UnityEngine;

public static class TutorialEvents
{
    // movement tutorial
    public static event Action OnPlayerMoved;
    public static void OnPlayerMovedInvoke()
    {
        OnPlayerMoved?.Invoke();
    }

    // core tutorial
    public static event Action OnPlayerNearCore;
    public static void OnPlayerNearCoreInvoke()
    {
        OnPlayerNearCore?.Invoke();
    }

    public static event Action OnTowerPurchased;
    public static void OnTowerPurchasedInvoke()
    {
        OnTowerPurchased?.Invoke();
    }

    public static event Action OnTowerPlaced;
    public static void OnTowerPlacedInvoke()
    {
        OnTowerPlaced?.Invoke();
    }

    // replace tutorial
    public static event Action OnTowerPickedUp;
    public static void OnTowerPickedUpInvoke()
    {
        OnTowerPickedUp?.Invoke();
    }

    public static Action<Vector2> OnEnemySpawned;
    public static void OnEnemySpawnedInvoke(Vector2 position)
    {
        OnEnemySpawned?.Invoke(position);
    }

    public static Action OnEnemyKilled;
    public static void OnEnemyKilledInvoke()
    {
        OnEnemyKilled?.Invoke();
    }

    // upgrade tutorial
    public static Action OnCoinPickedUp;
    public static void OnCoinPickedUpInvoke()
    {
        OnCoinPickedUp?.Invoke();
    }

    public static Action OnShopItemsDisabled;
    public static void OnShopItemsDisabledInvoke()
    {
        OnShopItemsDisabled?.Invoke();
    }

    // end
    public static event Action OnTutorialCompleted;
    public static void OnTutorialCompletedInvoke()
    {
        OnTutorialCompleted?.Invoke();
    }
}
