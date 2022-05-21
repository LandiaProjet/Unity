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

        //meme nombre d'fps sur toutes les platform
        Application.targetFrameRate = 30;

        foreach (var element in dontDestroyOnLoad)
        {
            DontDestroyOnLoad(element);
        }

        StartCoroutine(LoadLanguage());
    }

    public LevelSystem GetLevelSystem(){
        return levelSystem;
    }

    IEnumerator LoadLanguage()
    {
        yield return LocalizationSettings.InitializationOperation;
        if(PlayerData.getData().languageCode.Length > 0)
            LoadLocale(PlayerData.getData().languageCode);
    }

    public void LoadLocale(string languageIdentifier)
    {
        LocalizationSettings settings = LocalizationSettings.Instance;
        LocaleIdentifier localeCode = new LocaleIdentifier(languageIdentifier);//can be "en" "de" "ja" etc.

        for(int i = 0; i < LocalizationSettings.AvailableLocales.Locales.Count; i++)
        {
            Locale aLocale = LocalizationSettings.AvailableLocales.Locales[i];
            LocaleIdentifier anIdentifier = aLocale.Identifier;
            if(anIdentifier == localeCode)
            {
                LocalizationSettings.SelectedLocale = aLocale;
            }
        }
    }
}
