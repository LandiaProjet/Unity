using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScene : MonoBehaviour
{
    void Start()
    {
        MenuManager.instance.OpenMenu("MainMenu");
        TransitionManager.instance.loadingTransition.startLoading(0.1f, false);
       // MenuManager.instance.OpenMenu("PauseMenu", 8);
    }
}
