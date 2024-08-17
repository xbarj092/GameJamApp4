using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour {
    [SerializeField] private List<ShopItem> _items;
    [SerializeField] private List<GameObject> _towers;
    [SerializeField] private PlayerMovement _movement;
    [SerializeField] private PlayerInteractions _interaction;

    private void Awake() {
        _items[0].UpdateCost(2);
        _items[1].UpdateCost(10);
        _items[2].UpdateCost(10);
        _items[3].UpdateCost(10);
        _items[4].UpdateCost(5);
        _items[5].UpdateCost(5);
    }

    private void OnEnable() {
        DataEvents.OnCurrencyDataChanged += UpdateItems;
    }
    
    private void OnDisable() {
        DataEvents.OnCurrencyDataChanged -= UpdateItems;
    }

    private void Start() {
        UpdateItems(new(0));
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
        int cost = _items[0].Cost;
        SpendMoney(cost);
        _items[0].UpdateCost(cost+3);

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        ITowerBase tower = Instantiate(_towers[0], mousePosition, Quaternion.identity, null).GetComponent<ITowerBase>();
        _interaction.OnTowerPickedUp(tower);

    }

    public void BuyAttackSpeedTower() {
        int cost = _items[1].Cost;
        SpendMoney(cost);
        _items[1].UpdateCost(cost+15);

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        ITowerBase tower = Instantiate(_towers[1], mousePosition, Quaternion.identity, null).GetComponent<ITowerBase>();
        _interaction.OnTowerPickedUp(tower);
    }

    public void BuyDamageTower() {
        int cost = _items[2].Cost;
        SpendMoney(cost);
        _items[2].UpdateCost(cost+15);

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        ITowerBase tower = Instantiate(_towers[2], mousePosition, Quaternion.identity, null).GetComponent<ITowerBase>();
        _interaction.OnTowerPickedUp(tower);
    }

    public void BuyRangeTower() {
        int cost = _items[3].Cost;
        SpendMoney(cost);
        _items[3].UpdateCost(cost+15);

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        ITowerBase tower = Instantiate(_towers[3], mousePosition, Quaternion.identity, null).GetComponent<ITowerBase>();
        _interaction.OnTowerPickedUp(tower);
    }

    public void BuyPlayerMovementSpeed() {
        int cost = _items[4].Cost;
        SpendMoney(cost);
        _items[4].UpdateCost(cost+20);

        _movement.AddSpeed(0.2f);
    }

    public void BuyPlayerInteractionRange() {
        int cost = _items[5].Cost;
        SpendMoney(cost);
        _items[5].UpdateCost(cost+20);

        _interaction.MaxRange += 1;
    }
}
