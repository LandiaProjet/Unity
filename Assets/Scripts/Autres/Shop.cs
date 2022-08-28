using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class Shop : MonoBehaviour
{

    public ItemShop[] items;

    ShopSlot[] slots;

    public Transform itemsParent;

    public GameObject prefab;

    public GameObject inventoryDescription;

    private void Awake() {
        for (int i = 0; i < items.Length; i++)
		{
            GameObject obj = Instantiate(prefab);
            obj.transform.SetParent(itemsParent, false);
		}
        slots = itemsParent.GetComponentsInChildren<ShopSlot>();
        for (int i = 0; i < slots.Length; i++)
		{
            Debug.Log(Items.instance.items[items[i].id]);
            Item item = Items.instance.items[items[i].id];
            slots = itemsParent.GetComponentsInChildren<ShopSlot>();
			slots[i].AddItem(item, items[i].price);
            slots[i].btnInfo.onClick.AddListener(() => OpenDesription(item));
		}
    }

    public void CloseShopMenu(){
        MenuManager.instance.CloseMenu("Shop");
    }

    public void OpenDesription(Item item)
    {
        inventoryDescription.SetActive(true);
        InventoryItemDescriptionUI itemDescription = inventoryDescription.GetComponent<InventoryItemDescriptionUI>();
        itemDescription.itemName.SetText(LocalizationSettings.StringDatabase.GetLocalizedString(item.name));
        itemDescription.itemDescription.SetText(item.description);
        itemDescription.itemIcon.sprite = item.icon;
    }
}

[System.Serializable]
public class ItemShop {
    public int id;
    public int price;
}