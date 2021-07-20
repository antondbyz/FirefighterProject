using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using TMPro;

public class AdsManager : MonoBehaviour
{
    public static AdsManager Instance;

    public event Action FailedToLoadRewardedAd;
    public event Action LoadedRewardedAd;

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
        if(rewarded == null || !rewarded.IsLoaded()) 
        {
            CreateAndLoadRewardedAd();
            return false;
        }
        rewarded.Show();
        return true;
    }

    public void ShowInterstitialAd()
    {
        if(interstitial == null || !interstitial.IsLoaded())
        {
            CreateAndLoadInterstitialAd();
            return;
        }
        if(!PurchaseManager.Instance.IsProductPurchased(PurchaseManager.RemoveAdsId) && !IsRewardedShown)
        {
            interstitial.Show();
        }
    }

    private void RewardPlayer(object sender, Reward reward)
    {
        GameController.Instance.Player.LifesLeft += (int)(reward.Amount);
        IsRewardedShown = true;
    }

    private void CreateAndLoadInterstitialAd()
    {
        if(interstitial != null) interstitial.Destroy();
        interstitial = new InterstitialAd(INTERSTITIAL_ID);
        interstitial.OnAdClosed += HandleInterstitialAdClosed;
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
        rewarded.OnAdLoaded += HandleRewardedLoaded;
        AdRequest request = new AdRequest.Builder().Build();
        rewarded.LoadAd(request);
    }

    private void HandleInterstitialAdClosed(object sender, EventArgs args) 
    {
        CreateAndLoadInterstitialAd();
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

    private void HandleRewardedFailedToLoad(object sender, AdFailedToLoadEventArgs args) => FailedToLoadRewardedAd?.Invoke();

    private void HandleRewardedLoaded(object sender, EventArgs e) => LoadedRewardedAd?.Invoke();

    private IEnumerator InvokeFailLevel()
    {
        yield return null;
        GameController.Instance.IsPaused = true;
        GameController.Instance.FailLevel();
    }
}