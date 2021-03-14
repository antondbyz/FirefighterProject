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
            starsPanel.gameObject.SetActive(isAvailable);
            lockImage.SetActive(!isAvailable);
            image.color = isAvailable ? availableColor : notAvailableColor;
        }
    }

    [SerializeField] private TMP_Text levelNumber = null; 
    [SerializeField] private Transform starsPanel = null;
    [SerializeField] private GameObject lockImage = null;
    [SerializeField] private Color availableColor = Color.white;
    [SerializeField] private Color notAvailableColor = Color.white;

    private Image image;
    private Image[] stars;

    public void Initialize(int index, int levelBuildIndex, int starsAmount)
    {
        Index = index;
        levelNumber.text = (index + 1).ToString();
        LevelBuildIndex = levelBuildIndex;
        for(int i = 0; i < starsAmount; i++)
        {
            stars[i].color = Color.yellow;
        }
    }

    protected override void Awake() 
    {
        base.Awake();
        image = GetComponent<Image>();
        stars = new Image[starsPanel.childCount];
        for(int i = 0; i < stars.Length; i++)
        {
            stars[i] = starsPanel.GetChild(i).GetComponent<Image>();
        }
    }
}