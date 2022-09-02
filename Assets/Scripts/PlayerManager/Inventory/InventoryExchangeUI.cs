using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryExchangeUI : MonoBehaviour
{
	public Transform itemsParent;

    public TMPro.TextMeshProUGUI text;	

	isPlaying inventoryExchange;

	InventorySlot[] slots;

	/*void Start () {
		inventoryExchange = isPlaying.instance;
		inventoryExchange.onItemChangedCallback += UpdateUI;


		slots = itemsParent.GetComponentsInChildren<InventorySlot>();

		UpdateUI();
	}*/

    private void OnEnable()
    {
		if (inventoryExchange == null)
        {
			inventoryExchange = isPlaying.instance;
			inventoryExchange.onItemChangedCallback += UpdateUI;
			slots = itemsParent.GetComponentsInChildren<InventorySlot>();
		}
		UpdateUI();
    }

    void UpdateUI ()
	{
		for (int i = 0; i < slots.Length; i++)
		{
			if (i < inventoryExchange.inventory.Count)
			{
                Slot slot = inventoryExchange.inventory[i];
                Item item = Items.instance.items[slot.id];
                item.count = slot.count;
				slots[i].AddItem(item);
			} else
			{
				slots[i].ClearSlot();
			}
		}
        text.SetText("<color=#F8913F>"+inventoryExchange.inventory.Count+"</color> / " + slots.Length);
	}

	public void onCloseUI()
    {
		MenuManager.instance.CloseMenu("Exchange");
		PlayerData.getData().startInventory = true;
		PlayerData.getData().database.SaveData();
		if (!PlayerData.getData().startLevel)
        {
			MenuManager.instance.OpenMenu("tutorialLevel", 20);
			PlayerData.getData().startLevel = true;
			PlayerData.getData().database.SaveData();
		} else
        {
			isPlaying.instance.stats = Stats.inGame;
        }
		isPlaying.instance.ReloadArrow();
		HudManager.instance.SetPotion(isPlaying.instance.GetCount(4).ToString());
		HudManager.instance.SetChrono(isPlaying.instance.GetCount(5).ToString());
	}
}
