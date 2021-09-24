using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

public class AdsManager : MonoBehaviour
{
    public static AdsManager Instance;

    public event Action FailedToLoadRewardedLivesAd;
    public event Action FailedToLoadRewardedElectricityAd;
    public event Action LoadedRewardedLivesAd;
    public event Action LoadedRewardedElectricityAd;
    public event Action ElectricityDisabled;

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
        CreateAndLoadInterstitial();
        CreateAndLoadRewarded(rewardedLives, REWARDED_LIVES_ID, HandleRewardedLivesShown, HandleRewardedLivesClosed, HandleRewardedLivesFailedToLoad, 
            HandleRewardedLivesLoaded);
        CreateAndLoadRewarded(rewardedElectricity, REWARDED_ELECTRICITY_ID, HandleRewardedElectricityShown, HandleRewardedElectricityClosed, 
            HandleRewardedElectricityFailedToLoad, HandleRewardedElectricityLoaded);
        SceneManager.sceneUnloaded += (Scene scene) =>
        { 
            IsRewardedLivesShown = false;
        };
    }

    public bool ShowRewardedLivesAd() 
    { 
        if(rewardedLives == null || !rewardedLives.IsLoaded()) 
        {
            CreateAndLoadRewarded(rewardedLives, REWARDED_LIVES_ID, HandleRewardedLivesShown, HandleRewardedLivesClosed, HandleRewardedLivesFailedToLoad, 
                HandleRewardedLivesLoaded);
            return false;
        }
        rewardedLives.Show();
        return true;
    }

    public bool ShowRewardedElectricityAd() 
    { 
        if(rewardedElectricity == null || !rewardedElectricity.IsLoaded()) 
        {
            CreateAndLoadRewarded(rewardedElectricity, REWARDED_ELECTRICITY_ID, HandleRewardedElectricityShown, HandleRewardedElectricityClosed, 
            HandleRewardedElectricityFailedToLoad, HandleRewardedElectricityLoaded);
            return false;
        }
        rewardedElectricity.Show();
        return true;
    }

    public void ShowInterstitialAd()
    {
        if(interstitial == null || !interstitial.IsLoaded())
        {
            CreateAndLoadInterstitial();
            return;
        }
        if(!PurchaseManager.Instance.IsProductPurchased(PurchaseManager.RemoveAdsId))
        {
            interstitial.Show();
        }
    }

    private void CreateAndLoadInterstitial()
    {
        if(interstitial != null) interstitial.Destroy();
        interstitial = new InterstitialAd(INTERSTITIAL_ID);
        interstitial.OnAdClosed += HandleInterstitialClosed;
        AdRequest request = new AdRequest.Builder().Build();
        interstitial.LoadAd(request);
    }

    private void CreateAndLoadRewarded(RewardedAd ad, string id, EventHandler<Reward> reward, EventHandler<EventArgs> closed, 
        EventHandler<AdFailedToLoadEventArgs> failedToLoad, EventHandler<EventArgs> loaded)
    {
        if(ad != null) ad.Destroy();
        ad = new RewardedAd(id);
        ad.OnUserEarnedReward += reward;
        ad.OnAdClosed += closed;
        ad.OnAdFailedToLoad += failedToLoad;
        ad.OnAdLoaded += loaded;
        AdRequest request = new AdRequest.Builder().Build();
        ad.LoadAd(request);
    }

    private void HandleInterstitialClosed(object sender, EventArgs args) 
    {
        CreateAndLoadInterstitial();
    }

    private void HandleRewardedLivesShown(object sender, Reward reward)
    {
        GameController.Instance.Player.LifesLeft += (int)(reward.Amount);
        IsRewardedLivesShown = true;
    }

    private void HandleRewardedElectricityShown(object sender, Reward reward) 
    { 
        ElectricityDisabled?.Invoke();
    }

    private void HandleRewardedLivesClosed(object sender, EventArgs args) 
    { 
        CreateAndLoadRewarded(rewardedLives, REWARDED_LIVES_ID, HandleRewardedLivesShown, HandleRewardedLivesClosed, HandleRewardedLivesFailedToLoad, 
            HandleRewardedLivesLoaded);
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

    private void HandleRewardedElectricityClosed(object sender, EventArgs args) 
    { 
        CreateAndLoadRewarded(rewardedElectricity, REWARDED_ELECTRICITY_ID, HandleRewardedElectricityShown, HandleRewardedElectricityClosed, 
            HandleRewardedElectricityFailedToLoad, HandleRewardedElectricityLoaded);
        GameController.Instance.IsPaused = false;
    }

    private void HandleRewardedLivesFailedToLoad(object sender, AdFailedToLoadEventArgs args) => FailedToLoadRewardedLivesAd?.Invoke();

    private void HandleRewardedElectricityFailedToLoad(object sender, AdFailedToLoadEventArgs args) => FailedToLoadRewardedElectricityAd?.Invoke();

    private void HandleRewardedLivesLoaded(object sender, EventArgs e) => LoadedRewardedLivesAd?.Invoke();

    private void HandleRewardedElectricityLoaded(object sender, EventArgs e) => LoadedRewardedElectricityAd?.Invoke();

    private IEnumerator InvokeFailLevel()
    {
        yield return null;
        GameController.Instance.IsPaused = true;
        GameController.Instance.FailLevel();
    }
}