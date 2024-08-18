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

    public static Action OnPlayerCorrectPosition;
    public static void OnPlayerCorrectPositionInvoke()
    {
        OnPlayerCorrectPosition?.Invoke();
    }

    public static Action<Vector2> OnEnemySpawned;
    public static void OnEnemySpawnedInvoke(Vector2 position)
    {
        OnEnemySpawned?.Invoke(position);
    }

    // end
    public static event Action OnTutorialCompleted;
    public static void OnTutorialCompletedInvoke()
    {
        OnTutorialCompleted?.Invoke();
    }
}
