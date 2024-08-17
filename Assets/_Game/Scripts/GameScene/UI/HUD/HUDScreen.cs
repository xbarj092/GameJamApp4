using TMPro;
using UnityEngine;

public class HUDScreen : MonoBehaviour
{
    [SerializeField] private TMP_Text _coinAmount;

    private void Awake()
    {
        UpdateCoinAmount(LocalDataStorage.Instance.PlayerData.CurrencyData);
    }

    private void OnEnable()
    {
        DataEvents.OnCurrencyDataChanged += UpdateCoinAmount;
    }

    private void OnDisable()
    {
        DataEvents.OnCurrencyDataChanged += UpdateCoinAmount;
    }

    private void UpdateCoinAmount(CurrencyData data)
    {
        _coinAmount.text = data.Coins.ToString();
    }
}
