using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUI : MonoBehaviour
{
	public Transform itemsParent;

	LevelManager levelManager;

    public Sprite completLevel;
    public Sprite defaultLevel;

    public Sprite completStar;
    public Sprite defaultStar;

	public GameObject prefab;

	LevelButton[] levelButtons;

	private int totalPage = 0;

	private int page = 0;

	private int pageItem = 4;

	public GameObject nextButton;
	public GameObject backButton;

	public GameObject pageNavigation;

	void Start () {
		levelManager = LevelManager.instance;

		Refresh();
	}

	public void StartLevel(int level){
		//si le level est égal au level débloquer
		Debug.Log("Start Level n°" + level);
		if(level == PlayerData.getData().level){
			
		}
	}

	private void OnEnable() {
		levelButtons = GetComponentsInChildren<LevelButton>();
	}

	public void ClickNext(){
		page+=1;
		Refresh();
		ChangeNavigation(page);
	}

	public void ClickBack(){
		page-=1;
		Refresh();
		ChangeNavigation(page);
	}

    public void CloseUI(){
        MenuManager.instance.CloseMenu("Level");
    }

	public void Refresh(){
		totalPage = levelManager.slotLevels.Count / pageItem;

		int index = page * pageItem;

		for(int i = 0; i < levelButtons.Length; i++){
			int level = index + i + 1;

			if(level <= levelManager.slotLevels.Count){
				levelButtons[i].gameObject.SetActive(true);
				levelButtons[i].Setup(this, level, levelManager.slotLevels[i].star, level<=2);
			}
		}

		CheckButton();
	}
    

	private void CheckButton(){
		backButton.SetActive(page > 0);
		nextButton.SetActive(page < totalPage);
	}

	private void ChangeNavigation(int page){
		switch(page){
			case 1:
				pageNavigation.transform.position = new Vector3(-75,0,0);
				break;
			case 2:
				pageNavigation.transform.position = new Vector3(-25,0,0);
				break;
			case 3:
				pageNavigation.transform.position = new Vector3(25,0,0);
				break;
			case 4:
				pageNavigation.transform.position = new Vector3(75,0,0);
				break;
		}
	}
}
