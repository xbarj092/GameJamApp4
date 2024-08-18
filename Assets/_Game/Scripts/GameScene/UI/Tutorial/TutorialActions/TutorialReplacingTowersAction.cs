using UnityEngine;

public class TutorialReplacingTowersAction : TutorialAction
{
    [SerializeField] private GameObject _clickToContinue;

    private ActionScheduler _actionScheduler;
    private PositionHighlighter _positionHighlighter;
    private Vector2 _spawnPosition;

    private const float PLACE_POSITION_THRESHOLD = 2f;

    private void Awake()
    {
        _positionHighlighter = FindObjectOfType<PositionHighlighter>();
        _actionScheduler = FindObjectOfType<ActionScheduler>();
    }

    private void OnDisable()
    {
        TutorialEvents.OnTowerPickedUp -= OnTowerPickedUp;
        TutorialEvents.OnTowerPlaced -= OnTowerPlaced;
        TutorialEvents.OnEnemyKilled -= SpawnNewEnemy;
        TutorialEvents.OnEnemyKilled -= OnEnemyKilled;
    }

    public override void StartAction()
    {
        Vector2 sceneSize;
        sceneSize.y = Camera.main.orthographicSize * 2f * 1.1f;
        sceneSize.x = sceneSize.y * Camera.main.aspect;
        sceneSize /= 2;

        Vector2 towerPosition = TutorialManager.Instance.TowerPosition.normalized;
        _spawnPosition = -towerPosition * sceneSize;
        TutorialEvents.OnEnemySpawnedInvoke(_spawnPosition);
        _tutorialPlayer.MoveToNextNarratorText();
        _clickToContinue.SetActive(true);

        TutorialEvents.OnEnemyKilled += SpawnNewEnemy;
        _actionScheduler.ScheduleAction(PickupTower, () => Input.GetMouseButtonDown(0));
    }

    private void SpawnNewEnemy()
    {
        TutorialEvents.OnEnemySpawnedInvoke(_spawnPosition);
    }

    private void PickupTower()
    {
        _tutorialPlayer.MoveToNextNarratorText();
        _clickToContinue.SetActive(false);
        TutorialManager.Instance.CanPlayerPickTowers = true;
        TutorialEvents.OnTowerPickedUp += OnTowerPickedUp;
    }

    private void OnTowerPickedUp()
    {
        TutorialEvents.OnTowerPickedUp -= OnTowerPickedUp;
        _tutorialPlayer.MoveToNextNarratorText();
        TutorialManager.Instance.PlacePosition = -TutorialManager.Instance.TowerPosition.normalized * (FindObjectOfType<SizeIncrease>().transform.localScale.x - 1);
        _positionHighlighter.HighlightPosition(TutorialManager.Instance.PlacePosition, PLACE_POSITION_THRESHOLD);
        TutorialEvents.OnTowerPlaced += OnTowerPlaced;
    }

    private void OnTowerPlaced()
    {
        if (Mathf.Abs(TutorialManager.Instance.TowerPosition.x - TutorialManager.Instance.PlacePosition.x) < PLACE_POSITION_THRESHOLD &&
            Mathf.Abs(TutorialManager.Instance.TowerPosition.y - TutorialManager.Instance.PlacePosition.y) < PLACE_POSITION_THRESHOLD)
        {
            OnTowerPlacedCorrectly();
        }
    }

    private void OnTowerPlacedCorrectly()
    {
        TutorialManager.Instance.CanPlayerPickTowers = false;
        TutorialEvents.OnEnemyKilled -= SpawnNewEnemy;
        TutorialEvents.OnTowerPlaced -= OnTowerPlaced;
        _positionHighlighter.LowlightPosition();
        _tutorialPlayer.TextFadeAway();
        TutorialEvents.OnEnemyKilled += OnEnemyKilled;
    }

    private void OnEnemyKilled()
    {
        TutorialEvents.OnEnemyKilled -= OnEnemyKilled;
        TutorialManager.Instance.CanPlayerPickTowers = true;
        OnActionFinishedInvoke();
    }

    public override void Exit()
    {
        TutorialManager.Instance.InstantiateTutorial(TutorialID.Upgrades);
    }
}
