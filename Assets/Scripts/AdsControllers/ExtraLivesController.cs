using UnityEngine;
using UnityEngine.UI;

public class ExtraLivesController : MonoBehaviour
{
    public bool IsPlayerRewarded => isPlayerRewarded;

    [SerializeField] private GameObject getExtraLivesPanel = null;
    [SerializeField] private Button showAdButton = null;
    [SerializeField] private GameObject failedToLoadAdMessage = null;
    [SerializeField] private GameObject loadingAdMessage = null;

    private bool isPlayerRewarded;

    private void OnEnable()
    {
        AdsManager.Instance.LivesAdClosed += OnLivesAdClosed;
        AdsManager.Instance.LivesAdShowed += OnLivesAdShowed;
    }

    private void OnDisable()
    {
        AdsManager.Instance.LivesAdFailedToLoad -= OnLivesAdFailedToLoad;
        AdsManager.Instance.LivesAdLoaded -= OnLivesAdLoaded;
        AdsManager.Instance.LivesAdClosed -= OnLivesAdClosed;
        AdsManager.Instance.LivesAdShowed -= OnLivesAdShowed;
    }

    public void ShowExtraLivesPanel() => getExtraLivesPanel.SetActive(true);

    public void GetExtraLivesForAd() 
    {
        AdsManager.Instance.LivesAdFailedToLoad -= OnLivesAdFailedToLoad;
        AdsManager.Instance.LivesAdLoaded -= OnLivesAdLoaded;
        failedToLoadAdMessage.SetActive(false);

        bool success = AdsManager.Instance.ShowLivesAd();
        if(!success) 
        {
            AdsManager.Instance.LivesAdFailedToLoad += OnLivesAdFailedToLoad;
            AdsManager.Instance.LivesAdLoaded += OnLivesAdLoaded;
            showAdButton.interactable = false;
            loadingAdMessage.SetActive(true);
            StartCoroutine(GameManager.DoAfterDelay(new WaitForSecondsRealtime(5), () => showAdButton.interactable = true));
        }
    }

#region Ads callbacks
    private void OnLivesAdFailedToLoad()
    {
        loadingAdMessage.SetActive(false);
        failedToLoadAdMessage.SetActive(true);
        showAdButton.interactable = true;
    }

    private void OnLivesAdLoaded()
    {
        AdsManager.Instance.ShowLivesAd();
    }

    private void OnLivesAdShowed()
    {
        GameController.Instance.Player.LifesLeft += 2;
        isPlayerRewarded = true;
    }

    private void OnLivesAdClosed()
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
        getExtraLivesPanel.SetActive(false);
    }
#endregion
}