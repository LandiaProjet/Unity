using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
public class InventoryUI : MonoBehaviour
{
	public Transform itemsParent;

    public TMPro.TextMeshProUGUI text;

	public GameObject inventoryDescription;

	Inventory inventory;

	InventorySlot[] slots;

	public static InventoryUI instance;
	
	private void Awake() {
		instance = this;
	}

	/*void Start () {
		inventory = Inventory.instance;
		inventory.onItemChangedCallback += UpdateUI;


		slots = itemsParent.GetComponentsInChildren<InventorySlot>();

		UpdateUI();
	}*/

    private void OnEnable()
    {
		if (inventory == null)
        {
			inventory = Inventory.instance;
			inventory.onItemChangedCallback += UpdateUI;
			slots = itemsParent.GetComponentsInChildren<InventorySlot>();
		}
		UpdateUI();
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

	public void OpenDesription(Item item){
		inventoryDescription.SetActive(true);
		InventoryItemDescriptionUI itemDescription = inventoryDescription.GetComponent<InventoryItemDescriptionUI>();
		itemDescription.itemName.SetText(LocalizationSettings.StringDatabase.GetLocalizedString(item.name));
		itemDescription.itemDescription.SetText(item.description);
		itemDescription.itemIcon.sprite = item.icon;
	}

	public void CloseDescription(){
		inventoryDescription.SetActive(false);
	}
    
}
