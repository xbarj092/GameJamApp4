using System.Collections;
using System.Collections.Generic;
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

    public void DisableFunction() {
        //_costText.text = "xxx";
        _button.interactable = false;
    }
    
    public void EnableFunction() {
        //_costText.text = "xxx";
        _button.interactable = true;
    }

    public void SetMaxLevel() {
        _costText.text = "xxx";
        _button.interactable = false;
    }

    public void UpdateCost(int cost) {
        Cost = cost;
        _costText.text = cost.ToString();
    }

}
