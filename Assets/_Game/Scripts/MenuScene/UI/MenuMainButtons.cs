using UnityEngine.UI;
using UnityEngine;

public class MenuMainButtons : GameScreen
{
    [SerializeField] private Button _replayTutorialButton;

    private void Awake()
    {
        _replayTutorialButton.gameObject.SetActive(TutorialManager.Instance.TutorialCompleted);
    }

    public void PlayTheGame()
    {
        SceneLoadManager.Instance.GoMenuToGame();
    }

    public void ReplayTutorial()
    {
        TutorialManager.Instance.CompletedTutorials.Clear();
        SceneLoadManager.Instance.GoMenuToGame();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
