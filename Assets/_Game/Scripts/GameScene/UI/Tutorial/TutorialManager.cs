using System;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoSingleton<TutorialManager>
{
    [field: SerializeField] public bool TutorialsEnabled { get; private set; } = true;
    [field: SerializeField] public List<TutorialPlayer> TutorialList { get; private set; }

    public List<TutorialID> CompletedTutorials = new();
    public TutorialPlayer CurrentTutorial { get; private set; }
    public bool TutorialCompleted => CompletedTutorials.Contains(TutorialID.Upgrades);

    public Vector2 TowerPosition;
    public Vector2 PlacePosition;
    public bool CanPlayerPickTowers = true;
    public bool CanPlayerMove = true;

    public event Action<TutorialID> OnTutorialEnd;

    private void Awake() {
        if(PlayerPrefs.GetInt("TutorialDone", 0) == 1) {
            CompletedTutorials.Add(TutorialID.Upgrades);
        }
    }

    protected override void Init() { }

    public void InstantiateTutorial(TutorialID tutorialID, bool allowMultipleTutorialsAtOnce = false)
    {
        if (!TutorialsEnabled)
        {
            CompletedTutorials.Add(TutorialID.Upgrades);
            return;
        }

        if (!allowMultipleTutorialsAtOnce)
        {
            if (CurrentTutorial != null)
            {
                Debug.LogError($"[TutorialManager] - Trying to spawn tutorial {tutorialID}, but there is already tutorial {CurrentTutorial.TutorialID} playing. Returning");
                return;
            }
        }
        
        foreach (TutorialPlayer tutorial in TutorialList)
        {
            if (tutorial.TutorialID == tutorialID)
            {
                SpawnTutorial(tutorial);
                return;
            }
        }

        Debug.LogError($"[TutorialManager] - Cannot spawn tutorial {tutorialID}! Check if it exists!");
    }

    public bool IsTutorialPlaying(TutorialID tutorialID)
    {
        if (CurrentTutorial == null)
        {
            return false;
        }

        return CurrentTutorial.TutorialID == tutorialID;
    }

    public void DestroyCurrentTutorial()
    {
        if (CurrentTutorial != null)
        {
            CurrentTutorial.OnTutorialEnd -= OnCurrentTutorialEnd;
            Destroy(CurrentTutorial.gameObject);
        }
    }

    private void SpawnTutorial(TutorialPlayer tutorial)
    {
        CurrentTutorial = Instantiate(tutorial, FindObjectOfType<BaseCanvasController>().transform);
        if (CurrentTutorial == null)
        {
            Debug.LogError($"[TutorialManager] - Cannot instantiate tutorial {tutorial.TutorialID}! Check if it exists!");
        }
        else
        {
            CurrentTutorial.OnTutorialEnd += OnCurrentTutorialEnd;
        }
    }

    private void OnCurrentTutorialEnd(TutorialID tutorialID)
    {
        if (CurrentTutorial != null)
        {
            CurrentTutorial.OnTutorialEnd -= OnCurrentTutorialEnd;
            CurrentTutorial = null;
        }

        if(tutorialID == TutorialID.Upgrades) {
            PlayerPrefs.SetInt("TutorialDone", 1);
        }

        CompletedTutorials.Add(tutorialID);
        OnTutorialEnd?.Invoke(tutorialID);
    }
}
