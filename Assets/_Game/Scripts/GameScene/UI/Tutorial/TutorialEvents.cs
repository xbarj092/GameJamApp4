using System;

public static class TutorialEvents
{
    public static event Action OnPlayerMoved;
    public static void OnPlayerMovedInvoke()
    {
        OnPlayerMoved?.Invoke();
    }

    public static event Action OnPlayerAttacked;
    public static void OnPlayerAttackedInvoke()
    {
        OnPlayerAttacked?.Invoke();
    }
}
