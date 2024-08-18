public class KeyboardInputHandler : IInteractionHandler
{
    public void HandleInteraction()
    {
        if (!TutorialManager.Instance.TutorialCompleted)
        {
            return;
        }

        if (ScreenManager.Instance.ActiveGameScreen != null && 
            ScreenManager.Instance.ActiveGameScreen.GameScreenType == GameScreenType.Pause)
        {
            ScreenEvents.OnGameScreenClosedInvoke(GameScreenType.Pause);
        }
        else
        {
            ScreenEvents.OnGameScreenOpenedInvoke(GameScreenType.Pause);
        }
    }
}
