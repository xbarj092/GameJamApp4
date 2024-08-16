using System;

public static class DataEvents
{
    public static event Action<CurrencyData> OnCurrencyDataChanged;
    public static void OnCurrencyDataChangedInvoke(CurrencyData currencyData)
    {
        OnCurrencyDataChanged?.Invoke(currencyData);
    }

    public static event Action<PlayerStats> OnPlayerStatsChanged;
    public static void OnPlayerStatsChangedInvoke(PlayerStats playerStats)
    {
        OnPlayerStatsChanged?.Invoke(playerStats);
    }
}
