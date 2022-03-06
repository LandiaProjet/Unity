using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[System.Serializable]
public class SlotLevel
{
    public int id;
    public int star;
    public bool isFinish;
}

public class LevelManager : MonoBehaviour
{
    private Database database;
    public static LevelManager instance;

    public List<SlotLevel> slotLevels = new List<SlotLevel>();

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        database = new Database("Level.json", this);
        if (slotLevels.Count == 0)
            addLevel(0, 0, false);
    }

    public void addLevel(int id, int star, bool isFinish)
    {
        if (id < 0 || id >= Levels.instance.levels.Length)
            return;
        foreach (SlotLevel slotlevel in slotLevels)
            if (slotlevel.id == id)
                return;
        SlotLevel slot = new SlotLevel { id = id, isFinish = isFinish, star = star };
        slotLevels.Add(slot);
        database.SaveData();
    }

    public SlotLevel getLevel(int id)
    {
        if (id < 0 || id >= Levels.instance.levels.Length)
            return null;
        foreach (SlotLevel slotlevel in slotLevels)
        {
            if (slotlevel.id == id)
            {
                return slotlevel;
            }
        }
        return null;
    }

    public void editLevel(int id, int star, bool isFinish)
    {
        if (id < 0 || id >= Levels.instance.levels.Length)
            return;
        foreach (SlotLevel slotlevel in slotLevels)
        {
            if (slotlevel.id == id)
            {
                slotlevel.star = star;
                slotlevel.isFinish = isFinish;
                database.SaveData();
                return;
            }
        }
        return;
    }

    public bool openLevel(int id)
    {
        if (id < 0 || id >= Levels.instance.levels.Length)
            return false;
        foreach (SlotLevel slotlevel in slotLevels)
        {
            if (slotlevel.id == id)
            {
                TransitionManager.instance.loadingTransition.startLoadingLevel(1f, true , Levels.instance.levels[id].idScene);
                isPlaying.instance.startLevel(id);
                return true;
            }
        }
        return false;
    }

    public int getCountStar()
    {
        int Star = 0;

        foreach (SlotLevel slotLevel in slotLevels)
        {
            Star += slotLevel.star;
        }
        return Star;
    }
}
