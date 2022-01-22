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

    public Stats stats;

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
            time += Time.fixedDeltaTime;
            //teststart
            StartCoroutine(dommage());
            //testend
        }
    }

    //functiontest
    IEnumerator dommage()
    {
        while (shield >= 0)
        {
            yield return new WaitForSeconds(1f);
            addDommage(1f);
        }
    }

    public void startLevel()
    {
        time = 0;
        shield = 100f;
        instance.stats = Stats.inGame;
    }

    public void addItem(int id, List<Slot> inventory)
    {
        if (id < 0 || id >= Items.instance.items.Length)
            return;
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].id == id)
            {
                inventory[i].count++;
                return;
            }
        }
        Slot slot = new Slot { count = 1, id = id };
        inventory.Add(slot);
    }

    public void deleteItem(int id, List<Slot> inventory)
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
    }

    public void addDommage(float dommage)
    {
        if (stats != Stats.inGame)
            return;

        shield -= dommage;

        HudManager.instance.SetShield(shield);
        if (shield <= 0)
        {
            stats = Stats.Ending;
            PlayerMovement.instance.setDie(true);
        }
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
