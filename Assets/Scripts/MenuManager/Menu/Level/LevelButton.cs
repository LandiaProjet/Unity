using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public int idLevel;
    public Image Key;
    public Image Background;
    public Sprite Completed;
    public TMPro.TextMeshProUGUI Text;
    public GameObject Star;
    public Sprite StarCompleted;

    private SlotLevel levelInfo;
    private bool isActive = false;

    public void InitButton(int idLevel)
    {
        this.idLevel = idLevel;
        Text.text = this.idLevel.ToString();
        Text.enabled = false;
        Refresh();
    }

    public void Refresh()
    {
        if (LevelManager.instance.slotLevels.Count <= idLevel || idLevel < 0)
            return;
        Text.enabled = true;
        isActive = true;
        levelInfo = LevelManager.instance.slotLevels[idLevel];
        Key.enabled = false;
        if (!levelInfo.isFinish)
            return;
        Background.sprite = Completed;
        Star.SetActive(true);
        for (int i = 0; i < levelInfo.star; i++)
        {
            Star.transform.GetChild(i).gameObject.GetComponent<Image>().sprite = StarCompleted;
        }
    }

    public void onClick()
    {
        if (isActive == false)
        {
            Popup.instance.openPopup("Alerte", "Vous devez débloquer " + Levels.instance.levels[idLevel].RequiredStar.ToString() + " pour pouvoir passer au niveau suivant.", 20);
        }
        else
        {
            if (PlayerData.getData().health <= 0)
            {
                MenuManager.instance.OpenMenu("PopupNoHealth", 16);
            }
            else
            {
                Popup.instance.setButton("oui", ColorButton.Blue, () => {
                    Popup.instance.closePopup();
                    MenuManager.instance.CloseMenu("Level");
                    LevelManager.instance.openLevel(idLevel);
                }, 0);
                Popup.instance.setButton("non", ColorButton.Red, () => {
                    Popup.instance.closePopup();
                }, 1);
                Popup.instance.openPopup("Alerte", "être prêt ? Lorsque vous lancez la partie les items que vous mettrez en jeu ne pourront plus revenir jusqu'à la fin du jeu.", 20, 800);
            }
        }
    }
}
