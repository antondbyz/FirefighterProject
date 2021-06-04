using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
    public static AdsManager Instance;
    public static int CurrentInterstitialCall = 0;
    private const string interstitialId = "video";
    private const string rewardedId = "rewardedVideo"; 

    [SerializeField] private int interstitialCallsSkip = 3; 

    private void Awake() 
    {
        if(Instance == null) Instance = this;
        else Debug.LogWarning("More than one instance of AdsManager!");
        
        Advertisement.AddListener(this);
        Advertisement.Initialize("4132181", false);   
    }

    private void OnDisable() 
    {
        Advertisement.RemoveListener(this);    
    }

    public void ShowRewardedAd() => Advertisement.Show(rewardedId);

    public void ShowInterstitialAd() => Advertisement.Show(interstitialId);

    public void ShowInterstitialAfterLevel(bool isRewardedShown)
    {
        if(!isRewardedShown && CurrentInterstitialCall >= interstitialCallsSkip) 
        {
            ShowInterstitialAd();
            CurrentInterstitialCall = 0;
        }
        else CurrentInterstitialCall++;
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
        if(GameController.Instance != null) GameController.Instance.IsPaused = false;
    }
}