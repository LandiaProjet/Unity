using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Mission
{
    public int itemId;
    public int objectiveId;
    public int countMax;
    public int count;
}

public class MissionScript : MonoBehaviour
{
    public long timeStamp;

    private Database database;
    private List<Mission> missions = new List<Mission>();

    private void Start()
    {
        database = new Database("Missions.json", this);
    }

    public void addMission(int itemId, int objectiveId, int count)
    {
        if (itemId < 0 || itemId >= Items.instance.items.Length)
            return;
        
        Mission mission = new Mission { itemId = itemId, objectiveId = objectiveId, countMax = count, count = 0 };
        missions.Add(mission);
        database.SaveData();
    }

    public void deleteMission(int index)
    {
        if (missions.Count <= index || index < 0)
            return;
        missions.RemoveAt(index);
        database.SaveData();
    }

    public List<Mission> getMissions()
    {
        return missions;
    }
}
