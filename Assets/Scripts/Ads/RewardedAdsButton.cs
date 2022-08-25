using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class RewardedAdsButton : MonoBehaviour
{
    [SerializeField] Button _showAdButton;
    [SerializeField] UnityEvent execute;


    void Awake()
    {   
        _showAdButton.interactable = false;
    }

    private void Start()
    {
        _showAdButton.onClick.AddListener(ShowAd);
        _showAdButton.interactable = true;
    }


    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log("Ad Loaded: " + adUnitId);
 
        
    }
 
    public void ShowAd()
    {
        AdsInitializer.instance.ShowRewardAd(OnAdsShowFailure, OnAdsShowComplete);
    }
 
    public void OnAdsShowComplete()
    {
        BaseEventData eventData = new BaseEventData(EventSystem.current);
        eventData.selectedObject = this.gameObject;

        if (execute != null)
            execute.Invoke();
    }
 
 
    public void OnAdsShowFailure()
    {
        Popup.instance.openPopup("Erreur", "Réessayer dans 10 secondes",20);
    }
}