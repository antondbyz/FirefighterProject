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
                case State.DEFAULT: item.color = defaultColor; break;
                case State.PURCHASED: item.color = purchasedColor; break;
                case State.USING: item.color = usingColor; break;
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

    private State currentState = State.DEFAULT;
    private Color defaultColor;
    private bool selected;
    private int index;

    public void Initialize(PlayerSkin skin, int itemIndex) 
    { 
        Skin = skin;
        previewImage.sprite = Skin.SkinPreview;
        index = itemIndex;
        Selected = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Clicked?.Invoke(index);
    }

    private void Awake() => defaultColor = item.color;
}