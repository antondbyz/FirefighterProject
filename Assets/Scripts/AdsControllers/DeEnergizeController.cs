using UnityEngine;
using UnityEngine.UI;

public class DeEnergizeController : MonoBehaviour
{
    public static event System.Action CanDeenergize;

    [SerializeField] private GameObject deEnergizePanel = null;
    [SerializeField] private Button showAdButton = null;
    [SerializeField] private GameObject failedToLoadAdMessage = null;
    [SerializeField] private GameObject loadingAdMessage = null;
    [SerializeField] private GameObject showDeEnergizePanelButton = null;

    private void OnEnable()
    {
        AdsManager.Instance.ElectricityAdClosed += OnElectricityAdClosed;
        AdsManager.Instance.ElectricityAdShowed += OnElectricityAdShowed;
    }

    private void OnDisable()
    {
        AdsManager.Instance.ElectricityAdFailedToLoad -= OnElectricityAdFailedToLoad;
        AdsManager.Instance.ElectricityAdLoaded -= OnElectricityAdLoaded;
        AdsManager.Instance.ElectricityAdClosed -= OnElectricityAdClosed;
        AdsManager.Instance.ElectricityAdShowed -= OnElectricityAdShowed;
    }

    public void DeEnergizeForAd() 
    {
        AdsManager.Instance.ElectricityAdFailedToLoad -= OnElectricityAdFailedToLoad;
        AdsManager.Instance.ElectricityAdLoaded -= OnElectricityAdLoaded;
        failedToLoadAdMessage.SetActive(false);

        bool success = AdsManager.Instance.ShowElectricityAd();
        if(!success) 
        {
            AdsManager.Instance.ElectricityAdLoaded += OnElectricityAdLoaded;
            AdsManager.Instance.ElectricityAdFailedToLoad += OnElectricityAdFailedToLoad;
            showAdButton.interactable = false;
            loadingAdMessage.SetActive(true);
            StartCoroutine(GameManager.DoAfterDelay(new WaitForSecondsRealtime(5), () => showAdButton.interactable = true));
        }
    }

#region Ads callbacks
    private void OnElectricityAdClosed() 
    { 
        GameController.Instance.IsPaused = false;
        deEnergizePanel.SetActive(false);
    }

    private void OnElectricityAdShowed()
    {
        showDeEnergizePanelButton.SetActive(false);
        CanDeenergize?.Invoke();
    }

    private void OnElectricityAdFailedToLoad()
    {
        loadingAdMessage.SetActive(false);
        failedToLoadAdMessage.SetActive(true);
        showAdButton.interactable = true;
    }

    private void OnElectricityAdLoaded()
    {
        AdsManager.Instance.ShowElectricityAd();
    }
#endregion
}