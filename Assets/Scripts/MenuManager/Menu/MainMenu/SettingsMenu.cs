using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using System.Globalization;
public class SettingsMenu : MonoBehaviour
{
    public GameObject languagePanel;

    public GameObject languageText;
    public Image languageFlag;

    public ItemFlag[] flags;

    public Slider sfxSlider;
    public Slider musicSlider;

    private void Awake() {
        sfxSlider.onValueChanged.AddListener(setSFXVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        UpdateUI(PlayerData.getData().languageName);
    }

    public void OpenLanguage(){
        languagePanel.SetActive(true);
    }

    public void CloseLanguage(){
        languagePanel.SetActive(false);
    }

    public void ChangeLanguage(string name){
        UpdateUI(name);
        CloseLanguage();
    }

    public void UpdateUI(string name){
        GameObject Popup = languagePanel.transform.GetChild(1).gameObject;
        GameObject Contents = Popup.transform.GetChild(0).gameObject;
        int flagCount = 0;
        for (int i = 0; i < Contents.transform.childCount; i++)
        {
            GameObject childGameObject = Contents.transform.GetChild(i).gameObject;
            if(childGameObject.transform.GetChild(0).gameObject.activeSelf){
                childGameObject.transform.GetChild(0).gameObject.SetActive(false);
            }
            if (childGameObject.gameObject.name.Contains(name)){
                flagCount = i;
                childGameObject.transform.GetChild(0).gameObject.SetActive(true);
            }
        }
        languageText.GetComponent<TMPro.TextMeshProUGUI>().text = name;
        languageFlag.sprite = flags[flagCount].sprite;
        GameManager.instance.LoadLocale(flags[flagCount].code);
        PlayerData.getData().languageCode = flags[flagCount].code;
        PlayerData.getData().languageName = name;
        PlayerData.getData().database.SaveData();
    }

    public void SetMusicVolume(float value){
        SoundManager.instance.SetMusicVolume(value);
    }

    public void setSFXVolume(float value){
        SoundManager.instance.setSFXVolume(value);
    }

}

[System.Serializable]
public class ItemFlag{

    public Sprite sprite;

    public string code;

}