using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
	public Transform itemsParent;

    public TMPro.TextMeshProUGUI text;	

	Inventory inventory;

	InventorySlot[] slots;

	void Start () {
		inventory = Inventory.instance;
		inventory.onItemChangedCallback += UpdateUI;


		slots = itemsParent.GetComponentsInChildren<InventorySlot>();
	}
	
	void UpdateUI ()
	{
		for (int i = 0; i < slots.Length; i++)
		{
			if (i < inventory.inventory.Count)
			{
                Slot slot = inventory.inventory[i];
                Item item = Items.instance.items[slot.id];
                item.count = slot.count;
				slots[i].AddItem(item);
			} else
			{
				slots[i].ClearSlot();
			}
		}
        text.SetText("<color=#F8913F>"+inventory.inventory.Count+"</color> / " + slots.Length);
	}

    public void CloseUI(){
        MenuManager.instance.CloseMenu("Inventory");
    }
    
}
