using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour 
{
    [SerializeField] private Transform itemsList = null;
    [SerializeField] private ShopItem item = null;
    [SerializeField] private TMP_Text earnedMoney = null;

    private void Awake() 
    {
        for(int i = 0; i < PlayerSkinsManager.Skins.Length; i++)
        {
            ShopItem newItem = Instantiate(item, itemsList);
            newItem.Initialize(PlayerSkinsManager.Skins[i]);
        }
        earnedMoney.text = PlayerManager.CurrentBalance.ToString();
    }
}