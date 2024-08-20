using UnityEngine;

public class TutorialCoreAction : TutorialAction
{
    [SerializeField] private GameObject _towerTypes;
    [SerializeField] private GameObject _playerUpgradeTypes;
    [SerializeField] private GameObject _clickToContinue;

    [Header("TextTransforms")]
    [SerializeField] private Transform _towerPopupTransform;
    [SerializeField] private Transform _upgradePopupTransform;

    [Header("Cutouts")]
    [SerializeField] private GameObject _coreCutout;
    [SerializeField] private GameObject _shopItemCutout;
    [SerializeField] private RectTransform _playerCutout;

    [SerializeField] private GameObject _background;

    private Vector2 _corePosition;

    private PlayerInteractions _player;
    private ActionScheduler _actionScheduler;

    private void Awake()
    {
        _player = FindObjectOfType<PlayerInteractions>();
        _actionScheduler = FindObjectOfType<ActionScheduler>();
    }

    private void OnDisable()
    {
        TutorialEvents.OnPlayerNearCore -= OnPlayerNearCore;
        TutorialEvents.OnTowerPurchased -= OnTowerPurchased;
        TutorialEvents.OnTowerPlaced -= OnTowerPlaced;
    }

    private void Update()
    {
        Vector3 newWorldPosition = _player.transform.position;
        Vector2 newScreenPosition = Camera.main.WorldToScreenPoint(newWorldPosition);

        _playerCutout.transform.rotation = _player.transform.rotation;
        _playerCutout.transform.position = newScreenPosition;
    }

    public override void StartAction()
    {
        _corePosition = FindObjectOfType<CoreManager>().transform.position + TRANSFORM_POSITION_OFFSET;
        _playerCutout.gameObject.SetActive(true);
        _playerCutout.anchorMin = new Vector2(0, 0);
        _playerCutout.anchorMax = new Vector2(0, 0);
        _coreCutout.SetActive(true);
        _background.SetActive(true);
        _tutorialPlayer.SetTextLocalPosition(_corePosition);
        _tutorialPlayer.MoveToNextNarratorText();
        TutorialEvents.OnPlayerNearCore += OnPlayerNearCore;
    }

    private void OnPlayerNearCore()
    {
        TutorialEvents.OnPlayerNearCore -= OnPlayerNearCore;
        _coreCutout.SetActive(false);
        TutorialManager.Instance.CanPlayerMove = false;
        TutorialManager.Instance.CanPlayerPickTowers = false;
        _towerTypes.SetActive(true);
        _clickToContinue.SetActive(true);
        _tutorialPlayer.SetTextLocalPosition(_towerPopupTransform.localPosition);
        _tutorialPlayer.MoveToNextNarratorText();
        _actionScheduler.ScheduleAction(OnSecondTable, () => Input.GetMouseButtonDown(0));
    }

    private void OnSecondTable()
    {
        _towerTypes.SetActive(false);
        _playerUpgradeTypes.SetActive(true);
        _tutorialPlayer.SetTextLocalPosition(_upgradePopupTransform.localPosition);
        _tutorialPlayer.MoveToNextNarratorText();
        _actionScheduler.ScheduleAction(OnBeforePlayerBuy, () => Input.GetMouseButtonDown(0));
    }

    private void OnBeforePlayerBuy()
    {
        _tutorialPlayer.SetTextLocalPosition(_corePosition);
        _tutorialPlayer.MoveToNextNarratorText();
        _clickToContinue.SetActive(false);
        _shopItemCutout.SetActive(true);
        TutorialManager.Instance.CanPlayerPickTowers = true;
        _playerUpgradeTypes.SetActive(false);
        TutorialEvents.OnTowerPurchased += OnTowerPurchased;
    }

    private void OnTowerPurchased()
    {
        _shopItemCutout.SetActive(false);
        _background.SetActive(false);
        TutorialEvents.OnTowerPurchased -= OnTowerPurchased;
        _tutorialPlayer.SetTextLocalPosition(_towerPopupTransform.localPosition);
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
