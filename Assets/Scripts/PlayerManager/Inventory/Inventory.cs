using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Slot
{
    public int id;
    public int count;

}

public class Inventory : MonoBehaviour
{

    private Database database;

    public List<Slot> inventory = new List<Slot>();
    
    public static Inventory instance;
    public delegate void OnItemChanged();
	public OnItemChanged onItemChangedCallback;

    private void Awake() {
        instance = this;
    }

    private void Start()
    {
        Slot slot = new Slot { count = 5, id = 1 };
        inventory.Add(slot);
        database = new Database("Inventory.json", this);
        
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
                database.SaveData();
                if (onItemChangedCallback != null)
				    onItemChangedCallback.Invoke();
                return;
            }
        }
        Slot slot = new Slot { count = 1, id = id};
        inventory.Add(slot);
        database.SaveData();
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
                database.SaveData();
                if (onItemChangedCallback != null)
				    onItemChangedCallback.Invoke();
            }
        }
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

    public List<Slot> getInventory()
    {
        return inventory;
    }
}