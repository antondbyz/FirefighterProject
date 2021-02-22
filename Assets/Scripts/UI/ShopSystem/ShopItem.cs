using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour, IPointerClickHandler
{
    public enum State { DEFAULT, PURCHASED, USING }

    public event System.Action<int> Clicked;
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
                    item.color = defaultColor; 
                    cost.SetActive(true);
                    break;
                }
                case State.PURCHASED:
                { 
                    item.color = purchasedColor;
                    cost.SetActive(false);
                    break;
                }
                case State.USING: 
                {
                    item.color = usingColor;
                    cost.SetActive(false);
                    break;
                }
            }
        }
    }
    public bool Selected
    {
        get => selected;
        set
        {
            selected = value;
            selectionOutline.SetActive(selected);
        }
    }
    [HideInInspector] public PlayerSkin Skin;

    [SerializeField] private GameObject selectionOutline = null;
    [SerializeField] private Color purchasedColor = new Color();
    [SerializeField] private Color usingColor = new Color();
    [SerializeField] private Image item = null;
    [SerializeField] private Image previewImage = null;
    [Header("Cost")]
    [SerializeField] private GameObject cost = null;
    [SerializeField] private TMP_Text costText = null;
    [SerializeField] private Color enoughMoneyColor = new Color(255, 255, 255, 255);
    [SerializeField] private Color notEnoughMoneyColor = new Color(255, 255, 255, 255);

    private State currentState;
    private Color defaultColor;
    private bool selected;
    private int index;

    public void UpdateCostTextColor()
    {
        costText.color = GameManager.PlayerBalance >= Skin.Cost ? enoughMoneyColor : notEnoughMoneyColor;
    }

    public void Initialize(PlayerSkin skin, int itemIndex) 
    { 
        Skin = skin;
        previewImage.sprite = Skin.SkinPreview;
        index = itemIndex;
        costText.text = $"{skin.Cost}$";
        UpdateCostTextColor();
        CurrentState = State.DEFAULT;
        Selected = false;
    }

    public void OnPointerClick(PointerEventData eventData) => Clicked?.Invoke(index);

    private void Awake() => defaultColor = item.color;

    private void OnEnable() => GameManager.PlayerBalanceChanged += UpdateCostTextColor;

    private void OnDisable() => GameManager.PlayerBalanceChanged -= UpdateCostTextColor;
}