using EasyButtons;
using UnityEngine;

public class TowerUpgrade : TowerBase<TowerInstanceUpgrade, TowerUpgradeScriptable>
{
    [SerializeField] private UpgradePopup _popupPrefab;
    [SerializeField] private PlayerInRangeChecker _playerInRangeChecker;

    [SerializeField] private UpgradeTowerActiveRange _upgradeRangeChecker;
    public UpgradeTowerActiveRange UpgradeRangeChecker => _upgradeRangeChecker;

    private UpgradePopup _popupInstantiated;

    private void Awake()
    {
        SetStats();
    }

    private void OnEnable()
    {
        TowerManager.Instance.RegisterUpgradeTower(this);
    }

    private void OnDisable()
    {
        TowerManager.Instance.UnregisterUpgradeTower(this);
    }

    private void Update()
    {
        CheckPlayerInRange();
    }

    private void SetStats()
    {
        Instance = new(Stats.BaseLevel);
    }

    private void CheckPlayerInRange()
    {
        if (!IsMaxLevel())
        {
            if (_playerInRangeChecker.IsPlayerInRange && _popupInstantiated == null)
            {
                _popupInstantiated = Instantiate(_popupPrefab);
                _popupInstantiated.SetText(Instance.Level + " -> " + Instance.Level++);
            }
            else if (!_playerInRangeChecker.IsPlayerInRange && _popupInstantiated != null)
            {
                Destroy(_popupInstantiated);
            }
        }
    }

    [Button]
    public void UpgradeTowers()
    {
        if (!IsMaxLevel())
        {
            Instance.Level++;
            _upgradeRangeChecker.UpgradeTowers(Instance.Level);
        }
    }

    private new bool IsMaxLevel() => Instance.Level >= Stats.MaxLevel;
}
