using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private Text balanceText = null;
    [SerializeField] private Image playerPreviewImage = null;
    [SerializeField] private Text playerLevelText = null;
    [SerializeField] private Button playerUpgradeButton = null;

    private void OnEnable() 
    {
        UpdateUI();    
    }

    public void UpgradePlayer()
    {
        PlayerManager.Instance.Upgrade();
        UpdateUI();
    }

    public void UpdateUI()
    {
        balanceText.text = $"{PlayerManager.Instance.CurrentBalance}$";
        playerPreviewImage.sprite = PlayerManager.Instance.CurrentLevel.Preview;
        playerLevelText.text = $"Level {PlayerManager.Instance.CurrentLevelIndex}";
        playerUpgradeButton.interactable = PlayerManager.Instance.CanUpgrade;
        playerUpgradeButton.gameObject.SetActive(!PlayerManager.Instance.IsMaxLevel);
    }
}
