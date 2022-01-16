using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Slot
{
    public int id;
    public int count;
}

public class Inventory : MonoBehaviour
{
    public List<string> InventoryString;

    private Database database;

    private List<Slot> inventory;
    
    private void Start()
    {
        database = new Database("Inventory.json", this);
        InitInventory();
        // c'est pour les tests tu peux ajouter le addItem aussi pour tester
        deleteItem(0);
        // addItem(0);
    }

    private void InitInventory()
    {
        inventory = new List<Slot>();
        
        for (int i = 0; i < InventoryString.Count; i++)
        {
            Slot slot = JsonUtility.FromJson<Slot>(InventoryString[i]);
            inventory.Add(slot);
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
                InventoryString[i] = JsonUtility.ToJson(inventory[i]);
                database.SaveData();
                return;
            }
        }
        Slot slot = new Slot { count = 1, id = id};
        inventory.Add(slot);
        InventoryString.Add(JsonUtility.ToJson(slot));
        database.SaveData();
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
                    InventoryString.RemoveAt(i);
                }
                else
                {
                    inventory[i].count--;
                    InventoryString[i] = JsonUtility.ToJson(inventory[i]);
                }
                database.SaveData();
            }
        }
    }

    public List<Slot> getInventory()
    {
        return inventory;
    }
}
