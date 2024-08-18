using UnityEngine;

public class TutorialCoreAction : TutorialAction
{
    [SerializeField] private GameObject _towerTypes;
    [SerializeField] private GameObject _clickToContinue;

    private ActionScheduler _actionScheduler;

    private void Awake()
    {
        _actionScheduler = FindObjectOfType<ActionScheduler>();
    }

    private void OnDisable()
    {
        TutorialEvents.OnPlayerNearCore -= OnPlayerNearCore;
        TutorialEvents.OnTowerPurchased += OnTowerPurchased;
        TutorialEvents.OnTowerPlaced -= OnTowerPlaced;
    }

    public override void StartAction()
    {
        _tutorialPlayer.MoveToNextNarratorText();
        TutorialEvents.OnPlayerNearCore += OnPlayerNearCore;
    }

    private void OnPlayerNearCore()
    {
        TutorialEvents.OnPlayerNearCore -= OnPlayerNearCore;
        TutorialManager.Instance.CanPlayerMove = false;
        _towerTypes.SetActive(true);
        _clickToContinue.SetActive(true);
        _tutorialPlayer.MoveToNextNarratorText();
        _actionScheduler.ScheduleAction(OnBeforePlayerBuy, () => Input.GetMouseButtonDown(0));
    }

    private void OnBeforePlayerBuy()
    {
        _tutorialPlayer.MoveToNextNarratorText();
        _clickToContinue.SetActive(false);
        _towerTypes.SetActive(false);
        TutorialEvents.OnTowerPurchased += OnTowerPurchased;
    }

    private void OnTowerPurchased()
    {
        TutorialEvents.OnTowerPurchased -= OnTowerPurchased;
        _tutorialPlayer.MoveToNextNarratorText();
        TutorialManager.Instance.CanPlayerMove = true;
        TutorialEvents.OnTowerPlaced += OnTowerPlaced;
    }

    private void OnTowerPlaced()
    {
        TutorialEvents.OnTowerPlaced -= OnTowerPlaced;
        TutorialManager.Instance.CanPlayerPickTowers = false;
        OnActionFinishedInvoke();
    }

    public override void Exit()
    {
        TutorialManager.Instance.InstantiateTutorial(TutorialID.Replacing);
    }
}
