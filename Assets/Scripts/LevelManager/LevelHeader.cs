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
    }

    private void OnEnable()
    {
        Credit.text = string.Format("{0:#,0}", PlayerData.getData().money);
        Health.text = PlayerData.getData().health.ToString();
        CountStar.text = LevelManager.instance.getCountStar().ToString();
    }
}
