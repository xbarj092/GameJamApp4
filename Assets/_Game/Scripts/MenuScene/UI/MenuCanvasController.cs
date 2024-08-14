using UnityEngine;

public class MenuCanvasController : BaseCanvasController
{
    [SerializeField] private MenuMainButtons _menuMainButtonsPrefab;

    protected override GameScreen GetRelevantScreen(GameScreenType gameScreenType)
    {
        return gameScreenType switch
        {
            GameScreenType.MenuMain => Instantiate(_menuMainButtonsPrefab, transform),
            _ => base.GetRelevantScreen(gameScreenType),
        };
    }
}
