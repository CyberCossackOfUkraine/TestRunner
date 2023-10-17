using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
