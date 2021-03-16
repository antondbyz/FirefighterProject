using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : UI_Manager<ShopItem>
{
    private static List<int> purchasedItemsIndexes = new List<int>() { 0 };
    private static int usingItemIndex = 0;

    [SerializeField] private TMP_Text earnedMoney = null;
    [SerializeField] private GameObject infoPanel = null;
    [SerializeField] private TMP_Text extinguisherInfo = null;
    [SerializeField] private TMP_Text lifesInfo = null;
    [SerializeField] private Button buyButton = null;
    [SerializeField] private GameObject useButton = null;
    [SerializeField] private GameObject usingText = null;

    public void BuySelectedItem()
    {
        if(SelectedItem.EnoughMoneyToBuy)
        {
            GameManager.PlayerBalance -= SelectedItem.Skin.Price;
            SelectedItem.CurrentState = ShopItem.State.PURCHASED;
            purchasedItemsIndexes.Add(selectedItemIndex);
            UseSelectedItem();
            UpdateState();
            UpdateItemsAvailability(purchasedItemsIndexes[purchasedItemsIndexes.Count - 1] + 1);
        }
    }

    public void UseSelectedItem()
    {
        items[usingItemIndex].CurrentState = ShopItem.State.PURCHASED;
        SelectedItem.CurrentState = ShopItem.State.USING;
        usingItemIndex = selectedItemIndex;
        GameManager.CurrentPlayerSkin = items[usingItemIndex].Skin;
        UpdateState();
    }

    private void Awake() 
    {
        for(int i = 0; i < GameManager.PlayerSkins.Length; i++)
        {
            ShopItem newItem = Instantiate(item, itemsParent);
            newItem.Initialize(i, GameManager.PlayerSkins[i]);
            items.Add(newItem);
        }
        for(int i = 0; i < purchasedItemsIndexes.Count; i++) 
        {
            items[purchasedItemsIndexes[i]].CurrentState = ShopItem.State.PURCHASED;
        }
        items[usingItemIndex].CurrentState = ShopItem.State.USING;
        UpdateMoneyText();
        UpdateItemsAvailability(purchasedItemsIndexes[purchasedItemsIndexes.Count - 1] + 1);
    }

    protected override void OnEnable() 
    {
        base.OnEnable();
        GameManager.PlayerBalanceChanged += UpdateMoneyText;
        SelectItem(usingItemIndex);
    }

    protected override void OnDisable() 
    {
        base.OnDisable();
        GameManager.PlayerBalanceChanged -= UpdateMoneyText;
    }

    protected override void SelectItem(int index)
    {
        base.SelectItem(index);
        infoPanel.SetActive(items[index].IsAvailable);
        extinguisherInfo.text = items[index].Skin.ExtinguisherPower.ToString();
        lifesInfo.text = items[index].Skin.LifesAmount.ToString();
        UpdateState();
    }

    private void UpdateMoneyText() => earnedMoney.text = GameManager.PlayerBalance.ToString();

    private void UpdateState()
    {
        switch(SelectedItem.CurrentState)
        {
            case ShopItem.State.DEFAULT: 
            {
                buyButton.gameObject.SetActive(SelectedItem.IsAvailable && SelectedItem.EnoughMoneyToBuy);
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