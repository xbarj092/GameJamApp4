using UnityEngine;

public class TutorialReplacingTowersAction : TutorialAction
{
    [SerializeField] private GameObject _eventExplaining;
    [SerializeField] private GameObject _clickToContinue;

    private ActionScheduler _actionScheduler;

    private void Awake()
    {
        _actionScheduler = FindObjectOfType<ActionScheduler>();
    }

    public override void StartAction()
    {
        _tutorialPlayer.MoveToNextNarratorText();
        _clickToContinue.SetActive(true);
        _actionScheduler.ScheduleAction(ExplainEvents, () => Input.GetMouseButtonDown(0));
    }

    private void ExplainEvents()
    {
        _tutorialPlayer.MoveToNextNarratorText();
        _eventExplaining.SetActive(true);
        _actionScheduler.ScheduleAction(ExplainDiceStep, () => Input.GetMouseButtonDown(0));
    }

    private void ExplainDiceStep()
    {
        _tutorialPlayer.MoveToNextNarratorText();
        _eventExplaining.SetActive(false);
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
