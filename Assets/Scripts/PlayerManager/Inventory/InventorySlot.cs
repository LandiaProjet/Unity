using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour {

	public Image icon;
    public TMPro.TextMeshProUGUI text;

	Item item;

	public void AddItem (Item newItem)
	{
		this.item = newItem;
        text.SetText(this.item.count.ToString());
		icon.sprite = this.item.icon;
		icon.enabled = true;
        transform.GetChild(1).gameObject.SetActive(true);
	}

	public void ClearSlot ()
	{
		item = null;

        text.SetText("");
		icon.sprite = null;
		icon.enabled = false;
        transform.GetChild(1).gameObject.SetActive(false);
		
	}

	public void UseItem ()
	{
		if (this.item != null)
		{
			Debug.Log("test2");
            if(MenuManager.instance.isOpen("Exchange")){
				Debug.Log("test3");
				//action dans l'exchange;
				isPlaying.instance.addItem(item.id);
				Inventory.instance.deleteItem(item.id);
			} else {
				Debug.Log("test4");
				//action dans l'inventaire

			}
		}
	}

	public void UseItemExchange ()
	{
		if (this.item != null)
		{
            if(MenuManager.instance.isOpen("Exchange")){
				Debug.Log("test3");
				//action dans l'exchange;
				Inventory.instance.addItem(item.id);
				isPlaying.instance.deleteItem(item.id);
			} else {
				Debug.Log("test4");
				//action dans l'inventaire

			}
		}
	}

}