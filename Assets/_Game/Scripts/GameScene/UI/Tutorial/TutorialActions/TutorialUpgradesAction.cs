using UnityEngine;

public class TutorialUpgradesAction : TutorialAction
{
    [SerializeField] private GameObject _clickToContinue;

    private ActionScheduler _actionScheduler;

    private void Awake()
    {
        _actionScheduler = FindObjectOfType<ActionScheduler>();
    }

    private void OnDisable()
    {
        TutorialEvents.OnCoinPickedUp -= OnCoinPickedUp;
        TutorialEvents.OnPlayerNearCore -= OnPlayerNearCore;
        TutorialEvents.OnTowerPlaced -= OnTowerPlaced;
    }

    public override void StartAction()
    {
        // MoveTextToNextPosition();
        _tutorialPlayer.MoveToNextNarratorText();
        TutorialEvents.OnCoinPickedUp += OnCoinPickedUp;
    }

    private void OnCoinPickedUp()
    {
        TutorialEvents.OnCoinPickedUp -= OnCoinPickedUp;
        // MoveTextToNextPosition();
        _tutorialPlayer.MoveToNextNarratorText();
        TutorialEvents.OnPlayerNearCore += OnPlayerNearCore;
    }

    private void OnPlayerNearCore()
    {
        TutorialEvents.OnPlayerNearCore -= OnPlayerNearCore;
        TutorialManager.Instance.CanPlayerMove = false;
        TutorialManager.Instance.CanPlayerPickTowers = false;
        // MoveTextToNextPosition();
        _tutorialPlayer.MoveToNextNarratorText();
        TutorialEvents.OnShopItemsDisabledInvoke();
        TutorialEvents.OnTowerPurchased += OnTowerPurchased;
    }

    private void OnTowerPurchased()
    {
        TutorialEvents.OnTowerPurchased -= OnTowerPurchased;
        TutorialManager.Instance.CanPlayerMove = true;
        TutorialManager.Instance.CanPlayerPickTowers = true;
        TutorialEvents.OnTowerPlaced += OnTowerPlaced;
    }

    private void OnTowerPlaced()
    {
        TutorialEvents.OnTowerPlaced -= OnTowerPlaced;
        _clickToContinue.SetActive(true);
        TutorialManager.Instance.CanPlayerMove = false;
        TutorialManager.Instance.CanPlayerPickTowers = false;

        // MoveTextToNextPosition();
        _tutorialPlayer.MoveToNextNarratorText();
        _actionScheduler.ScheduleAction(OnAfterTowerPlaced, () => Input.GetMouseButtonDown(0));
    }

    private void OnAfterTowerPlaced()
    {
        // MoveTextToNextPosition();
        _tutorialPlayer.MoveToNextNarratorText();
        _actionScheduler.ScheduleAction(OnLastText, () => Input.GetMouseButtonDown(0));
    }

    private void OnLastText()
    {
        // MoveTextToNextPosition();
        _tutorialPlayer.MoveToNextNarratorText();
        _actionScheduler.ScheduleAction(OnActionFinishedInvoke, () => Input.GetMouseButtonDown(0));
    }

    public override void Exit()
    {
        TutorialManager.Instance.CanPlayerMove = true;
        TutorialManager.Instance.CanPlayerPickTowers = true;
        TutorialEvents.OnTutorialCompletedInvoke();
    }
}
