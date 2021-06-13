using TMPro;
using UnityEngine;

public class LocalizedText : MonoBehaviour 
{
    [SerializeField] private string localizationKey = "id_";

    private void Awake() 
    {
        GetComponent<TMP_Text>().text = LocalizationManager.Instance.GetLocalizedText(localizationKey);
    } 
}