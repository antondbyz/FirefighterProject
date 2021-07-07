using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

public class AdsManager : MonoBehaviour
{
    public static AdsManager Instance;

    public bool IsRewardedShown { get; private set; }

    private const string INTERSTITIAL_ID = "ca-app-pub-4333931459484038/3828086609";
    private const string REWARDED_ID = "ca-app-pub-4333931459484038/1578409359";
    private InterstitialAd interstitial;
    private RewardedAd rewarded;

    private void Awake() 
    {
        if(Instance == null) Instance = this;
        else Debug.LogWarning("More than one instance of AdsManager!");
    
        MobileAds.Initialize(initStatus => { });
        CreateAndLoadInterstitialAd();
        CreateAndLoadRewardedAd();
        SceneManager.sceneUnloaded += (Scene scene) => IsRewardedShown = false;
    }

    public bool ShowRewardedAd() 
    { 
        if(rewarded == null) return false;
        bool isAdLoaded = rewarded.IsLoaded();
        if(isAdLoaded) rewarded.Show();
        return isAdLoaded;
    }

    public void ShowInterstitialAd()
    {
        if(PurchaseManager.Instance.HasPurchasedProduct(PurchaseManager.RemoveAdsId) || interstitial == null) return;
        if(!IsRewardedShown) 
        {
            if(interstitial.IsLoaded()) interstitial.Show();
        }
    }

    private void RewardPlayer(object sender, Reward reward)
    {
        GameController.Instance.Player.LifesLeft += (int)(reward.Amount);
        IsRewardedShown = true;
    }

    private void CreateAndLoadInterstitialAd()
    {
        if(PurchaseManager.Instance.HasPurchasedProduct(PurchaseManager.RemoveAdsId)) return;
        if(interstitial != null) interstitial.Destroy();
        interstitial = new InterstitialAd(INTERSTITIAL_ID);
        interstitial.OnAdClosed += HandleInterstitialAdClosed;
        interstitial.OnAdFailedToLoad += HandleInterstitialFailedToLoad;
        AdRequest request = new AdRequest.Builder().Build();
        interstitial.LoadAd(request);
    }

    private void CreateAndLoadRewardedAd()
    {
        if(rewarded != null) rewarded.Destroy();
        rewarded = new RewardedAd(REWARDED_ID);
        rewarded.OnUserEarnedReward += RewardPlayer;
        rewarded.OnAdClosed += HandleRewardedAdClosed;
        rewarded.OnAdFailedToLoad += HandleRewardedFailedToLoad;
        AdRequest request = new AdRequest.Builder().Build();
        rewarded.LoadAd(request);
    }

    private void HandleRewardedAdClosed(object sender, EventArgs args) 
    { 
        CreateAndLoadRewardedAd();
        if(IsRewardedShown) 
        {
            GameController.Instance.Player.MoveToCurrentCheckpoint();
            GameController.Instance.Player.gameObject.SetActive(true);
            GameController.Instance.IsPaused = false;
        }
        else
        {
            StartCoroutine(InvokeFailLevel());
        }
    }

    private void HandleInterstitialAdClosed(object sender, EventArgs args) 
    {
        CreateAndLoadInterstitialAd();
    }

    private void HandleRewardedFailedToLoad(object sender, AdFailedToLoadEventArgs e)
    {
        CreateAndLoadRewardedAd();
    }

    private void HandleInterstitialFailedToLoad(object sender, AdFailedToLoadEventArgs e)
    {
        CreateAndLoadInterstitialAd();
    }

    private IEnumerator InvokeFailLevel()
    {
        yield return null;
        GameController.Instance.IsPaused = true;
        GameController.Instance.FailLevel();
    }
}