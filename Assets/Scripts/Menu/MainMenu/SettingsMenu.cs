using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public GameObject languagePanel;

    public GameObject languageText;
    public Image languageFlag;

    public Sprite[] flags;

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
        languageFlag.sprite = flags[flagCount];
    }
}
