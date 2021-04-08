using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
    private int PlayerBalance;
    private Level[] Levels;
    private int CurrentLevelIndex;
    private List<int> PurchasedItemsIndexes;
    private int UsingItemIndex;

    public SaveData()
    {
        PlayerBalance = GameManager.PlayerBalance;
        Levels = GameManager.Levels;
        CurrentLevelIndex = LevelSelectionManager.CurrentLevelIndex;
        PurchasedItemsIndexes = ShopManager.PurchasedItemsIndexes;
        UsingItemIndex = ShopManager.UsingItemIndex;
    }

    public void LoadSavedData()
    {
        GameManager.PlayerBalance = PlayerBalance;
        GameManager.Levels = Levels;
        LevelSelectionManager.CurrentLevelIndex = CurrentLevelIndex;
        ShopManager.PurchasedItemsIndexes = PurchasedItemsIndexes;
        ShopManager.UsingItemIndex = UsingItemIndex;
    }
}