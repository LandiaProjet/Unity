using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHeader : MonoBehaviour
{
    public TMPro.TextMeshProUGUI timeHealth;
    public TMPro.TextMeshProUGUI Health;
    public TMPro.TextMeshProUGUI Credit;
    public TMPro.TextMeshProUGUI CountStar;

    public void CloseLevelMenu()
    {
        MenuManager.instance.CloseMenu("Level");
    }

    private void FixedUpdate()
    {
        timeHealth.text = PlayerData.getData().getTimeHealth();
        Health.text = PlayerData.getData().health.ToString();
        Credit.text = string.Format("{0:#,0}", PlayerData.getData().money);
    }

    private void OnEnable()
    {
        CountStar.text = LevelManager.instance.getCountStar().ToString();
    }

    public void OpenShop()
    {
        MenuManager.instance.OpenMenu("Shop", 17);
    }

    public void OpenInventory()
    {
        MenuManager.instance.OpenMenu("Inventory", 18);
    }

    public void OpenMainMenu()
    {
        MenuManager.instance.CloseMenu("MainMenu");
        MenuManager.instance.OpenMenu("MainMenu", 15);
    }
}
