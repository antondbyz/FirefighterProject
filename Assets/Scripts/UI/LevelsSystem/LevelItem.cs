using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelItem : UI_Item
{
    public int LevelBuildIndex { get; private set; }

    public override bool IsAvailable 
    { 
        get => isAvailable; 
        set 
        {
            isAvailable = value; 
            levelNumber.gameObject.SetActive(isAvailable);
            stars.SetActive(isAvailable);
            lockImage.SetActive(!isAvailable);
            image.color = isAvailable ? availableColor : notAvailableColor;
        }
    }

    [SerializeField] private TMP_Text levelNumber = null; 
    [SerializeField] private GameObject stars = null;
    [SerializeField] private GameObject lockImage = null;
    [SerializeField] private Color availableColor = Color.white;
    [SerializeField] private Color notAvailableColor = Color.white;

    private Image image;

    public void Initialize(int index, int levelBuildIndex)
    {
        Index = index;
        levelNumber.text = (index + 1).ToString();
        LevelBuildIndex = levelBuildIndex;
    }

    protected override void Awake() 
    {
        base.Awake();
        image = GetComponent<Image>();
    }
}