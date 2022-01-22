using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudManager : MonoBehaviour
{
    public GameObject area;
    public Slider shield;
    public GameObject star;
    public GameObject arrow;
    public GameObject time;

    public static HudManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de HudManager dans la scène");
            return;
        }
        instance = this;
    }

    public void init(string time, string arrow)
    {
        SetTime(time);
        SetArrow(arrow);
        area.SetActive(true);
    }

    public void SetShield(float value){
        shield.value = Mathf.Max(value, 1);
    }

    public void SetStar(string text){
        star.GetComponent<TMPro.TextMeshProUGUI>().text = text;
    }

    public void SetArrow(string text){
        arrow.GetComponent<TMPro.TextMeshProUGUI>().text = text;
    }

    public void SetTime(string text)
    {
        time.GetComponent<TMPro.TextMeshProUGUI>().text = text;
    }
}
