using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Dropdown localesDropdown = null;
    [SerializeField] private GameObject removeAdsButton = null; 

    private void Awake() 
    {
        List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();
        for(int i = 0; i < LocalizationManager.Instance.Languages.Length; i++)
        {
            string text = LocalizationManager.Instance.Languages[i].LocaleName;
            Sprite image = LocalizationManager.Instance.Languages[i].LanguageFlag;
            options.Add(new Dropdown.OptionData(text, image));
        }
        localesDropdown.AddOptions(options);
        localesDropdown.value = LocalizationManager.CurrentLanguageIndex;
        localesDropdown.onValueChanged.AddListener(ChangeLanguage);
    }

    private void OnEnable() 
    {
        if(!PurchaseManager.Instance.IsProductPurchased(PurchaseManager.RemoveAdsId)) 
        {
            removeAdsButton.SetActive(true);
            PurchaseManager.Instance.RemoveAdsPurchaseCompleted += HandleAdsRemoved;
        }
        else removeAdsButton.SetActive(false);
    }

    private void OnDisable() => PurchaseManager.Instance.RemoveAdsPurchaseCompleted -= HandleAdsRemoved;

    public void NewGame()
    {
        SaveManager.ResetGame();
        SceneLoader.ReplaceCurrentScene(gameObject.scene.buildIndex);
    }

    public void RemoveAds() => PurchaseManager.Instance.PurchaseProduct(PurchaseManager.RemoveAdsId);

    private void ChangeLanguage(int index) => LocalizationManager.Instance.LoadLanguage(index);

    private void HandleAdsRemoved() => removeAdsButton.SetActive(false);
}