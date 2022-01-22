using UnityEngine;
using UnityEngine.UI;



public class LevelButton : MonoBehaviour
{

	private Image image;

	public GameObject lockButton;
	public GameObject starButton;

	public LevelUI levelUI;

	private void Start() {
		image = this.gameObject.GetComponent<Image>();	
	}

	private int level = 0;
	public TMPro.TextMeshProUGUI text;
    public void Setup(LevelUI levelUI, int level, int starCount, bool isUnLock)
	{
		this.level = level;
		this.levelUI = levelUI;

		if(isUnLock){
			lockButton.gameObject.SetActive(false);
			text.gameObject.SetActive(true);
			starButton.gameObject.SetActive(true);
			image.sprite = levelUI.completLevel;
			for (int i = 0; i < starCount; i++)
			{
				transform.GetChild(1).GetChild(i).gameObject.GetComponent<Image>().sprite = levelUI.completStar;
			}
			text.SetText(level.ToString());
		} else {
			lockButton.gameObject.SetActive(true);
			text.gameObject.SetActive(false);
			starButton.gameObject.SetActive(false);
			image.sprite = levelUI.defaultLevel;
		}
	}

	public void OnClick(){
		levelUI.StartLevel(level);
	}

}
