using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

public class AdsManager : MonoBehaviour
{
    public static AdsManager Instance;

    public event Action FailedToLoadRewardedLivesAd;
    public event Action LoadedRewardedLivesAd;

    public bool IsRewardedLivesShown { get; private set; }

    private const string INTERSTITIAL_ID = "ca-app-pub-4333931459484038/3828086609";
    private const string REWARDED_LIVES_ID = "ca-app-pub-4333931459484038/1578409359";
    private const string REWARDED_ELECTRICITY_ID = "ca-app-pub-4333931459484038/2046434715";
    private InterstitialAd interstitial;
    private RewardedAd rewardedLives;
    private RewardedAd rewardedElectricity;

    private void Awake() 
    {
        if(Instance == null) Instance = this;
        else Debug.LogWarning("More than one instance of AdsManager!");
    
        MobileAds.Initialize(initStatus => { });
        CreateAndLoadInterstitialAd();
        CreateAndLoadRewardedLivesAd();
        SceneManager.sceneUnloaded += (Scene scene) => IsRewardedLivesShown = false;
    }

    public bool ShowRewardedLivesAd() 
    { 
        if(rewardedLives == null || !rewardedLives.IsLoaded()) 
        {
            CreateAndLoadRewardedLivesAd();
            return false;
        }
        rewardedLives.Show();
        return true;
    }

    public void ShowInterstitialAd()
    {
        if(interstitial == null || !interstitial.IsLoaded())
        {
            CreateAndLoadInterstitialAd();
            return;
        }
        if(!PurchaseManager.Instance.IsProductPurchased(PurchaseManager.RemoveAdsId))
        {
            interstitial.Show();
        }
    }

    private void RewardPlayerWithLives(object sender, Reward reward)
    {
        GameController.Instance.Player.LifesLeft += (int)(reward.Amount);
        IsRewardedLivesShown = true;
    }

    private void CreateAndLoadInterstitialAd()
    {
        if(interstitial != null) interstitial.Destroy();
        interstitial = new InterstitialAd(INTERSTITIAL_ID);
        interstitial.OnAdClosed += HandleInterstitialAdClosed;
        AdRequest request = new AdRequest.Builder().Build();
        interstitial.LoadAd(request);
    }

    private void CreateAndLoadRewardedLivesAd()
    {
        if(rewardedLives != null) rewardedLives.Destroy();
        rewardedLives = new RewardedAd(REWARDED_LIVES_ID);
        rewardedLives.OnUserEarnedReward += RewardPlayerWithLives;
        rewardedLives.OnAdClosed += HandleRewardedLivesAdClosed;
        rewardedLives.OnAdFailedToLoad += HandleRewardedLivesFailedToLoad;
        rewardedLives.OnAdLoaded += HandleRewardedLivesLoaded;
        AdRequest request = new AdRequest.Builder().Build();
        rewardedLives.LoadAd(request);
    }

    private void HandleInterstitialAdClosed(object sender, EventArgs args) 
    {
        CreateAndLoadInterstitialAd();
    }

    private void HandleRewardedLivesAdClosed(object sender, EventArgs args) 
    { 
        CreateAndLoadRewardedLivesAd();
        if(IsRewardedLivesShown) 
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

    private void HandleRewardedLivesFailedToLoad(object sender, AdFailedToLoadEventArgs args) => FailedToLoadRewardedLivesAd?.Invoke();

    private void HandleRewardedLivesLoaded(object sender, EventArgs e) => LoadedRewardedLivesAd?.Invoke();

    private IEnumerator InvokeFailLevel()
    {
        yield return null;
        GameController.Instance.IsPaused = true;
        GameController.Instance.FailLevel();
    }
}