using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization.Settings;
using System.Collections.Generic;

public class LevelButton : MonoBehaviour
{
    public int idLevel;
    public Image Key;
    public Image Background;
    public Sprite Completed;
    public TMPro.TextMeshProUGUI Text;
    public GameObject Star;
    public Sprite StarCompleted;

    private SlotLevel levelInfo;
    private bool isActive = false;

    public void InitButton(int idLevel)
    {
        this.idLevel = idLevel;
        Text.text = this.idLevel.ToString();
        Text.enabled = false;
        Refresh();
    }

    public void Refresh()
    {
        if (LevelManager.instance.slotLevels.Count <= idLevel || idLevel < 0)
            return;
        Text.enabled = true;
        isActive = true;
        levelInfo = LevelManager.instance.slotLevels[idLevel];
        Key.enabled = false;
        if (!levelInfo.isFinish)
            return;
        Background.sprite = Completed;
        Star.SetActive(true);
        for (int i = 0; i < levelInfo.star; i++)
        {
            Star.transform.GetChild(i).gameObject.GetComponent<Image>().sprite = StarCompleted;
        }
    }

    public void onClick()
    {
        SoundManager.instance.PlayEffectSound(0);
        if (isActive == false)
        {
            Popup.instance.openPopup(LocalizationSettings.StringDatabase.GetLocalizedString("UI TEXT", "alert"), LocalizationSettings.StringDatabase.GetLocalizedString("UI TEXT", "Levellock", new List<object>{ Levels.instance.levels[idLevel].RequiredStar.ToString() }), 20);
        }
        else
        {
            PlayerData.getData().RemoveHealth();
            MenuManager.instance.CloseMenu("Level");
            InterstitialAds.interstitialAds.LoadAd();
            LevelManager.instance.openLevel(idLevel);
            InterstitialAds.interstitialAds.ShowAd();
            /*if (PlayerData.getData().health <= 0)
            {
                MenuManager.instance.OpenMenu("PopupNoHealth", 16);
            }
            else
            {
                PlayerData.getData().RemoveHealth();
                MenuManager.instance.CloseMenu("Level");
                InterstitialAds.interstitialAds.LoadAd();
                LevelManager.instance.openLevel(idLevel);
                InterstitialAds.interstitialAds.ShowAd();
            }*/
        }
    }
}
