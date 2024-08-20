using UnityEngine.UI;
using UnityEngine;

public class MenuMainButtons : GameScreen
{
    [SerializeField] private GameObject _replayTutorialButton;
    [SerializeField] private GameObject _playButton;

    private void Start()
    {
        _replayTutorialButton.gameObject.SetActive(TutorialManager.Instance.TutorialCompleted);
        _playButton.gameObject.SetActive(!TutorialManager.Instance.TutorialCompleted);
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
