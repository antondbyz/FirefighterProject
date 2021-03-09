using UnityEngine;
using TMPro;

public class LevelItem : Selectable
{
    [SerializeField] private TMP_Text levelNumber = null; 

    private int myIndex;

    public void Initialize(int myIndex)
    {
        this.myIndex = myIndex;
        levelNumber.text = (myIndex + 1).ToString();
    }
}