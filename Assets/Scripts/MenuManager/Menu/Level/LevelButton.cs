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
    }

    public void onClick()
    {
        if (isActive == false)
            return;
        MenuManager.instance.CloseMenu("Level");
        LevelManager.instance.openLevel(idLevel);
    }
}
