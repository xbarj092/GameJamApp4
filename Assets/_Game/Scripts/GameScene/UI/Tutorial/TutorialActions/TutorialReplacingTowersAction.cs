using UnityEngine;

public class TutorialReplacingTowersAction : TutorialAction
{
    [SerializeField] private GameObject _clickToContinue;

    private ActionScheduler _actionScheduler;

    private void Awake()
    {
        _actionScheduler = FindObjectOfType<ActionScheduler>();
    }

    private void OnDisable()
    {
        TutorialEvents.OnTowerPickedUp -= OnTowerPickedUp;
        TutorialEvents.OnPlayerCorrectPosition -= OnPlayerCorrectPosition;
        TutorialEvents.OnTowerPlaced -= OnTowerPlaced;
    }

    public override void StartAction()
    {
        Vector2 sceneSize;
        sceneSize.y = Camera.main.orthographicSize * 2f * 1.1f;
        sceneSize.x = sceneSize.y * Camera.main.aspect;
        sceneSize /= 2;

        Vector2 towerPosition = TutorialManager.Instance.TowerPosition.normalized;
        Vector2 spawnPosition = -towerPosition * sceneSize;
        TutorialEvents.OnEnemySpawnedInvoke(spawnPosition);
        _tutorialPlayer.MoveToNextNarratorText();
        _clickToContinue.SetActive(true);
        _actionScheduler.ScheduleAction(PickupTower, () => Input.GetMouseButtonDown(0));
    }

    private void PickupTower()
    {
        _tutorialPlayer.MoveToNextNarratorText();
        _clickToContinue.SetActive(false);
        TutorialEvents.OnTowerPickedUp += OnTowerPickedUp;
    }

    private void OnTowerPickedUp()
    {
        TutorialEvents.OnTowerPickedUp -= OnTowerPickedUp;
        _tutorialPlayer.MoveToNextNarratorText();
        Vector2 position = -TutorialManager.Instance.TowerPosition.normalized * FindObjectOfType<SizeIncrease>().transform.localScale.x;
        TutorialEvents.OnPlayerCorrectPosition += OnPlayerCorrectPosition;
    }

    private void OnPlayerCorrectPosition()
    {
        TutorialEvents.OnPlayerCorrectPosition -= OnPlayerCorrectPosition;
        _tutorialPlayer.MoveToNextNarratorText();
        TutorialEvents.OnTowerPlaced += OnTowerPlaced;
    }

    private void OnTowerPlaced()
    {
        TutorialEvents.OnTowerPlaced -= OnTowerPlaced;
        _tutorialPlayer.MoveToNextNarratorText();
        OnActionFinishedInvoke();
    }

    public override void Exit()
    {
        TutorialManager.Instance.InstantiateTutorial(TutorialID.Upgrades);
    }
}
