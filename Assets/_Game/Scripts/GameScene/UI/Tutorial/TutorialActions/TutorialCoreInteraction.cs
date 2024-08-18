using UnityEngine;
using UnityEngine.UIElements;

public class TutorialCoreAction : TutorialAction
{
    [SerializeField] private GameObject _towerTypes;
    [SerializeField] private GameObject _clickToContinue;

    [Header("TextTransforms")]
    [SerializeField] private Transform _popupTransform;
    private Vector2 _corePosition;

    private ActionScheduler _actionScheduler;

    private void Awake()
    {
        _actionScheduler = FindObjectOfType<ActionScheduler>();
    }

    private void OnDisable()
    {
        TutorialEvents.OnPlayerNearCore -= OnPlayerNearCore;
        TutorialEvents.OnTowerPurchased -= OnTowerPurchased;
        TutorialEvents.OnTowerPlaced -= OnTowerPlaced;
    }

    public override void StartAction()
    {
        _corePosition = FindObjectOfType<CoreManager>().transform.position + TRANSFORM_POSITION_OFFSET;
        _tutorialPlayer.SetTextPosition(_corePosition);
        _tutorialPlayer.MoveToNextNarratorText();
        TutorialEvents.OnPlayerNearCore += OnPlayerNearCore;
    }

    private void OnPlayerNearCore()
    {
        TutorialEvents.OnPlayerNearCore -= OnPlayerNearCore;
        TutorialManager.Instance.CanPlayerMove = false;
        TutorialManager.Instance.CanPlayerPickTowers = false;
        _towerTypes.SetActive(true);
        _clickToContinue.SetActive(true);
        _tutorialPlayer.SetTextPosition(_popupTransform.localPosition);
        _tutorialPlayer.MoveToNextNarratorText();
        _actionScheduler.ScheduleAction(OnBeforePlayerBuy, () => Input.GetMouseButtonDown(0));
    }

    private void OnBeforePlayerBuy()
    {
        _tutorialPlayer.SetTextPosition(_corePosition);
        _tutorialPlayer.MoveToNextNarratorText();
        _clickToContinue.SetActive(false);
        TutorialManager.Instance.CanPlayerPickTowers = true;
        _towerTypes.SetActive(false);
        TutorialEvents.OnTowerPurchased += OnTowerPurchased;
    }

    private void OnTowerPurchased()
    {
        TutorialEvents.OnTowerPurchased -= OnTowerPurchased;
        _tutorialPlayer.SetTextPosition(_popupTransform.localPosition);
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
