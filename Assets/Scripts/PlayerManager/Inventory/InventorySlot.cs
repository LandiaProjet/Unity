using UnityEngine;
using UnityEngine.UI;


public class InventorySlot : MonoBehaviour {

	public Image icon;
    public TMPro.TextMeshProUGUI text;

	Item item; 

	public void AddItem (Item newItem)
	{
		item = newItem;

        text.SetText(item.count.ToString());
		icon.sprite = item.icon;
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
		if (item != null)
		{
            
		}
	}

}