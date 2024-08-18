using System;

[Serializable]
public class PlayerStats
{
    public int TimeAlive;

    public PlayerStats(int timeAlive)
    {
        TimeAlive = timeAlive;
    }
}
