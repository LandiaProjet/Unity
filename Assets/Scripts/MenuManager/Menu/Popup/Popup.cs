using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public enum ColorButton
{
    BlueGreen,
    Brown,
    Green,
    Red,
    Purple,
    Blue
}
public class Popup : MonoBehaviour
{
    public static Popup instance;

    public GameObject popup;
    public GameObject buttonClose;
    public GameObject[] buttons;
    public TMPro.TextMeshProUGUI title;
    public TMPro.TextMeshProUGUI description;

    private RectTransform rectTransform;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de Popup dans la scène");
            return;
        }
        instance = this;
    }

    private void Start()
    {
        rectTransform = popup.GetComponent<RectTransform>();
        buttonClose.GetComponent<Button>().onClick.AddListener(() =>
        {
            closePopup();
        });
        // Exemple du popup
        /*setButton("oui", ColorButton.Blue, () => { }, 0);
        setButton("non", ColorButton.Red, () => { }, 1);
        openPopup("bonjour", "petite description", 8, 550);*/
    }

    public void setButton(string title, ColorButton color, UnityAction call, int zIndex)
    {
        GameObject button = buttons[(int)color];
        TMPro.TextMeshProUGUI titleButton = button.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        Button buttonEvent = button.GetComponent<Button>();

        button.transform.SetSiblingIndex(zIndex);
        titleButton.text = title;
        buttonEvent.onClick.RemoveAllListeners();
        buttonEvent.onClick.AddListener(call);
        button.SetActive(true);
    }

    public void openPopup(string title, string description, int zIndex = 0, int height = 450, int width = 810)
    {
        this.title.text = title;
        this.description.text = description;
        rectTransform.sizeDelta = new Vector2(width, height);
        MenuManager.instance.OpenMenu("Popup", zIndex);
    }

    public void closePopup()
    {
        foreach (GameObject button in buttons)
            if (button.activeSelf == true)
                button.SetActive(false);
        MenuManager.instance.CloseMenu("Popup");
    }
}
