using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openTutorial : MonoBehaviour
{
    void Start()
    {
        if (!PlayerData.getData().startLevel)
        {
            MenuManager.instance.OpenMenu("TutorialStart", 20);
            PlayerData.getData().startLevel = true;
            PlayerData.getData().database.SaveData();
        }
    }
}
