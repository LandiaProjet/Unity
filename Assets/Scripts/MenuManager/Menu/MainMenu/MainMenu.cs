using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Slider xpBar;
    public GameObject levelText;

    public GameObject moneyText;
    public GameObject healText;
    public GameObject healTimeText;
  
    private void Start() {
        SetLevel(PlayerData.getData().level.ToString());
        SetHealText(PlayerData.getData().health.ToString());
        SetHealTimeText("FULL");
        SetMoneyText(string.Format("{0:#,0}", PlayerData.getData().money));
        //SetXpBar(PlayerManager.instance.levelSystem.GetExperienceNormalized());
    }

    public void PlayGame(){
        TransitionManager.instance.loadingTransition.startLoading(2f, true);
        MenuManager.instance.CloseMenu("MainMenu");
        SceneManager.LoadScene(1);
    }
    
    public void ToggleSettings(bool isOpen){
        if(isOpen){
            MenuManager.instance.OpenMenu("Settings", 5);
        } else {
            MenuManager.instance.CloseMenu("Settings");
        }
    }
    public void ToggleMission(bool isOpen){
        if(isOpen){
            MenuManager.instance.OpenMenu("Mission", 5);
        } else {
            MenuManager.instance.CloseMenu("Mission");
        }
    }

    public void SetXpBar(float value){
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
