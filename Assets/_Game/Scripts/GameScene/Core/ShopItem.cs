using UnityEngine;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ShopItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _costText;
    [field: SerializeField] public int Cost { get; private set; }
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

    public void UpdateCost(int cost) {
        Cost = cost;
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
