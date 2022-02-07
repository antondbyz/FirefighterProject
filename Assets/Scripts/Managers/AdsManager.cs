using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdsManager : MonoBehaviour
{
    public static AdsManager Instance;

    public event Action LivesAdFailedToLoad;
    public event Action LivesAdLoaded;
    public event Action LivesAdClosed;
    public event Action LivesAdShowed;
    public event Action ElectricityAdFailedToLoad;
    public event Action ElectricityAdLoaded;
    public event Action ElectricityAdClosed;
    public event Action ElectricityAdShowed;
    public event Action InterstitialFailedToLoad;
    public event Action InterstitialClosed;

    private const string INTERSTITIAL_ID = "ca-app-pub-4333931459484038/3828086609";
    private const string REWARDED_LIVES_ID = "ca-app-pub-4333931459484038/1578409359";
    private const string REWARDED_ELECTRICITY_ID = "ca-app-pub-4333931459484038/2046434715";

    private InterstitialAd interstitial;
    private RewardedAd rewardedLives;
    private RewardedAd rewardedElectricity;
    private WaitForSecondsRealtime rewardedClosedDelay = new WaitForSecondsRealtime(0.1f);
    
    private void Awake() 
    {
        if(Instance == null) Instance = this;
        else Debug.LogWarning("More than one instance of AdsManager!");
        MobileAds.Initialize(initStatus => { });
        CreateAndLoadInterstitial();
        CreateAndLoadElectricityAd();
        CreateAndLoadLivesAd();
    }

    public bool ShowLivesAd() 
    { 
        if(rewardedLives == null || !rewardedLives.IsLoaded()) 
        {
            CreateAndLoadLivesAd();
            return false;
        }
        rewardedLives.Show();
        return true;
    }

    public bool ShowElectricityAd() 
    { 
        if(rewardedElectricity == null || !rewardedElectricity.IsLoaded()) 
        {
            CreateAndLoadElectricityAd();
            return false;
        }
        rewardedElectricity.Show();
        return true;
    }

    public bool ShowInterstitialAd()
    {
        if(!PurchaseManager.Instance.IsProductPurchased(PurchaseManager.RemoveAdsId))
        {
            if(interstitial == null || !interstitial.IsLoaded())
            {
                CreateAndLoadInterstitial();
                return false;
            }
            interstitial.Show();
            return true;
        }
        return false;
    }

    private void CreateAndLoadInterstitial()
    {
        if(interstitial != null) interstitial.Destroy();
        interstitial = new InterstitialAd(INTERSTITIAL_ID);
        interstitial.OnAdClosed += OnInterstitialClosed;
        interstitial.OnAdFailedToLoad += OnInterstitialFailedToLoad;
        AdRequest request = new AdRequest.Builder().Build();
        interstitial.LoadAd(request);
    }

    private void CreateAndLoadElectricityAd()
    {
        if(rewardedElectricity != null) rewardedElectricity.Destroy();
        rewardedElectricity = new RewardedAd(REWARDED_ELECTRICITY_ID);
        rewardedElectricity.OnUserEarnedReward += OnElectricityAdShowed;
        rewardedElectricity.OnAdClosed += OnElectricityAdClosed;
        rewardedElectricity.OnAdFailedToLoad += OnElectricityAdFailedToLoad;
        rewardedElectricity.OnAdLoaded += OnElectricityAdLoaded;
        AdRequest request = new AdRequest.Builder().Build();
        rewardedElectricity.LoadAd(request);
    }

    private void CreateAndLoadLivesAd()
    {
        if(rewardedLives != null) rewardedLives.Destroy();
        rewardedLives = new RewardedAd(REWARDED_LIVES_ID);
        rewardedLives.OnUserEarnedReward += OnLivesAdShowed;
        rewardedLives.OnAdClosed += OnLivesAdClosed;
        rewardedLives.OnAdFailedToLoad += OnLivesAdFailedToLoad;
        rewardedLives.OnAdLoaded += OnLivesAdLoaded;
        AdRequest request = new AdRequest.Builder().Build();
        rewardedLives.LoadAd(request);
    }

#region Ads callbacks

    #region Lives ad
    private void OnLivesAdShowed(object sender, Reward reward)
    {
        StartCoroutine(GameManager.DoAfterDelay(instruction: null, () => LivesAdShowed?.Invoke()));
    }

    private void OnLivesAdClosed(object sender, EventArgs args) 
    { 
        StartCoroutine(GameManager.DoAfterDelay(rewardedClosedDelay, () => 
        {
            CreateAndLoadLivesAd();
            LivesAdClosed?.Invoke();
        }));
    }

    private void OnLivesAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    { 
        StartCoroutine(GameManager.DoAfterDelay(instruction: null, () => LivesAdFailedToLoad?.Invoke()));
    }

    private void OnLivesAdLoaded(object sender, EventArgs e) 
    { 
        StartCoroutine(GameManager.DoAfterDelay(instruction: null, () => LivesAdLoaded?.Invoke()));
    }
    #endregion

    #region Electricity ad
    private void OnElectricityAdShowed(object sender, Reward reward) 
    { 
        StartCoroutine(GameManager.DoAfterDelay(instruction: null, () => ElectricityAdShowed?.Invoke()));
    }

    private void OnElectricityAdClosed(object sender, EventArgs args) 
    { 
        StartCoroutine(GameManager.DoAfterDelay(rewardedClosedDelay, () => 
        {
            CreateAndLoadElectricityAd();
            ElectricityAdClosed?.Invoke();
        }));
    }

    private void OnElectricityAdFailedToLoad(object sender, AdFailedToLoadEventArgs args) 
    { 
        StartCoroutine(GameManager.DoAfterDelay(instruction: null, () => ElectricityAdFailedToLoad?.Invoke()));
    }

    private void OnElectricityAdLoaded(object sender, EventArgs e) 
    { 
        StartCoroutine(GameManager.DoAfterDelay(instruction: null, () => ElectricityAdLoaded?.Invoke()));
    }
    #endregion

    #region Interstitial ad
    private void OnInterstitialClosed(object sender, EventArgs args) 
    {
        StartCoroutine(GameManager.DoAfterDelay(instruction: null, () => 
        {
            CreateAndLoadInterstitial();
            InterstitialClosed?.Invoke();
        }));
    }

    private void OnInterstitialFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        StartCoroutine(GameManager.DoAfterDelay(instruction: null, () => InterstitialFailedToLoad?.Invoke()));
    }
    #endregion

#endregion
}