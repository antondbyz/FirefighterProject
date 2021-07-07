using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
    private int playerBalance;
    private Level[] levels;
    private int currentLevelIndex;
    private List<int> purchasedItemsIndexes;
    private int usingItemIndex;
    private float musicVolume;
    private float soundsVolume;
    private int currentLanguageIndex;

    public SaveData()
    {
        playerBalance = GameManager.PlayerBalance;
        levels = GameManager.Levels;
        currentLevelIndex = LevelSelectionManager.CurrentLevelIndex;
        purchasedItemsIndexes = ShopManager.PurchasedItemsIndexes;
        usingItemIndex = ShopManager.UsingItemIndex;
        musicVolume = SettingsManager.MusicVolume;
        soundsVolume = SettingsManager.SoundsVolume;
        currentLanguageIndex = LocalizationManager.CurrentLanguageIndex;
    }

    public void ResetDataExceptSettings()
    {
        playerBalance = 0;
        GameManager.Instance.InitializeLevels();
        levels = GameManager.Levels;
        currentLevelIndex = 0;
        purchasedItemsIndexes = new List<int>() { 0 };
        usingItemIndex = 0;
    }

    public void LoadDataToTheGame()
    {
        GameManager.PlayerBalance = playerBalance;
        GameManager.Levels = levels;
        LevelSelectionManager.CurrentLevelIndex = currentLevelIndex;
        ShopManager.PurchasedItemsIndexes = purchasedItemsIndexes;
        ShopManager.UsingItemIndex = usingItemIndex;
        SettingsManager.MusicVolume = musicVolume;
        SettingsManager.SoundsVolume = soundsVolume;
        LocalizationManager.CurrentLanguageIndex = currentLanguageIndex;
    }
}