using UnityEngine;

public class MissionScene : MonoBehaviour
{
    public GameObject prefabMission;
    public GameObject parent;

    private MissionScript mission;

    private void OnEnable()
    {
        MissionScript.instance.setValue(false);
        foreach (Transform child in parent.transform)
        {
            child.gameObject.GetComponent<MissionItem>().Refresh();
        }
    }

    private void Start()
    {
        if (!mission)
        {
            mission = MissionScript.instance;
            mission.scene = this;
        }
        foreach (Mission missionNode in mission.missions)
        {
            initItem(missionNode);
        }
    }

    public void initItem(Mission missionNode)
    {
        GameObject item = Instantiate(prefabMission, parent.transform);
        item.GetComponent<MissionItem>().Init(missionNode);
    }
}
