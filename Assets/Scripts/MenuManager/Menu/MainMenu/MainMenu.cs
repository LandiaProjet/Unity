using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public GameObject settingsPanel;

    public GameObject missionPanel;

    public Slider xpBar;
    public GameObject levelText;

    public GameObject moneyText;
    public GameObject healText;
    public GameObject healTimeText;
  
    private void Start() {
        SetXpBar(PlayerManager.instance.playerData.experience);
        SetLevel(PlayerManager.instance.playerData.level.ToString());
        SetHealText(PlayerManager.instance.playerData.health.ToString());
        SetHealTimeText("500");
        SetMoneyText("1000");
    }

    public void PlayGame(){
        SceneManager.LoadScene(1);
    }
    
    public void OpenSettings(){
        settingsPanel.SetActive(true);
    }

    public void CloseSettings(){
        settingsPanel.SetActive(false);
    }

    public void OpenMission(){
        missionPanel.SetActive(true);
    }

    public void CloseMission(){
        missionPanel.SetActive(false);
    }

    public void SetXpBar(float value){
        xpBar.value = value;
    }

    public void SetLevel(string text){
        levelText.GetComponent<TMPro.TextMeshProUGUI>().text = text;
    }

    public void SetMoneyText(string text){
        moneyText.GetComponent<TMPro.TextMeshProUGUI>().text = text;
    }

    public void SetHealText(string text){
        healText.GetComponent<TMPro.TextMeshProUGUI>().text = text;
    }

    public void SetHealTimeText(string text){
        healTimeText.GetComponent<TMPro.TextMeshProUGUI>().text = text;
    }
}