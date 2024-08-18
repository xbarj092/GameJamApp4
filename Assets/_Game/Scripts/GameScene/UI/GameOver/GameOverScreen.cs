public class GameOverScreen : GameScreen
{
    // bound from inspector
    public void PlayAgain()
    {
        SceneLoadManager.Instance.RestartGame();
    }

    // bound from inspector
    public void MainMenu()
    {
        SceneLoadManager.Instance.GoGameToMenu();
    }
}
