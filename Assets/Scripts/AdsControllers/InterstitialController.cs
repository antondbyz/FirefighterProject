using UnityEngine;

public class InterstitialController : MonoBehaviour
{
    public void CloseLevelWithAd()
    {
        bool success = AdsManager.Instance.ShowInterstitialAd();
        if(success)
            AdsManager.Instance.InterstitialClosed += ScenesManager.Instance.ToTheMainMenu;
        else 
            ScenesManager.Instance.ToTheMainMenu();
    }
}
