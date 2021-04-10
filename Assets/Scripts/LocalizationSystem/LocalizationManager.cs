using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LocalizationManager : MonoBehaviour
{
    [SerializeField] private string localeFileName = "Locale";

    private static Dictionary<string, string> localizedText;
    private static string localesPath = Application.streamingAssetsPath + "/Locales";
    private const string missingLocalizationKey = "Localization key not found";

    public static string GetLocalizedText(string key)
    {
        string result = missingLocalizationKey;
        if(localizedText.ContainsKey(key)) result = localizedText[key];
        return result;
    }

    private void Awake() 
    {
        localizedText = new Dictionary<string, string>();
        string localePath = $"{localesPath}/{localeFileName}.json";
        if(File.Exists(localePath))
        {
            string jsonData = File.ReadAllText(localePath);
            LocalizationData localizationData = JsonUtility.FromJson<LocalizationData>(jsonData);
            for (int i = 0; i < localizationData.items.Length; i++)
            {
                localizedText.Add(localizationData.items[i].key, localizationData.items[i].value);
            }
        }
        else Debug.LogError("Locale " + localePath + " doesnt exist"); 
    }
}