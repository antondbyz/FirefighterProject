using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour 
{
    public static int SelectedItemIndex;

    [SerializeField] private Transform itemsList = null;
    [SerializeField] private ShopItem item = null;
    [SerializeField] private TMP_Text earnedMoney = null;

    private ShopItem[] items = null;

    public void SelectItem(int index)
    {
        GameManager.CurrentPlayerSkin = items[index].Skin;
        for(int i = 0; i < items.Length; i++)
        {
            items[i].Selected = (i == index);
        }
        SelectedItemIndex = index;
    }

    private void Awake() 
    {
        if(items == null) items = new ShopItem[GameManager.PlayerSkins.Length];
        for(int i = 0; i < GameManager.PlayerSkins.Length; i++)
        {
            ShopItem newItem = Instantiate(item, itemsList);
            newItem.Initialize(GameManager.PlayerSkins[i], i, i == SelectedItemIndex);
            items[i] = newItem;
        }
        earnedMoney.text = GameManager.PlayerBalance.ToString();
    }

    private void OnEnable() 
    {
        for(int i = 0; i < items.Length; i++)
        {
            items[i].Clicked += SelectItem;
        }
    }

    private void OnDisable() 
    {
        for(int i = 0; i < items.Length; i++)
        {
            items[i].Clicked -= SelectItem;
        }
    }
}