using UnityEngine;

public class TutorialAttackAction : TutorialAction
{
    [SerializeField] private GameObject _clickToContinue;

    private ActionScheduler _actionScheduler;

    private void Awake()
    {
        _actionScheduler = FindObjectOfType<ActionScheduler>();
    }

    public override void StartAction()
    {
        _actionScheduler.ScheduleAction(HideText, () => Input.GetMouseButtonDown(0));
    }

    private void HideText()
    {
        TutorialEvents.OnPlayerAttacked += OnPlayerAttacked;
    }

    private void OnPlayerAttacked()
    {
        TutorialEvents.OnPlayerAttacked -= OnPlayerAttacked;
        _actionScheduler.ScheduleAction(OnActionFinishedInvoke, () => Input.GetMouseButtonDown(0));
    }

    public override void Exit()
    {
    }
}
