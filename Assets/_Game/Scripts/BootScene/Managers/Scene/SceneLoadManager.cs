using UnityEngine;

public class SceneLoadManager : MonoSingleton<SceneLoadManager>
{
    protected override void Init()
    {
        base.Init();
        GoBootToMenu();
    }

    public void GoBootToMenu()
    {
        SceneLoader.OnSceneLoadDone += OnBootToMenuLoadDone;
        SceneLoader.LoadScene(SceneLoader.Scenes.MenuScene);
    }

    private void OnBootToMenuLoadDone(SceneLoader.Scenes scene)
    {
        PlayMenuMusic();
        Time.timeScale = 1;
        SceneLoader.OnSceneLoadDone -= OnBootToMenuLoadDone;
    }

    public void GoMenuToGame()
    {
        SceneLoader.OnSceneLoadDone += OnMenuToGameLoadDone;
        ResetTimer();
        GrantCoins();
        SceneLoader.LoadScene(SceneLoader.Scenes.GameScene, toUnload: SceneLoader.Scenes.MenuScene);
    }

    private void OnMenuToGameLoadDone(SceneLoader.Scenes scenes)
    {
        Time.timeScale = 1;
        TryShowTutorial();
        PlayAmbience();
        SceneLoader.OnSceneLoadDone -= OnMenuToGameLoadDone;
    }

    public void GoGameToMenu()
    {
        SceneLoader.OnSceneLoadDone += OnGameToMenuLoadDone;
        SceneLoader.LoadScene(SceneLoader.Scenes.MenuScene, toUnload: SceneLoader.Scenes.GameScene);
    }

    private void OnGameToMenuLoadDone(SceneLoader.Scenes scenes)
    {
        Time.timeScale = 1;
        PlayMenuMusic();
        SceneLoader.OnSceneLoadDone -= OnGameToMenuLoadDone;
    }

    public void RestartGame()
    {
        SceneLoader.OnSceneLoadDone += OnRestartGameDone;
        ResetTimer();
        GrantCoins();
        SceneLoader.LoadScene(SceneLoader.Scenes.GameScene, toUnload: SceneLoader.Scenes.GameScene);
    }

    private void OnRestartGameDone(SceneLoader.Scenes scenes)
    {
        Time.timeScale = 1;
        TryShowTutorial();
        PlayAmbience();
        SceneLoader.OnSceneLoadDone -= OnRestartGameDone;
    }

    public bool IsSceneLoaded(SceneLoader.Scenes sceneToCheck)
    {
        return SceneLoader.IsSceneLoaded(sceneToCheck);
    }

    private void PlayMenuMusic()
    {
        if (AudioManager.Instance.IsPlaying(SoundType.Ambience))
        {
            AudioManager.Instance.Stop(SoundType.Ambience);
        }
        
        if (!AudioManager.Instance.IsPlaying(SoundType.Menu))
        {
            AudioManager.Instance.Play(SoundType.Menu);
        }
    }

    private void PlayAmbience()
    {
        if (AudioManager.Instance.IsPlaying(SoundType.Menu))
        {
            AudioManager.Instance.Stop(SoundType.Menu);
        }

        if (!AudioManager.Instance.IsPlaying(SoundType.Ambience))
        {
            AudioManager.Instance.Play(SoundType.Ambience);
        }
    }

    private void TryShowTutorial()
    {
        TutorialManager.Instance.CanPlayerMove = true;
        TutorialManager.Instance.CanPlayerPickTowers = true;
        foreach (TutorialPlayer tutorial in TutorialManager.Instance.TutorialList)
        {
            if (!TutorialManager.Instance.CompletedTutorials.Contains(tutorial.TutorialID))
            {
                TutorialManager.Instance.InstantiateTutorial(tutorial.TutorialID);
                return;
            }
        }
    }

    private void ResetTimer()
    {
        LocalDataStorage.Instance.PlayerData.PlayerStats = new(0);
    }

    private void GrantCoins()
    {
        if (TutorialManager.Instance.TutorialCompleted)
        {
            LocalDataStorage.Instance.PlayerData.CurrencyData = new(12);
        }
    }
}
