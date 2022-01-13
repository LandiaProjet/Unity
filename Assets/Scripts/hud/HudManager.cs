using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudManager : MonoBehaviour
{

    public Slider shield;

    public GameObject star;

    public GameObject arrow;

    //Set le shield dans le menu qui ne peut pas dépasser 1
    public void SetShield(float value){
        shield.value = Mathf.Max(value, 1);
    }

    public void SetStar(string text){
        star.GetComponent<TMPro.TextMeshProUGUI>().text = text;
    }

    public void SetArrow(string text){
        arrow.GetComponent<TMPro.TextMeshProUGUI>().text = text;
    }
    
    public void reset(){
        SetShield(1);
        SetStar("0");
        SetArrow("0");
    }
}