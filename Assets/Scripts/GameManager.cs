using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class GameManager : MonoBehaviour
{
    public GameObject[] dontDestroyOnLoad;
    
    public static GameManager instance;

    public LevelSystem levelSystem = new LevelSystem();

    private void Awake() {
        instance = this;
       // LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.GetLocale(new LocaleIdentifier(Application.systemLanguage));

        foreach (var element in dontDestroyOnLoad)
        {
            DontDestroyOnLoad(element);
        }
    }

    public LevelSystem GetLevelSystem(){
        return levelSystem;
    }
}
