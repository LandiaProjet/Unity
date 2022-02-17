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
        SetMoneyText(string.Format("{0:#,0}", PlayerData.getData().money));
        //SetXpBar(PlayerManager.instance.levelSystem.GetExperienceNormalized());
    }

    private void FixedUpdate()
    {
        SetLevel(PlayerData.getData().level.ToString());
        SetMoneyText(string.Format("{0:#,0}", PlayerData.getData().money));
        SetHealText(PlayerData.getData().health.ToString());
        SetHealTimeText(PlayerData.getData().getTimeHealth());
    }

    public void PlayGame(){
        TransitionManager.instance.loadingTransition.startLoading(0.1f, true);
        MenuManager.instance.CloseMenu("MainMenu");
        SceneManager.LoadScene(1);
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
