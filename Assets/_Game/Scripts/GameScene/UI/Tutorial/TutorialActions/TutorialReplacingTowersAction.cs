using UnityEngine;

public class TutorialReplacingTowersAction : TutorialAction
{
    [SerializeField] private GameObject _clickToContinue;

    private ActionScheduler _actionScheduler;
    private Vector2 _spawnPosition;

    private void Awake()
    {
        _actionScheduler = FindObjectOfType<ActionScheduler>();
    }

    private void OnDisable()
    {
        TutorialEvents.OnTowerPickedUp -= OnTowerPickedUp;
        TutorialEvents.OnTowerPlaced -= OnTowerPlaced;
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
        TutorialEvents.OnTowerPickedUp += OnTowerPickedUp;
    }

    private void OnTowerPickedUp()
    {
        TutorialEvents.OnTowerPickedUp -= OnTowerPickedUp;
        _tutorialPlayer.MoveToNextNarratorText();
        TutorialManager.Instance.PlacePosition = -TutorialManager.Instance.TowerPosition.normalized * FindObjectOfType<SizeIncrease>().transform.localScale.x;
        TutorialEvents.OnTowerPlaced += OnTowerPlaced;
    }

    private void OnTowerPlaced()
    {
        if (Mathf.Abs(TutorialManager.Instance.TowerPosition.x - TutorialManager.Instance.PlacePosition.x) < 2 &&
            Mathf.Abs(TutorialManager.Instance.TowerPosition.y - TutorialManager.Instance.PlacePosition.y) < 2)
        {
            OnTowerPlacedCorrectly();
        }
    }

    private void OnTowerPlacedCorrectly()
    {
        TutorialEvents.OnEnemyKilled -= SpawnNewEnemy;
        TutorialEvents.OnTowerPlaced -= OnTowerPlaced;
        _tutorialPlayer.TextFadeAway();
        TutorialEvents.OnEnemyKilled += OnEnemyKilled;
    }

    private void OnEnemyKilled()
    {
        TutorialEvents.OnEnemyKilled -= OnEnemyKilled;
        OnActionFinishedInvoke();
    }

    public override void Exit()
    {
        TutorialManager.Instance.InstantiateTutorial(TutorialID.Upgrades);
    }
}
