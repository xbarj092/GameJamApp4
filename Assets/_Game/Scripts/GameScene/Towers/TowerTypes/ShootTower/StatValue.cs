using System;

[Serializable]
public struct StatValue
{
    public int Level;
    public float Value;

    public StatValue(int level, float value)
    {
        Level = level;
        Value = value;
    }
}
