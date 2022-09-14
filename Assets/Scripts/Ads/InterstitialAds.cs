using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InterstitialAds : MonoBehaviour
{

    public static InterstitialAds interstitialAds;

    private InterstitialAd interstitialAd;
    public UnityEvent OnAdLoadedEvent;
    public UnityEvent OnAdFailedToLoadEvent;
    public UnityEvent OnAdOpeningEvent;
    public UnityEvent OnAdFailedToShowEvent;
    public UnityEvent OnUserEarnedRewardEvent;
    public UnityEvent OnAdClosedEvent;
    public UnityEvent OnEnd;

    void Start()
    {
        interstitialAds = this;
        LoadAd();
    }

    private AdRequest CreateAdRequest()
    {
        return new AdRequest.Builder()
            .Build();
    }
    private IEnumerator waitAfterReload()
    {
        yield return new WaitForSeconds(60.0f);
        LoadAd();
    }

    public void LoadAd()
    {
        Debug.Log("Requesting Interstitial ad.");

#if UNITY_EDITOR
            string adUnitId = "unused";
#elif UNITY_ANDROID
        string adUnitId = "ca-app-pub-1927631574874259/1068104007";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
        string adUnitId = "unexpected_platform";
#endif
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
        }

        interstitialAd = new InterstitialAd(adUnitId);

        // Add Event Handlers
        interstitialAd.OnAdLoaded += (sender, args) =>
        {
            Debug.Log("Interstitial ad loaded.");
            OnAdLoadedEvent.Invoke();
        };
        interstitialAd.OnAdFailedToLoad += (sender, args) =>
        {
            Debug.Log("Interstitial ad failed to load with error: " + args.LoadAdError.GetMessage());
            OnAdFailedToLoadEvent.Invoke();
            //StartCoroutine(waitAfterReload());
        };
        interstitialAd.OnAdOpening += (sender, args) =>
        {
            Debug.Log("Interstitial ad opening.");
            OnAdOpeningEvent.Invoke();
        };
        interstitialAd.OnAdClosed += (sender, args) =>
        {
            Debug.Log("Interstitial ad closed.");
            OnAdClosedEvent.Invoke();
            StartCoroutine(DoEnd());
        };
        interstitialAd.OnAdDidRecordImpression += (sender, args) =>
        {
            Debug.Log("Interstitial ad recorded an impression.");
        };
        interstitialAd.OnAdFailedToShow += (sender, args) =>
        {
            Debug.Log("Interstitial ad failed to show.");
            StartCoroutine(DoEnd());
        };
        interstitialAd.OnPaidEvent += (sender, args) =>
        {
            string msg = string.Format("{0} (currency: {1}, value: {2}",
                                        "Interstitial ad received a paid event.",
                                        args.AdValue.CurrencyCode,
                                        args.AdValue.Value);
            Debug.Log(msg);
        };
        interstitialAd.LoadAd(CreateAdRequest());
    }

    private void stopSound()
    {
        SoundManager.instance.mixer.SetFloat("MusicVolume", Mathf.Log10(0.00001f) * 20);
        SoundManager.instance.mixer.SetFloat("SFXVolume", Mathf.Log10(0.00001f) * 20);
    }

    private void restartSound()
    {
        SoundManager.instance.mixer.SetFloat("MusicVolume", Mathf.Log10(PlayerData.getData().music) * 20);
        SoundManager.instance.mixer.SetFloat("SFXVolume", Mathf.Log10(PlayerData.getData().sfx) * 20);
    }

    private IEnumerator DoEnd()
    {
        yield return new WaitForSeconds(0.01f);
        OnEnd.Invoke();
        OnEnd.RemoveAllListeners();
        restartSound();
        LoadAd();
    }

    public void ShowAd()
    {
        if (this.interstitialAd.IsLoaded())
        {
            stopSound();
            this.interstitialAd.Show();
        } else
        {
            StartCoroutine(DoEnd());
        }
    }
}
