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
        addLevel(0, 3, true);
        addLevel(1, 1, false);
        addLevel(2, 2, false);
        if (onChangedCallback != null)
            onChangedCallback.Invoke();
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
                TransitionManager.instance.loadingTransition.startLoading(1f);
                SceneManager.LoadScene(Levels.instance.levels[id].idScene);
                isPlaying.instance.stats = Stats.inGame;
                return true;
            }
        }
        return false;
    }

    public delegate void OnChanged();
	public OnChanged onChangedCallback;
}
