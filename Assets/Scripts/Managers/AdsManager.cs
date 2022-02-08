using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdsManager : MonoBehaviour
{
    public static AdsManager Instance;

#region Ads events
    #region Money ad
    public event Action MoneyAdFailedToLoad;
    public event Action MoneyAdLoaded;
    public event Action MoneyAdClosed;
    public event Action MoneyAdShowed;
    #endregion

    #region Lives ad
    public event Action LivesAdFailedToLoad;
    public event Action LivesAdLoaded;
    public event Action LivesAdClosed;
    public event Action LivesAdShowed;
    #endregion

    #region Electricity ad
    public event Action ElectricityAdFailedToLoad;
    public event Action ElectricityAdLoaded;
    public event Action ElectricityAdClosed;
    public event Action ElectricityAdShowed;
    #endregion

    #region Interstitial ad
    public event Action InterstitialFailedToLoad;
    public event Action InterstitialClosed;
    #endregion
#endregion

    private const string INTERSTITIAL_ID = "ca-app-pub-4333931459484038/3828086609";
    private const string REWARDED_LIVES_ID = "ca-app-pub-4333931459484038/1578409359";
    private const string REWARDED_ELECTRICITY_ID = "ca-app-pub-4333931459484038/2046434715";
    private const string REWARDED_MONEY_ID = "ca-app-pub-4333931459484038/6840725038";

    private InterstitialAd interstitial;
    private RewardedAd rewardedLives;
    private RewardedAd rewardedElectricity;
    private RewardedAd rewardedMoney;
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

#region Show ads
    public bool ShowMoneyAd()
    {
        if(rewardedMoney == null || !rewardedMoney.IsLoaded()) 
        {
            CreateAndLoadMoneyAd();
            return false;
        }
        rewardedMoney.Show();
        return true;
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
#endregion

#region Load ads
    private void CreateAndLoadMoneyAd()
    {
        if(rewardedMoney != null) rewardedMoney.Destroy();
        rewardedMoney = new RewardedAd(REWARDED_MONEY_ID);
        rewardedMoney.OnUserEarnedReward += OnMoneyAdShowed;
        rewardedMoney.OnAdClosed += OnMoneyAdClosed;
        rewardedMoney.OnAdFailedToLoad += OnMoneyAdFailedToLoad;
        rewardedMoney.OnAdLoaded += OnMoneyAdLoaded;
        AdRequest request = new AdRequest.Builder().Build();
        rewardedMoney.LoadAd(request);
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

    private void CreateAndLoadInterstitial()
    {
        if(interstitial != null) interstitial.Destroy();
        interstitial = new InterstitialAd(INTERSTITIAL_ID);
        interstitial.OnAdClosed += OnInterstitialClosed;
        interstitial.OnAdFailedToLoad += OnInterstitialFailedToLoad;
        AdRequest request = new AdRequest.Builder().Build();
        interstitial.LoadAd(request);
    }
#endregion

#region Ads callbacks
    #region Money ad
    private void OnMoneyAdShowed(object sender, Reward reward)
    {
        MoneyAdShowed?.Invoke();
    }

    private void OnMoneyAdClosed(object sender, EventArgs args) 
    { 
        StartCoroutine(GameManager.DoAfterDelay(rewardedClosedDelay, () => 
        {
            CreateAndLoadMoneyAd();
            MoneyAdClosed?.Invoke();
        }));
    }

    private void OnMoneyAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    { 
        StartCoroutine(GameManager.DoAfterDelay(instruction: null, () => MoneyAdFailedToLoad?.Invoke()));
    }

    private void OnMoneyAdLoaded(object sender, EventArgs e) 
    { 
        StartCoroutine(GameManager.DoAfterDelay(instruction: null, () => MoneyAdLoaded?.Invoke()));
    }
    #endregion

    #region Lives ad
    private void OnLivesAdShowed(object sender, Reward reward)
    {
        LivesAdShowed?.Invoke();
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
        ElectricityAdShowed?.Invoke();
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