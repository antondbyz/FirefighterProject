using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : Selectable
{
    public enum State { DEFAULT, PURCHASED, USING }

    public State CurrentState
    {
        get => currentState;
        set
        {
            currentState = value;
            switch(currentState)
            {
                case State.DEFAULT: 
                {
                    image.color = defaultColor; 
                    price.SetActive(true);
                    break;
                }
                case State.PURCHASED:
                { 
                    image.color = purchasedColor;
                    price.SetActive(false);
                    break;
                }
                case State.USING: 
                {
                    image.color = usingColor;
                    price.SetActive(false);
                    break;
                }
            }
        }
    }
    public int Index { get; private set; }
    public bool EnoughMoneyToBuy => GameManager.PlayerBalance >= Skin.Price;
    [HideInInspector] public PlayerSkin Skin;

    [SerializeField] private Color purchasedColor = new Color();
    [SerializeField] private Color usingColor = new Color();
    [SerializeField] private Image previewImage = null;
    [Header("Price")]
    [SerializeField] private GameObject price = null;
    [SerializeField] private TMP_Text priceText = null;
    [SerializeField] private Color enoughMoneyColor = new Color(255, 255, 255, 255);
    [SerializeField] private Color notEnoughMoneyColor = new Color(255, 255, 255, 255);

    private Image image;
    private State currentState;
    private Color defaultColor;

    public void UpdateCostTextColor() => priceText.color = EnoughMoneyToBuy ? enoughMoneyColor : notEnoughMoneyColor;

    public void Initialize(PlayerSkin skin, int itemIndex) 
    { 
        Skin = skin;
        previewImage.sprite = Skin.SkinPreview;
        Index = itemIndex;
        priceText.text = $"{skin.Price}$";
        UpdateCostTextColor();
        CurrentState = State.DEFAULT;
        Selected = false;
    }

    protected override void Awake() 
    {
        base.Awake(); 
        image = GetComponent<Image>();
        defaultColor = image.color;
    }

    private void OnEnable() => GameManager.PlayerBalanceChanged += UpdateCostTextColor;

    private void OnDisable() => GameManager.PlayerBalanceChanged -= UpdateCostTextColor;
}