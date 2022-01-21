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
    public List<Item> award;
    public List<Slot> inventory;
    public float time;

    public Stats stats;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de isPlaying dans la scène");
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
            Debug.Log(time);
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
                return;
            }
        }
        Slot slot = new Slot { count = 1, id = id };
        inventory.Add(slot);
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
    }
}
