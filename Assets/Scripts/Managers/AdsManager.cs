using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
    public static AdsManager Instance;

    private const string interstitialId = "video";
    private const string rewardedId = "rewardedVideo"; 

    private void Awake() 
    {
        if(Instance == null) Instance = this;
        else Debug.LogWarning("More than one instance of AdsManager!");
        
        Advertisement.AddListener(this);
        Advertisement.Initialize("4132181", true);   
    }

    private void OnDisable() 
    {
        Advertisement.RemoveListener(this);    
    }

    public void ShowRewardedAd()
    {
        Advertisement.Show(rewardedId);
    }

    public void ShowInterstitialAd()
    {
        Advertisement.Show(interstitialId);
    }

    public void OnUnityAdsReady(string placementId)
    {
        
    }

    public void OnUnityAdsDidError(string message)
    {
        
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if(placementId == rewardedId) GameController.Instance.TotalEarnedMoney *= 2;
    }
}