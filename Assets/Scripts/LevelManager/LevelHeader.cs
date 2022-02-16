using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHeader : MonoBehaviour
{
    public TMPro.TextMeshProUGUI CountStar;

    public void CloseLevelMenu()
    {
        MenuManager.instance.CloseMenu("Level");
    }


    private void OnEnable()
    {
        CountStar.text = LevelManager.instance.getCountStar().ToString();
    }
}
