using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Mission
{
    public int money;
    public int objectiveId;
    public int countMax;
    public int count;
}

public class MissionScript : MonoBehaviour
{
    public long timeStamp;
    public bool isRead;

    private Database database;
    public List<Mission> missions = new List<Mission>();
    public static MissionScript instance;
    [System.NonSerialized]
    public MissionScene scene;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de Mission dans la sc�ne");
            return;
        }
        instance = this;
    }

    private void Start()
    {
        database = new Database("Missions.json", this);
    }

    public Mission addMission(int money, int objectiveId, int count)
    {
        Mission mission = new Mission { money = money, objectiveId = objectiveId, countMax = count, count = 0 };
        missions.Add(mission);
        database.SaveData();
        return mission;
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

    int calculateSeconds()
    {
        DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Local);

        DateTime dtNow = DateTime.Now;

        TimeSpan result = dtNow.Subtract(dt);

        int seconds = Convert.ToInt32(result.TotalSeconds);

        return seconds;
    }

    public void CreateDailyMission()
    {
        int timeNow = calculateSeconds();

        if (timeNow >= timeStamp)
        {
            Mission missionNode = addMission(UnityEngine.Random.Range(5, 500), UnityEngine.Random.Range(0, Objectives.instance.objectives.Length - 1), UnityEngine.Random.Range(5, 50));
            timeStamp = timeNow + 86400;
            isRead = true;
            database.SaveData();
            if (scene)
                scene.initItem(missionNode);
        }
    }

    public void addValueMission(int type, int value)
    {
        foreach (Mission mission in missions)
        {
            if (type != mission.objectiveId)
                continue;
            mission.count += value;
        }
        database.SaveData();
    }

    public void setValue(bool value)
    {
        isRead = value;
        database.SaveData();
    }
}
