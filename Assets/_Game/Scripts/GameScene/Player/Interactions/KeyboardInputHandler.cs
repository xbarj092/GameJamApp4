public class KeyboardInputHandler : IInteractionHandler
{
    public void HandleInteraction()
    {
        if (ScreenManager.Instance.ActiveGameScreen.GameScreenType == GameScreenType.Pause)
        {
            ScreenEvents.OnGameScreenClosedInvoke(GameScreenType.Pause);
        }
        else
        {
            ScreenEvents.OnGameScreenOpenedInvoke(GameScreenType.Pause);
        }
    }
}
