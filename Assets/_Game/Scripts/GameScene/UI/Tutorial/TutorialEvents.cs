using System;

public static class TutorialEvents
{
    public static event Action OnPlayerMoved;
    public static void OnPlayerMovedInvoke()
    {
        OnPlayerMoved?.Invoke();
    }

    public static event Action OnPlayerNearCore;
    public static void OnPlayerNearCoreInvoke()
    {
        OnPlayerNearCore?.Invoke();
    }
}
