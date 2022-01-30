using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonManager : MonoBehaviour
{
    public GameObject ButtonSwitch;
    public GameObject ButtonAttack;
    public GameObject Icon;

    public Sprite sword;
    public Sprite arrow;

    private string mode = "";
    private GameObject player;
    private Image image;

    public static ButtonManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de ButtonManager dans la scène");
            return;
        }
        instance = this;
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        PlayerAttack playerAttack = player.GetComponent<PlayerAttack>();
        EventTrigger trigger = ButtonAttack.GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();

        image = Icon.GetComponentInChildren<Image>();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((data) => { playerAttack.Attack(); });
        trigger.triggers.Add(entry);
    }

    void Update() {
        if (isPlaying.instance.stats != Stats.inGame)
            return;
        if(PlayerManager.instance.mode != mode){
            checkButtonType();
        }
    }

    void checkButtonType()
    {
        if (PlayerManager.instance.mode == "idle")
        {
            ButtonAttack.SetActive(false);
            mode = PlayerManager.instance.mode;
            return;
        }
        ButtonAttack.SetActive(true);
        if (PlayerManager.instance.mode == "sword")
        {
            image.sprite = sword;
        }
        else if (PlayerManager.instance.mode == "bow")
        {
            image.sprite = arrow;
        }
        mode = PlayerManager.instance.mode;
    }

    public void ToggleAdditionalButton(bool value)
    {
        ButtonAttack.SetActive(value);
        ButtonSwitch.SetActive(value);
        checkButtonType();
    }
}
