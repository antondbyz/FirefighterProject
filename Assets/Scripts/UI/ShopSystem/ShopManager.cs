using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour 
{
    private static ShopItem[] items;
    private static List<int> purchasedItemsIndexes = new List<int>(){ 0 };
    private static int usingItemIndex = 0;

    [SerializeField] private Transform itemsList = null;
    [SerializeField] private ShopItem item = null;
    [SerializeField] private TMP_Text earnedMoney = null;
    [SerializeField] private GameObject buyButton = null;
    [SerializeField] private GameObject useButton = null;
    [SerializeField] private GameObject usingText = null;

    private int selectedItemIndex;

    public void SelectItem(int index)
    {
        selectedItemIndex = index;
        for(int i = 0; i < items.Length; i++)
        {
            items[i].Selected = (i == index);
        }
        UpdateButton();
    }

    public void BuySelectedItem()
    {
        items[selectedItemIndex].CurrentState = ShopItem.State.PURCHASED;
        purchasedItemsIndexes.Add(selectedItemIndex);
        UpdateButton();
    }

    public void UseSelectedItem()
    {
        items[usingItemIndex].CurrentState = ShopItem.State.PURCHASED;
        items[selectedItemIndex].CurrentState = ShopItem.State.USING;
        usingItemIndex = selectedItemIndex;
        GameManager.CurrentPlayerSkin = items[usingItemIndex].Skin;
        UpdateButton();
    }

    private void Awake() 
    {
        items = new ShopItem[GameManager.PlayerSkins.Length];
        for(int i = 0; i < items.Length; i++)
        {
            ShopItem newItem = Instantiate(item, itemsList);
            newItem.Initialize(GameManager.PlayerSkins[i], i);
            items[i] = newItem;
        }
        for(int i = 0; i < purchasedItemsIndexes.Count; i++)
        {
            items[purchasedItemsIndexes[i]].CurrentState = ShopItem.State.PURCHASED;
        }
        items[usingItemIndex].CurrentState = ShopItem.State.USING;
        earnedMoney.text = GameManager.PlayerBalance.ToString();
    }

    private void OnEnable() 
    {
        for(int i = 0; i < items.Length; i++)
        {
            items[i].Clicked += SelectItem;
        }
        SelectItem(usingItemIndex);
    }

    private void OnDisable() 
    {
        for(int i = 0; i < items.Length; i++)
        {
            items[i].Clicked -= SelectItem;
        }
    }

    private void UpdateButton()
    {
        switch(items[selectedItemIndex].CurrentState)
        {
            case ShopItem.State.DEFAULT: 
            {
                buyButton.SetActive(true);
                useButton.SetActive(false);
                usingText.SetActive(false);
                break;
            }
            case ShopItem.State.PURCHASED: 
            {
                useButton.SetActive(true);
                buyButton.SetActive(false);
                usingText.SetActive(false);
                break;
            }
            case ShopItem.State.USING:
            {
                usingText.SetActive(true);
                buyButton.SetActive(false);
                useButton.SetActive(false);
                break;
            }
        }
    }
}