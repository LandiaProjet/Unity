using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{

    public ItemShop[] items;

    ShopSlot[] slots;

    public Transform itemsParent;

    public GameObject prefab;

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
		}
    }
}

[System.Serializable]
public class ItemShop {
    public int id;
    public int price;
}