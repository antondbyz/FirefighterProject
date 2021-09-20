using System.Collections;
using UnityEngine;

public class ExtraLivesController : MonoBehaviour
{
    [SerializeField] private GameObject getExtraLivesPanel = null;
    [SerializeField] private GameObject showRewardedAdButton = null;
    [SerializeField] private GameObject failedToLoadAdMessage = null;
    [SerializeField] private GameObject loadingAdMessage = null;

    private void OnEnable()
    {
        AdsManager.Instance.FailedToLoadRewardedLivesAd += HandleRewardedAdFailedToLoad;
        AdsManager.Instance.LoadedRewardedLivesAd += HandleRewardedAdLoaded;
    }

    private void OnDisable()
    {
        AdsManager.Instance.FailedToLoadRewardedLivesAd -= HandleRewardedAdFailedToLoad;
        AdsManager.Instance.LoadedRewardedLivesAd -= HandleRewardedAdLoaded;
    }

    public void ShowExtraLivesPanel() => getExtraLivesPanel.SetActive(true);

    public void GetExtraLivesForAd() 
    {
        failedToLoadAdMessage.SetActive(false);
        bool success = AdsManager.Instance.ShowRewardedLivesAd();
        if(success) getExtraLivesPanel.SetActive(false);
        else 
        {
            showRewardedAdButton.SetActive(false);
            loadingAdMessage.SetActive(true);
        }
    }

    private void HandleRewardedAdFailedToLoad()
    {
        if(getExtraLivesPanel.activeSelf)
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
        if(getExtraLivesPanel.activeSelf)
        {
            StartCoroutine(DoNextFrame(() => 
            {
                AdsManager.Instance.ShowRewardedLivesAd();
                getExtraLivesPanel.SetActive(false);
            }));
        }
    }

    private IEnumerator DoNextFrame(System.Action action)
    {
        yield return null;
        action?.Invoke();
    }
}