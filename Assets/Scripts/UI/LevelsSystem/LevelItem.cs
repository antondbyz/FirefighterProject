using UnityEngine;
using TMPro;

public class LevelItem : MonoBehaviour 
{
    [SerializeField] private TMP_Text levelNumber = null; 

    public void Initialize(int number)
    {
        levelNumber.text = number.ToString();
    }
}