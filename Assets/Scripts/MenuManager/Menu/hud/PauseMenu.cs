using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public Image IconButton;
    public Sprite CloseIcon;
    public Sprite OpenIcon;

    public void toggleMenu()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    private void OnEnable()
    {
        IconButton.sprite = CloseIcon;
    }

    private void OnDisable()
    {
        IconButton.sprite = OpenIcon;
    }
}
