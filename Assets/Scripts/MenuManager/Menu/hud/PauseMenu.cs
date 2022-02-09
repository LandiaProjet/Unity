using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public Image IconButton;
    public Sprite CloseIcon;
    public Sprite OpenIcon;

    public static PauseMenu instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de PauseMenu dans la scene");
            return;
        }
        instance = this;
    }

    public void toggleMenu()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    private void OnEnable()
    {
        if (isPlaying.instance.stats == Stats.inGame)
            isPlaying.instance.stats = Stats.inPause;
        IconButton.sprite = CloseIcon;
    }

    private void OnDisable()
    {
        if (isPlaying.instance.stats == Stats.inPause)
            isPlaying.instance.stats = Stats.inGame;
        IconButton.sprite = OpenIcon;
    }

    public void onLeave()
    {
        MenuManager.instance.CloseMenu("PauseMenu");
        endingLevel.instance.BackToHome();
        HudManager.instance.stopGame();
    }

    public void openMission()
    {
        MenuManager.instance.OpenMenu("Mission", 15);
    }
}
