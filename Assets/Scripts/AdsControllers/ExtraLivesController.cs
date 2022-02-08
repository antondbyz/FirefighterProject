using UnityEngine;
using UnityEngine.UI;

public class ExtraLivesController : MonoBehaviour
{
    public bool IsPlayerRewarded => isPlayerRewarded;

    [SerializeField] private Button showAdButton = null;
    [SerializeField] private GameObject failedToLoadAdMessage = null;
    [SerializeField] private GameObject loadingAdMessage = null;

    private bool isPlayerRewarded;

    private void OnEnable()
    {
        AdsManager.Instance.LivesAdClosed += OnAdClosed;
        AdsManager.Instance.LivesAdShowed += OnAdShowed;
    }

    private void OnDisable()
    {
        AdsManager.Instance.LivesAdFailedToLoad -= OnAdFailedToLoad;
        AdsManager.Instance.LivesAdLoaded -= OnAdLoaded;
        AdsManager.Instance.LivesAdClosed -= OnAdClosed;
        AdsManager.Instance.LivesAdShowed -= OnAdShowed;
    }

    public void GetExtraLivesForAd() 
    {
        AdsManager.Instance.LivesAdFailedToLoad -= OnAdFailedToLoad;
        AdsManager.Instance.LivesAdLoaded -= OnAdLoaded;
        failedToLoadAdMessage.SetActive(false);

        bool success = AdsManager.Instance.ShowLivesAd();
        if(!success) 
        {
            AdsManager.Instance.LivesAdFailedToLoad += OnAdFailedToLoad;
            AdsManager.Instance.LivesAdLoaded += OnAdLoaded;
            showAdButton.interactable = false;
            loadingAdMessage.SetActive(true);
            StartCoroutine(GameManager.DoAfterDelay(new WaitForSecondsRealtime(5), () => showAdButton.interactable = true));
        }
    }

#region Ads callbacks
    private void OnAdFailedToLoad()
    {
        loadingAdMessage.SetActive(false);
        failedToLoadAdMessage.SetActive(true);
        showAdButton.interactable = true;
    }

    private void OnAdLoaded()
    {
        AdsManager.Instance.ShowLivesAd();
    }

    private void OnAdShowed()
    {
        GameController.Instance.Player.LifesLeft += 2;
        isPlayerRewarded = true;
    }

    private void OnAdClosed()
    {
        if(isPlayerRewarded)
        {
            GameController.Instance.Player.MoveToCurrentCheckpoint();
            GameController.Instance.Player.gameObject.SetActive(true);
            GameController.Instance.IsPaused = false;
        }
        else
        {
            GameController.Instance.FailLevel();
            GameController.Instance.IsPaused = true;
        }
        gameObject.SetActive(false);
    }
#endregion
}