using TMPro;
using UnityEngine;

public class LocalizedText : MonoBehaviour 
{
    [SerializeField] private string localizationKey = "id_";

    private TMP_Text myText;

    private void Awake() 
    {
        myText = GetComponent<TMP_Text>();
    } 

    private void OnEnable() 
    {
        UpdateText(); 
        LocalizationManager.Instance.LanguageChanged += UpdateText;
    }

    private void OnDisable() => LocalizationManager.Instance.LanguageChanged -= UpdateText;

    private void UpdateText() => myText.text = LocalizationManager.Instance.GetLocalizedText(localizationKey);
}