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

    public static Action<bool> OnEnemyKilled;
    public static void OnEnemyKilledInvoke(bool coreDeath)
    {
        OnEnemyKilled?.Invoke(coreDeath);
    }

    // upgrade tutorial
    public static Action<Coin> OnCoinPickedUp;
    public static void OnCoinPickedUpInvoke(Coin pickedUpCoin)
    {
        OnCoinPickedUp?.Invoke(pickedUpCoin);
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
