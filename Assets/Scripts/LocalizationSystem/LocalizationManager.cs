using System.Collections.Generic;
using UnityEngine;

public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager Instance;
    public static int CurrentLanguageIndex;

    public event System.Action LanguageChanged;
    public Language[] Languages => languages;

    [SerializeField] private Language[] languages = null;

    private TextAsset[] locales;
    private Dictionary<string, string> localizedText = new Dictionary<string, string>();
    private const string missingLocalizationKey = "Localization key not found";

    public void LoadLanguage(int languageIndex)
    {
        string jsonData = "";
        for(int i = 0; i < locales.Length; i++) 
        {
            if(locales[i].name == languages[languageIndex].LocaleName)
            { 
                jsonData = locales[i].text;
                break;
            }
        }
        LocalizationData localizationData = JsonUtility.FromJson<LocalizationData>(jsonData);
        localizedText.Clear();
        for (int i = 0; i < localizationData.items.Length; i++)
        {
            localizedText.Add(localizationData.items[i].key, localizationData.items[i].value);
        }
        LanguageChanged?.Invoke();
        CurrentLanguageIndex = languageIndex;
    }

    public string GetLocalizedText(string key)
    {
        string result = missingLocalizationKey;
        if(localizedText.ContainsKey(key)) result = localizedText[key];
        return result;
    }

    private void Awake() 
    {
        if(Instance == null) Instance = this;
        else Debug.LogWarning("More than one instance of LocalizationManager");

        locales = Resources.LoadAll<TextAsset>("Locales");
        LoadLanguage(CurrentLanguageIndex);
    }
}