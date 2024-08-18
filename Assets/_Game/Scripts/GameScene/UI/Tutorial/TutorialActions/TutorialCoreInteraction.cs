using UnityEngine;

public class TutorialCoreAction : TutorialAction
{
    [SerializeField] private GameObject _clickToContinue;

    private ActionScheduler _actionScheduler;

    private void Awake()
    {
        _actionScheduler = FindObjectOfType<ActionScheduler>();
    }

    private void OnDisable()
    {
        TutorialEvents.OnPlayerNearCore -= OnPlayerNearCore;
        TutorialEvents.OnPlayerMoved -= OnPlayerMoved;
    }

    public override void StartAction()
    {
        _tutorialPlayer.MoveToNextNarratorText();
        TutorialEvents.OnPlayerNearCore += OnPlayerNearCore;
    }

    private void OnPlayerNearCore()
    {
        TutorialEvents.OnPlayerNearCore -= OnPlayerNearCore;
        _clickToContinue.SetActive(true);
        _tutorialPlayer.MoveToNextNarratorText();
        _actionScheduler.ScheduleAction(ExplainEvents, () => Input.GetMouseButtonDown(0));
    }

    private void ExplainEvents()
    {
        _tutorialPlayer.MoveToNextNarratorText();
        _actionScheduler.ScheduleAction(ExplainDiceStep, () => Input.GetMouseButtonDown(0));
    }

    private void ExplainDiceStep()
    {
        _tutorialPlayer.MoveToNextNarratorText();
        _actionScheduler.ScheduleAction(OnAfterDiceStep, () => Input.GetMouseButtonDown(0));
    }

    private void OnAfterDiceStep()
    {
        _clickToContinue.SetActive(false);

        _tutorialPlayer.MoveToNextNarratorText();
        TutorialEvents.OnPlayerMoved += OnPlayerMoved;
    }

    private void OnPlayerMoved()
    {
        TutorialEvents.OnPlayerMoved -= OnPlayerMoved;
        OnActionFinishedInvoke();
    }

    public override void Exit()
    {
    }
}
