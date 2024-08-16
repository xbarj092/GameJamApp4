using System;
using UnityEngine;

[Serializable]
public class PlayerData
{
    [SerializeField] private CurrencyData _currencyData;
    public CurrencyData CurrencyData
    {
        get => _currencyData;
        set
        {
            _currencyData = value;
            DataEvents.OnCurrencyDataChangedInvoke(_currencyData);
        }
    }

    [SerializeField] private PlayerStats _playerStats;
    public PlayerStats PlayerStats
    {
        get => _playerStats;
        set
        {
            _playerStats = value;
            DataEvents.OnPlayerStatsChangedInvoke(_playerStats);
        }
    }
}
