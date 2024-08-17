using System;
using TMPro;
using UnityEngine;

public class UpgradePopup : MonoBehaviour
{
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private TMP_Text _priceText;
    [SerializeField] private TMP_Text _valueText;

    public event Action OnUpgradePressed;

    public void SetTexts(string levelText, string priceText)
    {
        _levelText.text = levelText;
        _priceText.text = priceText;
    }

    public void PressUpgrade()
    {
        OnUpgradePressed?.Invoke();
    }
}
