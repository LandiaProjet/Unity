using System;
using System.Threading.Tasks;
using Unity.Services.Mediation;
using UnityEngine;
using Unity.Services.Core;
using UnityEngine.Events;

public class InterstitialAds : MonoBehaviour
{

    public static InterstitialAds interstitialAds;

    public UnityEvent OnEnd;

    public string androidAdUnitId;
    public string iosAdUnitId;
    IInterstitialAd interstitialAd;

    async void Start()
    {
        interstitialAds = this;

        // Instantiate an interstitial ad object with platform-specific Ad Unit ID
        if (Application.platform == RuntimePlatform.Android)
        {
            interstitialAd = MediationService.Instance.CreateInterstitialAd(androidAdUnitId);
        }
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            interstitialAd = MediationService.Instance.CreateInterstitialAd(iosAdUnitId);
        }
#if UNITY_EDITOR
        else
        {
            interstitialAd = MediationService.Instance.CreateInterstitialAd("myExampleAdUnitId");
        }

        try
        {
            // Load an ad:
            await interstitialAd.LoadAsync();
            // Here our load succeeded.

            // This is for demonstration purposes, we recommend you load the
            // ad in advance, and show when needed as load may take some time
            //await ShowAd();
        }
        catch (Exception e)
        {
            // Here our load failed.
        }
#endif
    }

    public async Task ShowAd()
    {
        // Ensure the ad has loaded, then show it.
        if (interstitialAd.AdState == AdState.Loaded)
        {
            try
            {
                await interstitialAd.ShowAsync();
                // Here show succeeded.
            }
            catch (Exception e)
            {
                // Here show failed.
            }
        }
        await DoEnd();
    }

    async Task DoEnd()
    {
        OnEnd.Invoke();
        OnEnd.RemoveAllListeners();
        await interstitialAd.LoadAsync();
    }
}
