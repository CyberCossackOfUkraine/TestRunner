using GoogleMobileAds.Api;
using UnityEngine;

public class RewardLifeAd : MonoBehaviour
{
    private void Awake()
    {
        MobileAds.Initialize(initStatus => { });
    }
    void Start()
    {
        AdMobScript.LoadRewardedAd();
    }

    public void RegisterEventHandlers(RewardedAd ad)
    {
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Rewarded ad full screen content closed.");
        };
    }
}