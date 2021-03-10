using UnityEngine;
using TMPro;

public class LevelItem : UI_Item
{
    public int LevelIndex { get; private set; }

    [SerializeField] private TMP_Text levelNumber = null; 

    public void Initialize(int index, int levelIndex)
    {
        this.Index = index;
        levelNumber.text = (index + 1).ToString();
        LevelIndex = levelIndex;
    }
}