using EasyButtons;
using UnityEngine;

public class TowerUpgrade : TowerBase<TowerInstanceUpgrade, TowerUpgradeScriptable>
{
    [SerializeField] private Transform _popupSpawnTransform;
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

        if (_popupInstantiated != null)
        {
            _popupInstantiated.OnUpgradePressed -= UpgradeTowers;
        }
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
                _popupInstantiated = Instantiate(_popupPrefab, _popupSpawnTransform);
                _popupInstantiated.OnUpgradePressed += UpgradeTowers;
                UpdatePopupValues();
            }
            else if (!_playerInRangeChecker.IsPlayerInRange && _popupInstantiated != null)
            {
                _popupInstantiated.OnUpgradePressed -= UpgradeTowers;
                Destroy(_popupInstantiated);
            }
        }
    }

    private void UpdatePopupValues()
    {
        string levelText = null;
        string priceText = null;

        if (Instance.Level == Stats.MaxLevel)
        {
            levelText = Instance.Level.ToString();
            priceText = "MAX";
        }
        else
        {
            levelText = Instance.Level + " -> " + (Instance.Level + 1);
            priceText = Stats.UpgradePrices[Instance.Level - 1].ToString();
        }

        _popupInstantiated.SetTexts(levelText, priceText);
    }

    [Button]
    public void UpgradeTowers()
    {
        if (!IsMaxLevel())
        {
            Instance.Level++;
            _upgradeRangeChecker.UpgradeTowers(Instance.Level);
            UpdatePopupValues();
        }
    }

    private new bool IsMaxLevel() => Instance.Level >= Stats.MaxLevel;
}
