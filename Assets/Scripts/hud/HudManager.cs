using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudManager : MonoBehaviour
{

    public Slider shield;

    public Text star;

    public Text arrow;

    //Set le shield dans le menu qui ne peut pas dépasser 1
    public void SetShield(float value){
        shield.value = Mathf.Max(value, 1);
        Debug.Log("coucou");
    }

    public void SetStar(string text){
        star.text = text;
    }

    public void SetArrow(string text){
        star.text = text;
    }
    
    public void reset(){
        SetShield(1);
        SetStar("0");
        SetArrow("0");
    }
}
