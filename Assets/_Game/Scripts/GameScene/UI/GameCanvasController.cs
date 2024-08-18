using UnityEngine;

public class GameCanvasController : BaseCanvasController
{
    [SerializeField] private GameOverScreen _gameOverScreenPrefab;

    protected override GameScreen GetRelevantScreen(GameScreenType gameScreenType)
    {
        return gameScreenType switch
        {
            GameScreenType.GameOver => Instantiate(_gameOverScreenPrefab, transform),
            _ => base.GetRelevantScreen(gameScreenType),
        };
    }
}
