using UnityEngine;

public class TutorialMovementAction : TutorialAction
{
    private void OnDisable()
    {
        TutorialEvents.OnPlayerMoved -= OnPlayerMoved;
    }

    public override void StartAction()
    {
        //Vector2 position = FindObjectOfType<PlayerInteractions>().transform.position + TRANSFORM_POSITION_OFFSET;
        Vector2 position = FindObjectOfType<CoreManager>().transform.position + TRANSFORM_POSITION_OFFSET;
        _tutorialPlayer.SetTextLocalPosition(position);
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
