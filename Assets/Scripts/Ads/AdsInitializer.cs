using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections;
using Unity.Services.Core;
using Unity.Services.Mediation;
using System.Threading.Tasks;

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

    public string androidAdUnitId;
    public string iosAdUnitId;
    private bool success;
    IRewardedAd rewardedAd;

    private async void Awake()
    {
        instance = this;
        await UnityServices.InitializeAsync();
    }

    public async void Start()
    {
        success = false;
        if (Application.platform == RuntimePlatform.Android)
        {
            rewardedAd = new RewardedAd(androidAdUnitId);
        }
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            rewardedAd = new RewardedAd(iosAdUnitId);
        }
#if UNITY_EDITOR
        else
        {
            rewardedAd = new RewardedAd("myExampleAdUnitId");
        }
#endif

        //rewardedAd.OnLoaded += AdLoaded;
        //rewardedAd.OnFailedLoad += AdFailedToLoad;

        // Subscribe callback methods to show events:
        //rewardedAd.OnShowed += AdShown;
        //rewardedAd.OnFailedShow += AdFailedToShow;
        rewardedAd.OnUserRewarded += UserRewarded;
        rewardedAd.OnClosed += AdClosed;

        try
        {
            // Load an ad:
            await rewardedAd.LoadAsync();
            // Here our load succeeded.

            // This is for demonstration purposes, we recommend you load the
            // ad in advance, and show when needed as load may take some time
            //await ShowAd();
        }
        catch (Exception e)
        {
            // Here our load failed.
        }

    }

    public async Task ShowRewardAd(UnityAction OnFailure, UnityAction OnSuccess)
    {
        // Ensure the ad has loaded, then show it.
        if (rewardedAd.AdState == AdState.Loaded)
        {
            try
            {
                this.OnFailure.AddListener(OnFailure);
                this.OnSuccess.AddListener(OnSuccess);
                await rewardedAd.ShowAsync();
                Debug.Log("c bon");
            }
            catch (Exception e)
            {
                Debug.Log("erreur");
                await DoEnd();
            }
        } else
        {
            await DoEnd();
        }
    }

    void UserRewarded(object sender, RewardEventArgs args)
    {
        success = true;
        Debug.Log("success");
    }

    void AdClosed(object sender, EventArgs e)
    {
        Debug.Log("closed");
        StartCoroutine(WaitBeforeDoEnd());
    }

    private IEnumerator WaitBeforeDoEnd()
    {
        yield return new WaitForSeconds(0.1f);
        DoEnd();
    }

    private async Task DoEnd()
    {
        Debug.Log(success);
        if (success)
            OnSuccess.Invoke();
        else
            OnFailure.Invoke();
        await rewardedAd.LoadAsync();
        OnSuccess.RemoveAllListeners();
        OnFailure.RemoveAllListeners();
        success = false;
    }
}