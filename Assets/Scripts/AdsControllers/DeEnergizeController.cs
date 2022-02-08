using UnityEngine;
using UnityEngine.UI;

public class DeEnergizeController : MonoBehaviour
{
    public static event System.Action Deenergize;

    [SerializeField] private Button showAdButton = null;
    [SerializeField] private GameObject failedToLoadAdMessage = null;
    [SerializeField] private GameObject loadingAdMessage = null;
    [SerializeField] private GameObject showPanelButton = null;

    private void OnEnable()
    {
        AdsManager.Instance.ElectricityAdClosed += OnAdClosed;
        AdsManager.Instance.ElectricityAdShowed += OnAdShowed;
    }

    private void OnDisable()
    {
        AdsManager.Instance.ElectricityAdFailedToLoad -= OnAdFailedToLoad;
        AdsManager.Instance.ElectricityAdLoaded -= OnAdLoaded;
        AdsManager.Instance.ElectricityAdClosed -= OnAdClosed;
        AdsManager.Instance.ElectricityAdShowed -= OnAdShowed;
    }

    public void DeEnergizeForAd() 
    {
        AdsManager.Instance.ElectricityAdFailedToLoad -= OnAdFailedToLoad;
        AdsManager.Instance.ElectricityAdLoaded -= OnAdLoaded;
        failedToLoadAdMessage.SetActive(false);

        bool success = AdsManager.Instance.ShowElectricityAd();
        if(!success) 
        {
            AdsManager.Instance.ElectricityAdLoaded += OnAdLoaded;
            AdsManager.Instance.ElectricityAdFailedToLoad += OnAdFailedToLoad;
            showAdButton.interactable = false;
            loadingAdMessage.SetActive(true);
            StartCoroutine(GameManager.DoAfterDelay(new WaitForSecondsRealtime(5), () => showAdButton.interactable = true));
        }
    }

#region Ads callbacks
    private void OnAdClosed() 
    { 
        GameController.Instance.IsPaused = false;
        gameObject.SetActive(false);
    }

    private void OnAdShowed()
    {
        showPanelButton.SetActive(false);
        Deenergize?.Invoke();
    }

    private void OnAdFailedToLoad()
    {
        loadingAdMessage.SetActive(false);
        failedToLoadAdMessage.SetActive(true);
        showAdButton.interactable = true;
    }

    private void OnAdLoaded()
    {
        AdsManager.Instance.ShowElectricityAd();
    }
#endregion
}