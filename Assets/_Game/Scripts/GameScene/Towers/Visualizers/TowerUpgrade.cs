using EasyButtons;
using UnityEngine;

public class TowerUpgrade : TowerBase<TowerInstanceUpgrade, TowerUpgradeScriptable>
{
    [SerializeField] private Transform _popupSpawnTransform;
    [SerializeField] private UpgradePopup _popup;
    [SerializeField] private PlayerInRangeChecker _playerInRangeChecker;

    [SerializeField] private UpgradeTowerActiveRange _upgradeRangeChecker;
    public UpgradeTowerActiveRange UpgradeRangeChecker => _upgradeRangeChecker;

    private void Awake()
    {
        SetStats();
    }

    private void OnEnable()
    {
        TowerManager.Instance.RegisterUpgradeTower(this);
        _popup.OnUpgradePressed += UpgradeTowers;
    }

    private void OnDisable()
    {
        TowerManager.Instance.UnregisterUpgradeTower(this);
        _popup.OnUpgradePressed -= UpgradeTowers;
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
            if (_playerInRangeChecker.IsPlayerInRange && !_popup.gameObject.activeInHierarchy)
            {
                _popup.gameObject.SetActive(true);
                UpdatePopupValues();
            }
            else if (!_playerInRangeChecker.IsPlayerInRange && _popup.gameObject.activeInHierarchy)
            {
                _popup.gameObject.SetActive(false);
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
            priceText = "<color=white>MAX</color>";
        }
        else
        {
            levelText = $"{Instance.Level} -> {Instance.Level + 1}";

            if (HasEnoughCoins())
            {
                priceText = $"<color=white>{Stats.UpgradePrices[Instance.Level - 1]}</color>";
            }
            else
            {
                priceText = $"<color=red>{Stats.UpgradePrices[Instance.Level - 1]}</color>";
            }
        }

        _popup.SetTexts(levelText, priceText);
    }

    [Button]
    public void UpgradeTowers()
    {
        if (!IsMaxLevel() && HasEnoughCoins())
        {
            CurrencyData currencyData = LocalDataStorage.Instance.PlayerData.CurrencyData;
            currencyData.Coins -= Stats.UpgradePrices[Instance.Level - 1];
            LocalDataStorage.Instance.PlayerData.CurrencyData = currencyData;
            Instance.Level++;
            _upgradeRangeChecker.UpgradeTowers(Instance.Level);
            UpdatePopupValues();
        }
    }

    private bool HasEnoughCoins() => Stats.UpgradePrices[Instance.Level - 1] <= LocalDataStorage.Instance.PlayerData.CurrencyData.Coins;

    private new bool IsMaxLevel() => Instance.Level >= Stats.MaxLevel;
}
