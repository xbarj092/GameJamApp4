using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDScreen : GameScreen
{
    [SerializeField] private TMP_Text _coinAmount;
    [SerializeField] private TMP_Text _timeText;

    private void Awake()
    {
        UpdateCoinAmount(LocalDataStorage.Instance.PlayerData.CurrencyData);
        if (TutorialManager.Instance.TutorialCompleted)
        {
            InvokeRepeating(nameof(UpdateTimeText), 0, 1);
        }
    }

    private void OnEnable()
    {
        DataEvents.OnCurrencyDataChanged += UpdateCoinAmount;
        TutorialEvents.OnTutorialCompleted += StartCountingTime;
    }

    private void OnDisable()
    {
        DataEvents.OnCurrencyDataChanged -= UpdateCoinAmount;
        TutorialEvents.OnTutorialCompleted -= StartCountingTime;
    }

    private void UpdateCoinAmount(CurrencyData data)
    {
        _coinAmount.text = data.Coins.ToString();
    }

    private void StartCountingTime()
    {
        _timeText.enabled = true;
        InvokeRepeating(nameof(UpdateTimeText), 0, 1);
    }

    private void UpdateTimeText()
    {
        PlayerStats stats = LocalDataStorage.Instance.PlayerData.PlayerStats;
        stats.TimeAlive++;
        TimeSpan time = TimeSpan.FromSeconds(stats.TimeAlive);
        List<string> timeComponents = new();

        if (time.Hours > 0)
        {
            timeComponents.Add($"{time.Hours}h");
        }
        if (time.Minutes > 0)
        {
            timeComponents.Add($"{time.Minutes}m");
        }
        if (time.Seconds > 0 || timeComponents.Count == 0)
        {
            timeComponents.Add($"{time.Seconds}s");
        }

        _timeText.text = string.Join(" ", timeComponents);
        LocalDataStorage.Instance.PlayerData.PlayerStats = stats;
    }
}
