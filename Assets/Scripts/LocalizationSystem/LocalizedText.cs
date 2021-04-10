using TMPro;
using UnityEngine;

public class LocalizedText : MonoBehaviour 
{
    [SerializeField] private string localizationKey = "id_";
    private TMP_Text myText;

    private void Awake() 
    {
        myText = GetComponent<TMP_Text>();
        myText.text = LocalizationManager.GetLocalizedText(localizationKey);
    }    
}