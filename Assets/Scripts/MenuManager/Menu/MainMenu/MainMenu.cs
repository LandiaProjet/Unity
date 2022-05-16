using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Slider xpBar;
    public GameObject levelText;
    public GameObject notif;

    public GameObject moneyText;
    public GameObject healText;
    public GameObject healTimeText;
  
    private void Start() {
        SetLevel(GameManager.instance.GetLevelSystem().GetLevelNumber().ToString());
        SetExperience(GameManager.instance.GetLevelSystem().GetExperienceNormalized());
        MissionScript.instance.CreateDailyMission();
        SetMoneyText(string.Format("{0:#,0}", PlayerData.getData().money));
        InvokeRepeating("updateValue", 0.5f, 1f);
    }

    private void Update()
    {
        if (!notif.activeSelf && MissionScript.instance.isRead)
            notif.SetActive(true);
        if (notif.activeSelf && !MissionScript.instance.isRead)
            notif.SetActive(false);
        SetMoneyText(string.Format("{0:#,0}", PlayerData.getData().money));
    }

    private void updateValue()
    {
        SetHealText(PlayerData.getData().health.ToString());
        SetHealTimeText(PlayerData.getData().getTimeHealth());
    }

    public void PlayGame(){
        TransitionManager.instance.loadingTransition.startLoadingLevel(0.1f, true, 1);
        MenuManager.instance.CloseMenu("MainMenu");
    }
    
    public void ToggleSettings(bool isOpen){
        if(isOpen){
            MenuManager.instance.OpenMenu("Settings", 15);
        } else {
            MenuManager.instance.CloseMenu("Settings");
        }
    }
    public void ToggleMission(bool isOpen){
        if(isOpen){
            MenuManager.instance.OpenMenu("Mission", 15);
        } else {
            notif.SetActive(false);
            MenuManager.instance.CloseMenu("Mission");
        }
    }

    public void SetExperience(float value){
        xpBar.value = value;
    }

    public void SetLevel(string text){
        levelText.GetComponent<TMPro.TextMeshProUGUI>().SetText(text);
    }

    public void SetMoneyText(string text){
        moneyText.GetComponent<TMPro.TextMeshProUGUI>().SetText(text);
    }

    public void SetHealText(string text){
        healText.GetComponent<TMPro.TextMeshProUGUI>().SetText(text);
    }

    public void SetHealTimeText(string text){
        healTimeText.GetComponent<TMPro.TextMeshProUGUI>().SetText(text);
    }
}
