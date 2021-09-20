using System.Collections;
using UnityEngine;

public class DeEnergizeController : MonoBehaviour
{
    [SerializeField] private GameObject deEnergizePanel = null;
    [SerializeField] private GameObject showRewardedAdButton = null;
    [SerializeField] private GameObject failedToLoadAdMessage = null;
    [SerializeField] private GameObject loadingAdMessage = null;
    [SerializeField] private GameObject showDeEnergizePanelButton = null;

    private void OnEnable()
    {
        AdsManager.Instance.FailedToLoadRewardedElectricityAd += HandleRewardedAdFailedToLoad;
        AdsManager.Instance.LoadedRewardedElectricityAd += HandleRewardedAdLoaded;
        AdsManager.Instance.ElectricityDisabled += HideDeEnergizeButton;
    }

    private void OnDisable()
    {
        AdsManager.Instance.FailedToLoadRewardedElectricityAd -= HandleRewardedAdFailedToLoad;
        AdsManager.Instance.LoadedRewardedElectricityAd -= HandleRewardedAdLoaded;
        AdsManager.Instance.ElectricityDisabled -= HideDeEnergizeButton;
    }

    public void DeEnergizeForAd() 
    {
        failedToLoadAdMessage.SetActive(false);
        bool success = AdsManager.Instance.ShowRewardedElectricityAd();
        if(success) deEnergizePanel.SetActive(false);
        else 
        {
            showRewardedAdButton.SetActive(false);
            loadingAdMessage.SetActive(true);
        }
    }

    private void HideDeEnergizeButton() => showDeEnergizePanelButton.SetActive(false);

    private void HandleRewardedAdFailedToLoad()
    {
        if(deEnergizePanel.activeSelf)
        {
            StartCoroutine(DoNextFrame(() => 
            {
                failedToLoadAdMessage.SetActive(true);
                loadingAdMessage.SetActive(false);
                showRewardedAdButton.SetActive(true);
            }));
        }
    }

    private void HandleRewardedAdLoaded()
    {
        if(deEnergizePanel.activeSelf)
        {
            StartCoroutine(DoNextFrame(() => 
            {
                AdsManager.Instance.ShowRewardedElectricityAd();
                deEnergizePanel.SetActive(false);
            }));
        }
    }

    private IEnumerator DoNextFrame(System.Action action)
    {
        yield return null;
        action?.Invoke();
    }
}