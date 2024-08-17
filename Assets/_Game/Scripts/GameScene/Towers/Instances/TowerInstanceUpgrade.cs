using System;

[Serializable]
public class TowerInstanceUpgrade : TowerInstanceBase
{
    public int Level;

    public TowerInstanceUpgrade(int level)
    {
        Level = level;
    }
}
