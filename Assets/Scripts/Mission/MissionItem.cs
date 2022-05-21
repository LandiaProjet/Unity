using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization.Settings;

public class MissionItem : MonoBehaviour
{
    public TMPro.TextMeshProUGUI description;
    public TMPro.TextMeshProUGUI money;
    public Image icon;
    public Slider slider;
    public GameObject button;

    private Objective objective;
    private Mission mission;

    public void Init(Mission item)
    {
        if (item.objectiveId < 0 || item.objectiveId >= Objectives.instance.objectives.Length)
            return;
        mission = item;
        objective = Objectives.instance.objectives[item.objectiveId];
        description.text = LocalizationSettings.StringDatabase.GetLocalizedString("UI TEXT", objective.description, new List<object>{ item.countMax.ToString() });
        //description.text = objective.description.Replace("-X-", item.countMax.ToString());
        money.text = item.money.ToString();
        icon.sprite = objective.sprite;
        slider.value = item.count * 100 / item.countMax;
        if (item.count >= item.countMax)
            button.SetActive(true);
    }

    public void Refresh()
    {
        slider.value = mission.count * 100 / mission.countMax;
        if (mission.count >= mission.countMax)
            button.SetActive(true);

        description.text = LocalizationSettings.StringDatabase.GetLocalizedString("UI TEXT", objective.description, new List<object>{ mission.countMax.ToString() });
        
    }

    public void OnClick()
    {
        int index = MissionScript.instance.missions.FindIndex((e) => e == mission);
        PlayerData.getData().AddCredit(mission.money);
        MissionScript.instance.deleteMission(index);
        Destroy(gameObject);
    }
}
