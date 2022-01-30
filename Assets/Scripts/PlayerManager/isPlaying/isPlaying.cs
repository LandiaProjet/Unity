using System;
using System.Collections.Generic;
using UnityEngine;

public enum Stats
{
    Starting,
    Ending,
    inGame,
    inPause,
    notHere
}

public class isPlaying : MonoBehaviour
{
    public static isPlaying instance;

    public float shield;
    public List<Slot> award = new List<Slot>();
    public List<Slot> inventory = new List<Slot>();
    public float time;
    public int star;
    public int credit;
    public int exp;

    public int idLevel;

    public Stats stats;

    public delegate void OnItemChanged();
	public OnItemChanged onItemChangedCallback;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de isPlaying dans la sc�ne");
            return;
        }
        instance = this;
    }

    private void Start()
    {
        stats = Stats.notHere;
    }

    private void FixedUpdate()
    {
        if (stats == Stats.inGame)
        {
            time -= Time.fixedDeltaTime;
            if (star != 0 && Levels.instance.levels[idLevel].timeStar[star - 1] > time)
            {
                star--;
                HudManager.instance.SetStar(star.ToString());
            }
            if (star < 3 && Levels.instance.levels[idLevel].timeStar[star] < time)
            {
                star++;
                HudManager.instance.SetStar(star.ToString());
            }
            var ts = TimeSpan.FromSeconds(time);
            HudManager.instance.SetTime(string.Format("{0:00}:{1:00}", ts.Minutes, ts.Seconds));
            if (time <= 0)
                endLevel();
        }
    }

    public void startLevel(int idLevel)
    {
        award = new List<Slot>();
        inventory = new List<Slot>();
        this.idLevel = idLevel;
        shield = 100f;
        instance.stats = Stats.Starting;
        MenuManager.instance.OpenMenu("Exchange", 10);
        time = Levels.instance.levels[idLevel].secondTimeMax;
        star = 3;
        exp = 0;
        credit = 0;
        var ts = TimeSpan.FromSeconds(time);
        HudManager.instance.SetStar(star.ToString());
        HudManager.instance.initGame(string.Format("{0:00}:{1:00}", ts.Minutes, ts.Seconds), "0");
    }

    public void endLevel()
    {
        stats = Stats.Ending;
        PlayerMovement.instance.setDie(true);
        HudManager.instance.stopGame();
        MenuManager.instance.OpenMenu("PopupDefeat", 10);
    }

    public void addItem(int id)
    {
        if (id < 0 || id >= Items.instance.items.Length)
            return;
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].id == id)
            {
                inventory[i].count++;
                if (onItemChangedCallback != null)
                    onItemChangedCallback.Invoke();
                return;
            }
        }
        Slot slot = new Slot { count = 1, id = id };
        inventory.Add(slot);
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

    public void deleteItem(int id)
    {
        if (id < 0 || id >= Items.instance.items.Length)
            return;
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].id == id)
            {
                if (inventory[i].count == 1)
                {
                    inventory.RemoveAt(i);
                }
                else
                {
                    inventory[i].count--;
                }
            }
        }
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

    public int GetCount(int id){
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].id == id)
            {
                return inventory[i].count;
            }
        }
        return 0;
    }

    public void addDommage(float dommage)
    {
        if (stats != Stats.inGame)
            return;

        shield -= dommage;

        HudManager.instance.SetShield(shield);
        if (shield <= 0)
            endLevel();
    }

    public void addHealth(int health)
    {
        if (stats != Stats.inGame)
            return;

        shield += health;

        HudManager.instance.SetShield(shield);
        if (shield > 100)
            shield = 100;
    }
}
