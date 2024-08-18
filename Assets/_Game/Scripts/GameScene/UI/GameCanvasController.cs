using UnityEngine;

public class GameCanvasController : BaseCanvasController
{
    [SerializeField] private HUDScreen _hudScreenPrefab;
    [SerializeField] private GameOverScreen _gameOverScreenPrefab;
    [SerializeField] private PauseScreen _pauseScreenPrefab;

    protected override GameScreen GetRelevantScreen(GameScreenType gameScreenType)
    {
        return gameScreenType switch
        {
            GameScreenType.HUD => Instantiate(_hudScreenPrefab, transform),
            GameScreenType.GameOver => Instantiate(_gameOverScreenPrefab, transform),
            GameScreenType.Pause => Instantiate(_pauseScreenPrefab, transform),
            _ => base.GetRelevantScreen(gameScreenType),
        };
    }

    protected override GameScreen GetActiveGameScreen(GameScreenType gameScreenType)
    {
        return gameScreenType switch
        {
            GameScreenType.Pause => Instantiate(_hudScreenPrefab, transform),
            _ => base.GetActiveGameScreen(gameScreenType)
        };
    }
}
