using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour {
    [SerializeField] private List<ShopItem> _items;
    [SerializeField] private List<GameObject> _towers;
    [SerializeField] private PlayerMovement _movement;
    [SerializeField] private PlayerInteractions _interaction;


    private void OnEnable() {
        DataEvents.OnCurrencyDataChanged += UpdateItems;
        TutorialEvents.OnShopItemsDisabled += DisableNonUpgradeShopItems;
    }
    
    private void OnDisable() {
        DataEvents.OnCurrencyDataChanged -= UpdateItems;
        TutorialEvents.OnShopItemsDisabled -= DisableNonUpgradeShopItems;
    }

    private void Start() {
        UpdateItems(new(0));
        _items[0].SetCost(1);
        _items[1].SetCost(10);
        _items[2].SetCost(10);
        _items[3].SetCost(10);
        _items[4].SetCost(5);
        _items[5].SetCost(5);
    }

    private void DisableNonUpgradeShopItems()
    {
        _items[0].DisableFunction(false);
        _items[4].DisableFunction(false);
        _items[5].DisableFunction(false);
    }

    private void UpdateItems(CurrencyData data) {
        CurrencyData currencyData = LocalDataStorage.Instance.PlayerData.CurrencyData;
        _items.ForEach((i) => { if(i.Cost > currencyData.Coins) i.DisableFunction(); else i.EnableFunction(); });
    }

    private void SpendMoney(int cost) {
        CurrencyData currencyData = LocalDataStorage.Instance.PlayerData.CurrencyData;
        currencyData.Coins -= cost;
        LocalDataStorage.Instance.PlayerData.CurrencyData = currencyData;
    }

    public void BuyShootTower() {
        if (TutorialManager.Instance.IsTutorialPlaying(TutorialID.Core))
        {
            TutorialEvents.OnTowerPurchasedInvoke();
        }

        int cost = _items[0].Cost;
        SpendMoney(cost);
        _items[0].UpdateCost(1.5f);

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        ITowerBase tower = Instantiate(_towers[0], mousePosition, Quaternion.identity, null).GetComponent<ITowerBase>();
        _interaction.OnTowerPickedUp(tower);

    }

    public void BuyAttackSpeedTower()
    {
        if (TutorialManager.Instance.IsTutorialPlaying(TutorialID.Upgrades))
        {
            TutorialEvents.OnTowerPurchasedInvoke();
        }

        int cost = _items[1].Cost;
        SpendMoney(cost);
        _items[1].UpdateCost(1);

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        ITowerBase tower = Instantiate(_towers[1], mousePosition, Quaternion.identity, null).GetComponent<ITowerBase>();
        _interaction.OnTowerPickedUp(tower);
    }

    public void BuyDamageTower()
    {
        if (TutorialManager.Instance.IsTutorialPlaying(TutorialID.Upgrades))
        {
            TutorialEvents.OnTowerPurchasedInvoke();
        }

        int cost = _items[2].Cost;
        SpendMoney(cost);
        _items[2].UpdateCost(1);

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        ITowerBase tower = Instantiate(_towers[2], mousePosition, Quaternion.identity, null).GetComponent<ITowerBase>();
        _interaction.OnTowerPickedUp(tower);
    }

    public void BuyRangeTower()
    {
        if (TutorialManager.Instance.IsTutorialPlaying(TutorialID.Upgrades))
        {
            TutorialEvents.OnTowerPurchasedInvoke();
        }

        int cost = _items[3].Cost;
        SpendMoney(cost);
        _items[3].UpdateCost(1);

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        ITowerBase tower = Instantiate(_towers[3], mousePosition, Quaternion.identity, null).GetComponent<ITowerBase>();
        _interaction.OnTowerPickedUp(tower);
    }

    public void BuyPlayerMovementSpeed() {
        int cost = _items[4].Cost;
        SpendMoney(cost);
        _items[4].UpdateCost(5);

        _movement.AddSpeed(0.66f);
    }

    public void BuyPlayerInteractionRange() {
        int cost = _items[5].Cost;
        SpendMoney(cost);
        _items[5].UpdateCost(5);

        _interaction.MaxRange += 0.66f;
    }
}
