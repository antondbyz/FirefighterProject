using UnityEngine;
using UnityEngine.UI;

public class DoubleMoneyController : MonoBehaviour
{
    [SerializeField] private Button showAdButton = null;
    [SerializeField] private GameObject failedToLoadAdMessage = null;
    [SerializeField] private GameObject loadingAdMessage = null;
    [SerializeField] private GameObject showPanelButton = null;

    private void OnEnable()
    {
        AdsManager.Instance.MoneyAdClosed += OnAdClosed;
        AdsManager.Instance.MoneyAdShowed += OnAdShowed;
    }

    private void OnDisable()
    {
        AdsManager.Instance.MoneyAdFailedToLoad -= OnAdFailedToLoad;
        AdsManager.Instance.MoneyAdLoaded -= OnAdLoaded;
        AdsManager.Instance.MoneyAdClosed -= OnAdClosed;
        AdsManager.Instance.MoneyAdShowed -= OnAdShowed;
    }

    public void DoubleMoneyForAd() 
    {
        AdsManager.Instance.MoneyAdFailedToLoad -= OnAdFailedToLoad;
        AdsManager.Instance.MoneyAdLoaded -= OnAdLoaded;
        failedToLoadAdMessage.SetActive(false);

        bool success = AdsManager.Instance.ShowMoneyAd();
        if(!success) 
        {
            AdsManager.Instance.MoneyAdLoaded += OnAdLoaded;
            AdsManager.Instance.MoneyAdFailedToLoad += OnAdFailedToLoad;
            showAdButton.interactable = false;
            loadingAdMessage.SetActive(true);
            StartCoroutine(GameManager.DoAfterDelay(new WaitForSecondsRealtime(5), () => showAdButton.interactable = true));
        }
    }

#region Ads callbacks
    private void OnAdClosed() 
    { 
        gameObject.SetActive(false);
    }

    private void OnAdShowed()
    {
        GameController.Instance.Player.EarnedMoney *= 2;
        GameController.Instance.UpdateEarnedMoneyText();
        showPanelButton.SetActive(false);
    }

    private void OnAdFailedToLoad()
    {
        loadingAdMessage.SetActive(false);
        failedToLoadAdMessage.SetActive(true);
        showAdButton.interactable = true;
    }

    private void OnAdLoaded()
    {
        AdsManager.Instance.ShowMoneyAd();
    }
#endregion
}
