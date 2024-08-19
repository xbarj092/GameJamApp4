using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialUpgradesAction : TutorialAction
{
    [SerializeField] private GameObject _clickToContinue;

    [Header("TextTransforms")]
    [SerializeField] private Transform _upgradePlaceTransform;

    [Header("Cutouts")]
    [SerializeField] private RectTransform _coinCutout;
    [SerializeField] private GameObject _upgradeTowerCutout;
    [SerializeField] private RectTransform _levellingTowerCutout;

    [SerializeField] private GameObject _background;

    private List<RectTransform> _coinCutouts = new();
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
        _background.SetActive(true);
        Coin[] coins = FindObjectsOfType<Coin>();
        Vector3 worldPosition = Vector3.zero;
        foreach (Coin coin in coins)
        {
            worldPosition = coin.transform.position;
            RectTransform cutout = Instantiate(_coinCutout, transform);
            cutout.gameObject.SetActive(true);
            cutout.anchorMin = new(0, 0);
            cutout.anchorMax = new(0, 0);
            cutout.transform.position = Camera.main.WorldToScreenPoint(worldPosition);
            _coinCutouts.Add(cutout);
        }

        Vector2 screenPosition = Camera.main.WorldToScreenPoint(worldPosition + new Vector3(0, 2, 0));
        _tutorialPlayer.GetTextTransform().anchorMin = new(0, 0);
        _tutorialPlayer.GetTextTransform().anchorMax = new(0, 0);

        _tutorialPlayer.SetTextPosition(screenPosition);
        _tutorialPlayer.MoveToNextNarratorText();
        TutorialEvents.OnCoinPickedUp += OnCoinPickedUp;
    }

    private void OnCoinPickedUp()
    {
        StopAllCoroutines();
        StartCoroutine(DelayedCoinPickedUp());
    }

    private IEnumerator DelayedCoinPickedUp()
    {
        yield return null;
        foreach (RectTransform cutout in _coinCutouts)
        {
            cutout.gameObject.SetActive(false);
        }

        if (FindObjectsOfType<Coin>().Length == 0)
        {
            _background.SetActive(false);
            TutorialEvents.OnCoinPickedUp -= OnCoinPickedUp;
            _tutorialPlayer.SetTextLocalPosition(FindObjectOfType<CoreManager>().transform.position + TRANSFORM_POSITION_OFFSET);
            _tutorialPlayer.MoveToNextNarratorText();
            TutorialEvents.OnPlayerNearCore += OnPlayerNearCore;
        }
    }

    private void OnPlayerNearCore()
    {
        _background.SetActive(true);
        _upgradeTowerCutout.SetActive(true);
        TutorialEvents.OnPlayerNearCore -= OnPlayerNearCore;
        TutorialManager.Instance.CanPlayerMove = false;
        TutorialManager.Instance.CanPlayerPickTowers = false;
        _tutorialPlayer.MoveToNextNarratorText();
        TutorialEvents.OnShopItemsDisabledInvoke();
        TutorialEvents.OnTowerPurchased += OnTowerPurchased;
    }

    private void OnTowerPurchased()
    {
        _background.SetActive(false);
        _upgradeTowerCutout.SetActive(false);
        _tutorialPlayer.TextFadeAway();
        TutorialEvents.OnTowerPurchased -= OnTowerPurchased;
        TutorialManager.Instance.CanPlayerMove = true;
        TutorialManager.Instance.CanPlayerPickTowers = true;
        TutorialEvents.OnTowerPlaced += OnTowerPlaced;
    }

    private void OnTowerPlaced()
    {
        UpgradePopup popup = FindObjectOfType<UpgradePopup>();
        if (popup != null)
        {
            _background.SetActive(true);
            _levellingTowerCutout.gameObject.SetActive(true);

            Vector3 worldPosition = popup.transform.position;
            _levellingTowerCutout.anchorMin = new(0, 0);
            _levellingTowerCutout.anchorMax = new(0, 0);

            _levellingTowerCutout.transform.position = Camera.main.WorldToScreenPoint(worldPosition);
        }

        TutorialEvents.OnTowerPlaced -= OnTowerPlaced;
        _clickToContinue.SetActive(true);
        TutorialManager.Instance.CanPlayerMove = false;
        TutorialManager.Instance.CanPlayerPickTowers = false;

        _tutorialPlayer.SetTextPosition(_upgradePlaceTransform.position);
        _tutorialPlayer.MoveToNextNarratorText();
        _actionScheduler.ScheduleAction(OnAfterTowerPlaced, () => Input.GetMouseButtonDown(0));
    }

    private void OnAfterTowerPlaced()
    {
        _background.SetActive(false);
        _levellingTowerCutout.gameObject.SetActive(false);
        _tutorialPlayer.MoveToNextNarratorText();
        _actionScheduler.ScheduleAction(OnLastText, () => Input.GetMouseButtonDown(0));
    }

    private void OnLastText()
    {
        _tutorialPlayer.SetTextLocalPosition(FindObjectOfType<CoreManager>().transform.position + TRANSFORM_POSITION_OFFSET);
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
