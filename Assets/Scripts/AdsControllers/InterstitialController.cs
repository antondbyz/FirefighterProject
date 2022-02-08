using UnityEngine;

public class InterstitialController : MonoBehaviour
{
    public void CloseLevelWithAd()
    {
        bool success = AdsManager.Instance.ShowInterstitialAd();
        if(success)
            AdsManager.Instance.InterstitialClosed += GameController.Instance.CloseLevel;
        else 
            GameController.Instance.CloseLevel();
    }

    private void OnDisable()
    {
        AdsManager.Instance.InterstitialClosed -= GameController.Instance.CloseLevel;
    }
}
