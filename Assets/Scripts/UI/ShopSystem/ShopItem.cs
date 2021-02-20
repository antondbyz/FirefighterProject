using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour, IPointerClickHandler
{
    public event System.Action<int> Clicked;
    public bool Selected
    {
        get => selected;
        set
        {
            selected = value;
            image.color = selected ? selectedColor : defaultColor;
        }
    }
    [HideInInspector] public PlayerSkin Skin;

    [SerializeField] private Color selectedColor = new Color();
    [SerializeField] private Image previewImage;

    private Image image;
    private Color defaultColor;
    private bool selected;
    private int index;

    public void Initialize(PlayerSkin skin, int itemIndex, bool selected) 
    { 
        Skin = skin;
        previewImage.sprite = Skin.SkinPreview;
        index = itemIndex;
        Selected = selected;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Clicked?.Invoke(index);
    }

    private void Awake() 
    {
        image = GetComponent<Image>();
        defaultColor = image.color;    
    }
}