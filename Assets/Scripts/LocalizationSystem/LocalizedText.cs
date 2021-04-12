using TMPro;
using UnityEngine;

public class LocalizedText : MonoBehaviour 
{
    [SerializeField] private string localizationKey = "id_";
    private TMP_Text myText;

    private void Awake() 
    {
        myText = GetComponent<TMP_Text>();
        UpdateText();
        LocalizationManager.Instance.LocaleChanged += UpdateText;
    } 

    private void OnDestroy() => LocalizationManager.Instance.LocaleChanged -= UpdateText;

    private void UpdateText() => myText.text = LocalizationManager.Instance.GetLocalizedText(localizationKey);
}