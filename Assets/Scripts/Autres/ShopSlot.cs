using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour {

	public Image icon;
    public TMPro.TextMeshProUGUI text;

    public TMPro.TextMeshProUGUI priceText;

	public Item item;

    public int price;

	public void AddItem (Item newItem, int price)
	{
        this.price = price;
		this.item = newItem;
        text.SetText(this.item.name.ToString());
        priceText.SetText(price.ToString());
		icon.sprite = this.item.icon;
		icon.enabled = true;
	}

	public void ClearSlot ()
	{
		this.item = null;
        text.SetText("");
		icon.sprite = null;
		icon.enabled = false;
	}

    public void buyItem() {
        if(PlayerData.getData().money >= price){
            PlayerData.getData().RemoveCredit(price);
            Inventory.instance.addItem(item.id);
            Debug.Log("Item acheté");
        } else {
            Debug.Log("peut pas");
        }
    }
}