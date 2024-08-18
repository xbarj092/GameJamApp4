using UnityEngine;

public class TutorialMovementAction : TutorialAction
{
    [SerializeField] private GameObject _clickToContinue;

    private void OnDisable()
    {
        TutorialEvents.OnPlayerMoved -= OnPlayerMoved;
    }

    public override void StartAction()
    {
        Vector2 position = FindObjectOfType<PlayerInteractions>().transform.position + TRANSFORM_POSITION_OFFSET;
        _tutorialPlayer.SetTextPosition(position);
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
        TutorialManager.Instance.InstantiateTutorial(TutorialID.Core);
    }
}
