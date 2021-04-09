using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
    private int PlayerBalance;
    private Level[] Levels;
    private int CurrentLevelIndex;
    private List<int> PurchasedItemsIndexes;
    private int UsingItemIndex;
    private float MusicVolume;
    private float SoundsVolume;

    public SaveData()
    {
        PlayerBalance = GameManager.PlayerBalance;
        Levels = GameManager.Levels;
        CurrentLevelIndex = LevelSelectionManager.CurrentLevelIndex;
        PurchasedItemsIndexes = ShopManager.PurchasedItemsIndexes;
        UsingItemIndex = ShopManager.UsingItemIndex;
        MusicVolume = SettingsManager.MusicVolume;
        SoundsVolume = SettingsManager.SoundsVolume;
    }

    public void ResetDataExceptSettings()
    {
        PlayerBalance = 0;
        GameManager.Instance.InitializeLevels();
        Levels = GameManager.Levels;
        CurrentLevelIndex = 0;
        PurchasedItemsIndexes = new List<int>() { 0 };
        UsingItemIndex = 0;
    }

    public void LoadGameData()
    {
        GameManager.PlayerBalance = PlayerBalance;
        GameManager.Levels = Levels;
        LevelSelectionManager.CurrentLevelIndex = CurrentLevelIndex;
        ShopManager.PurchasedItemsIndexes = PurchasedItemsIndexes;
        ShopManager.UsingItemIndex = UsingItemIndex;
        SettingsManager.MusicVolume = MusicVolume;
        SettingsManager.SoundsVolume = SoundsVolume;
    }
}