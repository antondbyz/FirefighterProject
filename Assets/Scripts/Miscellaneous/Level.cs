using UnityEngine;

public struct Level
{
    public const int MAX_STARS = 3;
    public int BuildIndex { get; private set; }
    public bool IsCompleted { get; private set; }
    public int StarsAmount { get; private set; }

    public Level(int buildIndex)
    {
        BuildIndex = buildIndex;
        IsCompleted = false;
        StarsAmount = 0;        
    }

    public void ChangeStarsAmount(int value)
    {
        value = Mathf.Clamp(value, 0, MAX_STARS);
        if(value > StarsAmount) StarsAmount = value;
    }

    public void Complete() => IsCompleted = true;
}