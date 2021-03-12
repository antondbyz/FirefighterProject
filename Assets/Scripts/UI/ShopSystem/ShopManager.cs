using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : UI_Manager<ShopItem>
{
    private static List<ShopItem> purchasedItems = new List<ShopItem>();
    private static ShopItem usingItem;

    [SerializeField] private Transform itemsList = null;
    [SerializeField] private ShopItem item = null;
    [SerializeField] private TMP_Text earnedMoney = null;
    [SerializeField] private TMP_Text extinguisherInfo = null;
    [SerializeField] private TMP_Text lifesInfo = null;
    [SerializeField] private Button buyButton = null;
    [SerializeField] private GameObject useButton = null;
    [SerializeField] private GameObject usingText = null;

    public void BuySelectedItem()
    {
        if(items[selectedItem.Index].EnoughMoneyToBuy)
        {
            GameManager.PlayerBalance -= items[selectedItem.Index].Skin.Price;
            items[selectedItem.Index].CurrentState = ShopItem.State.PURCHASED;
            purchasedItems.Add(selectedItem);
            UpdateState();
            UpdateItemsAvailability();
        }
    }

    public void UseSelectedItem()
    {
        items[usingItem.Index].CurrentState = ShopItem.State.PURCHASED;
        items[selectedItem.Index].CurrentState = ShopItem.State.USING;
        usingItem = selectedItem;
        GameManager.CurrentPlayerSkin = items[usingItem.Index].Skin;
        UpdateState();
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
        if(purchasedItems.Count == 0) purchasedItems.Add(items[0]);
        for(int i = 0; i < purchasedItems.Count; i++)
        {
            items[purchasedItems[i].Index].CurrentState = ShopItem.State.PURCHASED;
        }
        if(usingItem == null) usingItem = items[0];
        items[usingItem.Index].CurrentState = ShopItem.State.USING;
        UpdateMoneyText();
        UpdateItemsAvailability();
    }

    protected override void OnEnable() 
    {
        base.OnEnable();
        GameManager.PlayerBalanceChanged += UpdateMoneyText;
        SelectItem(usingItem.Index);
    }

    protected override void OnDisable() 
    {
        base.OnDisable();
        GameManager.PlayerBalanceChanged -= UpdateMoneyText;
    }

    protected override void SelectItem(int index)
    {
        base.SelectItem(index);
        extinguisherInfo.text = items[index].Skin.ExtinguisherPower.ToString();
        lifesInfo.text = items[index].Skin.LifesAmount.ToString();
        UpdateState();
    }

    private void UpdateItemsAvailability()
    {
        int lastAvailableItem = 0;
        for(int i = 0; i < items.Length; i++)
        {
            if(items[i].CurrentState != ShopItem.State.DEFAULT) lastAvailableItem = i;
            else break;
        }
        UpdateItemsAvailability(lastAvailableItem);
        for(int i = 0; i < items.Length; i++)
        {
            Debug.Log(items[i].IsAvailable);
        }
    }

    private void UpdateMoneyText() => earnedMoney.text = GameManager.PlayerBalance.ToString();

    private void UpdateState()
    {
        buyButton.interactable = items[selectedItem.Index].EnoughMoneyToBuy;
        switch(items[selectedItem.Index].CurrentState)
        {
            case ShopItem.State.DEFAULT: 
            {
                buyButton.gameObject.SetActive(true);
                useButton.SetActive(false);
                usingText.SetActive(false);
                break;
            }
            case ShopItem.State.PURCHASED: 
            {
                useButton.SetActive(true);
                buyButton.gameObject.SetActive(false);
                usingText.SetActive(false);
                break;
            }
            case ShopItem.State.USING:
            {
                usingText.SetActive(true);
                buyButton.gameObject.SetActive(false);
                useButton.SetActive(false);
                break;
            }
        }
    }
}