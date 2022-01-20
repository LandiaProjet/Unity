using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUI : MonoBehaviour
{
	public Transform itemsParent;

	LevelManager levelManager;

	LevelSlot[] slots;

    public Sprite completLevel;
    public Sprite defaultLevel;

    public Sprite completStar;
    public Sprite defaultStar;

	void Start () {
		levelManager = LevelManager.instance;
		levelManager.onChangedCallback += UpdateUI;


		slots = itemsParent.GetComponentsInChildren<LevelSlot>();
	}
	
	void UpdateUI ()
	{
		for (int i = 0; i < slots.Length; i++)
		{
			if (i < levelManager.slotLevels.Count)
			{
                Debug.Log("UPDATE ICI");
                SlotLevel slot = levelManager.slotLevels[i];
				slots[i].AddLevel(slot, slot.isFinish ? completLevel : defaultLevel, completStar, defaultStar);
			} else
			{
                Debug.Log("UPDATE" + slots.Length);
				slots[i].ClearSlot(defaultLevel);
			}
		}
	}

    public void CloseUI(){
        MenuManager.instance.CloseMenu("Level");
    }
    
}
