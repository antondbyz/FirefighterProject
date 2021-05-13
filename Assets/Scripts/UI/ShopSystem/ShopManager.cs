using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopManager : UI_Manager<ShopItem>
{
    public static List<int> PurchasedItemsIndexes = new List<int>() { 0 };
    public static int UsingItemIndex = 0;

    [SerializeField] private TMP_Text earnedMoney = null;
    [SerializeField] private GameObject infoPanel = null;
    [SerializeField] private TMP_Text extinguisherInfo = null;
    [SerializeField] private TMP_Text lifesInfo = null;
    [SerializeField] private GameObject buyButton = null;
    [SerializeField] private GameObject useButton = null;

    public void BuySelectedItem()
    {
        if(SelectedItem.EnoughMoneyToBuy)
        {
            GameManager.PlayerBalance -= SelectedItem.Skin.Price;
            SelectedItem.CurrentState = ShopItem.State.PURCHASED;
            PurchasedItemsIndexes.Add(selectedItemIndex);
            UseSelectedItem();
            UpdateActionButton();
            UpdateItemsAvailability(PurchasedItemsIndexes[PurchasedItemsIndexes.Count - 1] + 1);
        }
    }

    public void UseSelectedItem()
    {
        items[UsingItemIndex].CurrentState = ShopItem.State.PURCHASED;
        SelectedItem.CurrentState = ShopItem.State.USING;
        UsingItemIndex = selectedItemIndex;
        UpdateActionButton();
    }

    protected override void Awake() 
    {
        base.Awake();
        for(int i = 0; i < GameManager.PlayerSkins.Length; i++)
        {
            ShopItem newItem = Instantiate(item, itemsParent);
            newItem.Initialize(i, GameManager.PlayerSkins[i]);
            items.Add(newItem);
        }
        for(int i = 0; i < PurchasedItemsIndexes.Count; i++) 
        {
            items[PurchasedItemsIndexes[i]].CurrentState = ShopItem.State.PURCHASED;
        }
        items[UsingItemIndex].CurrentState = ShopItem.State.USING;
        UpdateMoneyText();
        UpdateItemsAvailability(PurchasedItemsIndexes[PurchasedItemsIndexes.Count - 1] + 1);
    }

    protected override void OnEnable() 
    {
        base.OnEnable();
        GameManager.PlayerBalanceChanged += UpdateMoneyText;
        SelectItem(UsingItemIndex, false);
    }

    protected override void OnDisable() 
    {
        base.OnDisable();
        GameManager.PlayerBalanceChanged -= UpdateMoneyText;
    }

    protected override void SelectItem(int index, bool playSound)
    {
        base.SelectItem(index, playSound);
        infoPanel.SetActive(items[index].IsAvailable);
        extinguisherInfo.text = items[index].Skin.ExtinguisherPower.ToString();
        lifesInfo.text = items[index].Skin.LifesAmount.ToString();
        UpdateActionButton();
    }

    private void UpdateMoneyText() => earnedMoney.text = GameManager.PlayerBalance.ToString();

    private void UpdateActionButton()
    {
        switch(SelectedItem.CurrentState)
        {
            case ShopItem.State.DEFAULT: 
            {
                buyButton.SetActive(SelectedItem.IsAvailable && SelectedItem.EnoughMoneyToBuy);
                useButton.SetActive(false);
                break;
            }
            case ShopItem.State.PURCHASED: 
            {
                useButton.SetActive(true);
                buyButton.SetActive(false);
                break;
            }
            case ShopItem.State.USING:
            {
                buyButton.SetActive(false);
                useButton.SetActive(false);
                break;
            }
        }
    }
}