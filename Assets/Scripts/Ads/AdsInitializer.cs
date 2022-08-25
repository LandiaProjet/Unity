using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;
using UnityEngine.Events;

public class AdsInitializer : MonoBehaviour
{

    public UnityEvent OnAdLoadedEvent;
    public UnityEvent OnAdFailedToLoadEvent;
    public UnityEvent OnAdOpeningEvent;
    public UnityEvent OnAdFailedToShowEvent;
    public UnityEvent OnUserEarnedRewardEvent;
    public UnityEvent OnAdClosedEvent;

    public UnityEvent OnSuccess;
    public UnityEvent OnFailure;

    public static AdsInitializer instance;

    private RewardedAd rewardedAd;

    private void Awake()
    {
        instance = this;
        MobileAds.Initialize(initStatus => { });
    }

    public void Start()
    {
        LoadRewardAd();
    }

    private AdRequest CreateAdRequest()
    {
        return new AdRequest.Builder()
            .Build();
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
        restartSound();
        LoadRewardAd();
        OnSuccess.RemoveAllListeners();
        OnFailure.RemoveAllListeners();
    }

    public void LoadRewardAd()
    {
        Debug.Log("Requesting Rewarded ad.");
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
        string adUnitId = "unexpected_platform";
#endif

        rewardedAd = new RewardedAd(adUnitId);

        rewardedAd.OnAdLoaded += (sender, args) =>
        {
            Debug.Log("Reward ad loaded.");
            OnAdLoadedEvent.Invoke();
        };
        rewardedAd.OnAdFailedToLoad += (sender, args) =>
        {
            Debug.Log("Reward ad failed to load.");
            OnAdFailedToLoadEvent.Invoke();
        };
        rewardedAd.OnAdOpening += (sender, args) =>
        {
            Debug.Log("Reward ad opening.");
            OnAdOpeningEvent.Invoke();
        };
        rewardedAd.OnAdFailedToShow += (sender, args) =>
        {
            Debug.Log("Reward ad failed to show with error: " + args.AdError.GetMessage());
            OnAdFailedToShowEvent.Invoke();
            OnFailure.Invoke();
            StartCoroutine(DoEnd());
        };
        rewardedAd.OnAdClosed += (sender, args) =>
        {
            Debug.Log("Reward ad closed.");
            OnAdClosedEvent.Invoke();
            StartCoroutine(DoEnd());
        };
        rewardedAd.OnUserEarnedReward += (sender, args) =>
        {
            Debug.Log("User earned Reward ad reward: " + args.Amount);
            OnUserEarnedRewardEvent.Invoke();
            OnSuccess.Invoke();
        };
        rewardedAd.OnAdDidRecordImpression += (sender, args) =>
        {
            Debug.Log("Reward ad recorded an impression.");
        };
        rewardedAd.OnPaidEvent += (sender, args) =>
        {
            string msg = string.Format("{0} (currency: {1}, value: {2}",
                                        "Rewarded ad received a paid event.",
                                        args.AdValue.CurrencyCode,
                                        args.AdValue.Value);
            Debug.Log(msg);
        };
        rewardedAd.LoadAd(CreateAdRequest());
    }

    public void ShowRewardAd(UnityAction OnFailure, UnityAction OnSuccess)
    {
        if (rewardedAd != null && rewardedAd.IsLoaded())
        {
            stopSound();
            this.OnFailure.AddListener(OnFailure);
            this.OnSuccess.AddListener(OnSuccess);
            rewardedAd.Show();
        } else
        {
            OnFailure.Invoke();
        }
    }
}