using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HudManager : MonoBehaviour
{
    public GameObject areaInformation;
    public Slider shield;
    public GameObject star;
    public GameObject arrow;
    public GameObject time;

    public GameObject potion;
    public GameObject chrono;

    public GameObject itemArea;

    public GameObject PopupPotion;
    public GameObject PopupChrono;

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

    public void initGame(string time, string arrow, int star, float shield, int potion, int chrono)
    {
        SetShield(shield);
        SetTime(time);
        SetArrow(arrow);
        SetStar(star.ToString());
        SetChrono(chrono.ToString());
        SetPotion(potion.ToString());
        areaInformation.SetActive(true);
        itemArea.SetActive(true);
        ButtonManager.instance.ToggleAdditionalButton(true);
    }

    public void RecoveryGame()
    {
        areaInformation.SetActive(true);
        itemArea.SetActive(true);
        ButtonManager.instance.ToggleAdditionalButton(true);
        SetShield(100);
    }

    public void stopGame()
    {
        areaInformation.SetActive(false);
        itemArea.SetActive(false);
        ButtonManager.instance.ToggleAdditionalButton(false);
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

    public void SetPotion(string text)
    {
        potion.GetComponent<TMPro.TextMeshProUGUI>().text = text;
    }

    public void SetChrono(string text)
    {
        chrono.GetComponent<TMPro.TextMeshProUGUI>().text = text;
    }

    public int getPotion()
    {
        return Int32.Parse(potion.GetComponent<TMPro.TextMeshProUGUI>().text);
    }
    public int getChrono()
    {
        return Int32.Parse(chrono.GetComponent<TMPro.TextMeshProUGUI>().text);
    }

    public int getArrow()
    {
        return Int32.Parse(arrow.GetComponent<TMPro.TextMeshProUGUI>().text);
    }

    public void SetTime(string text)
    {
        time.GetComponent<TMPro.TextMeshProUGUI>().text = text;
    }

    IEnumerator WaitBeforeDisable(GameObject wanted, float time)
    {
        wanted.SetActive(true);
        yield return new WaitForSeconds(time);
        wanted.SetActive(false);
    }

    public void EnablePopupPotion()
    {
        StartCoroutine(WaitBeforeDisable(PopupPotion, 5f));
    }

    public void EnablePopupChrono()
    {
        StartCoroutine(WaitBeforeDisable(PopupChrono, 5f));
    }
}
