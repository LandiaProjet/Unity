using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission
{
    public int itemId;
    public int objectiveId;
    public int countMax;
    public int count;
}

public class MissionScript : MonoBehaviour
{
    public List<string> MissionsString;
    public long timeStamp;

    private Database database;
    private List<Mission> missions;

    private void Start()
    {
        database = new Database("Missions.json", this);
        InitMissions();
    }

    private void InitMissions()
    {
        missions = new List<Mission>();

        for (int i = 0; i < MissionsString.Count; i++)
        {
            Mission mission = JsonUtility.FromJson<Mission>(MissionsString[i]);
            missions.Add(mission);
        }
    }

    public void addMission(int itemId, int objectiveId, int count)
    {
        if (itemId < 0 || itemId >= Items.instance.items.Length)
            return;
        
        Mission mission = new Mission { itemId = itemId, objectiveId = objectiveId, countMax = count, count = 0 };
        missions.Add(mission);
        MissionsString.Add(JsonUtility.ToJson(mission));
        database.SaveData();
    }

    public void deleteMission(int index)
    {
        if (missions.Count <= index || index < 0)
            return;
        missions.RemoveAt(index);
        MissionsString.RemoveAt(index);
        database.SaveData();
    }

    public List<Mission> getMissions()
    {
        return missions;
    }
}
