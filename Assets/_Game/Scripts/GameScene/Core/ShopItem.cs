using UnityEngine;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ShopItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _costText;
    public int Cost { get { return Mathf.FloorToInt(_cost); }}
    [field: SerializeField] public float _cost;
    [SerializeField] private Button _button;

    private void Awake() {
        _button = GetComponent<Button>();
    }



    public void DisableFunction(bool disableText = true) {
        if (disableText)
        {
            _costText.text = $"<color=red>{Cost}</color>";
        }

        _button.interactable = false;
    }
    
    public void EnableFunction() {
        _costText.text = $"<color=white>{Cost}</color>";
       _button.interactable = true;
    }

    public void SetMaxLevel()
    {
        _costText.text = "<color=red>MAX</color>";
        _button.interactable = false;
    }

    public void SetCost(float cost) {
        _cost = cost;
        if(Cost > LocalDataStorage.Instance.PlayerData.CurrencyData.Coins) {
            DisableFunction();
        } else {
            EnableFunction();
        }
    }

    public void UpdateCost(float cost) {
        _cost += cost;
        if (Cost > LocalDataStorage.Instance.PlayerData.CurrencyData.Coins)
        {
            DisableFunction();
        }
        else
        {
            EnableFunction();
        }
    }
}
