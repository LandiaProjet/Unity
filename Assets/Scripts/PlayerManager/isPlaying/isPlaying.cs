using System;
using System.Collections;
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

    public Vector2 lastPoint;
    public bool immunity;

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
                OnDefeat();
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
        immunity = false;
        star = 3;
        exp = 0;
        credit = 0;
        var ts = TimeSpan.FromSeconds(time);
        HudManager.instance.initGame(string.Format("{0:00}:{1:00}", ts.Minutes, ts.Seconds), "0", star);
    }

    public void endLevel()
    {
        stats = Stats.Ending;
        PlayerMovement.instance.rb.simulated = false;
        HudManager.instance.stopGame();
    }

    public void OnDefeat()
    {
        if (stats != Stats.inGame)
            return;
        endLevel();
        MenuManager.instance.OpenMenu("PopupDefeat", 10);
    }

    public void OnVictory()
    {
        if (stats != Stats.inGame)
            return;
        SlotLevel levelInfo;

        endLevel();
        levelInfo = LevelManager.instance.getLevel(idLevel);
        if (levelInfo != null && levelInfo.star < star)
            LevelManager.instance.editLevel(idLevel, star, true);
        TransferAllItemInMainInventory();
        if (Levels.instance.levels.Length > (idLevel + 1) && LevelManager.instance.getLevel(idLevel + 1) == null)
        {
            Level level = Levels.instance.levels[idLevel + 1];
            if (level.RequiredStar > LevelManager.instance.getCountStar())
            {
                Popup.instance.openPopup("Alerte", "Vous devez débloquer " + level.RequiredStar.ToString() + " pour pouvoir passer au niveau suivant.", 20);
            }
            else
            {
                LevelManager.instance.addLevel(idLevel + 1, 0, false);
            }
        }
        MenuManager.instance.OpenMenu("PopupVictory", 10);
    }

    public void TransferAllItemInMainInventory()
    {
        foreach (Slot slot in inventory)
        {
            for (int i = 0; i < slot.count; i++)
            {
                Inventory.instance.addItem(slot.id);
            }
        }
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
        if (stats != Stats.inGame || immunity)
            return;
        shield -= dommage;
        PlayerMovement.instance.HitAnimation();
        HudManager.instance.SetShield(shield);
        if (shield <= 0)
            OnDefeat();
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

    public IEnumerator StartImmunity(float time)
    {
        immunity = true;
        yield return new WaitForSeconds(time);
        immunity = false;
    }

    public void ReloadArrow()
    {
        foreach (Slot slot in inventory)
        {
            if (slot.id == 2)
            {
                HudManager.instance.SetArrow(slot.count.ToString());
                return;
            }
        }
        HudManager.instance.SetArrow("0");
    }

    public void RemoveArrow()
    {
        deleteItem(2);
        ReloadArrow();
    }
}
