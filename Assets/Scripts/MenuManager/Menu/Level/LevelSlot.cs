using UnityEngine;
using UnityEngine.UI;



public class LevelSlot : MonoBehaviour
{
	SlotLevel slot;

    public void AddLevel(SlotLevel newSlot, Sprite sprite, Sprite completStar, Sprite defaultStar)
	{
		slot = newSlot;
		this.gameObject.GetComponent<Image>().sprite = sprite;
		for (int i = 0; i < slot.star; i++)
		{
			Debug.Log("ui"+i);
			transform.GetChild(1).GetChild(i).gameObject.GetComponent<Image>().sprite = completStar;
		}
	}

	public void ClearSlot (Sprite sprite)
	{
		slot = null;
		this.gameObject.GetComponent<Image>().sprite = sprite;
	}
}
