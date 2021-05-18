using System.Collections.Generic;
using UnityEngine;

public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager Instance;
    public event System.Action LocaleChanged;

    [SerializeField] private string defaultLocaleName = "Locale_";
    private TextAsset[] locales;
    private Dictionary<string, string> localizedText = new Dictionary<string, string>();
    private const string missingLocalizationKey = "Localization key not found";

    private int currentLocaleIndex = 0;

    public string GetLocalizedText(string key)
    {
        string result = missingLocalizationKey;
        if(localizedText.ContainsKey(key)) result = localizedText[key];
        return result;
    }

    public void SwitchLocale()
    {
        currentLocaleIndex++;
        if(currentLocaleIndex >= locales.Length) currentLocaleIndex = 0;
        LoadCurrentLocale();
        LocaleChanged?.Invoke();
    }

    private void Awake() 
    {
        if(Instance == null) Instance = this;
        else Debug.LogWarning("More than one instance of LocalizationManager");

        locales = Resources.LoadAll<TextAsset>("Locales");
        for(int i = 0; i < locales.Length; i++) 
        {
            if(locales[i].name == defaultLocaleName) currentLocaleIndex = i;
        }
        LoadCurrentLocale();
    }

    private void LoadCurrentLocale()
    {
        localizedText.Clear();
        string jsonData = locales[currentLocaleIndex].text;
        LocalizationData localizationData = JsonUtility.FromJson<LocalizationData>(jsonData);
        for (int i = 0; i < localizationData.items.Length; i++)
        {
            localizedText.Add(localizationData.items[i].key, localizationData.items[i].value);
        }
    }
}