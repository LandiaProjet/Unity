using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonManager : MonoBehaviour
{
    public GameObject player;
    public GameObject ButtonAttack;
    public GameObject Icon;

    public Image image;
    public Sprite sword;
    public Sprite arrow;

    public string mode = "";

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
        if(PlayerManager.instance.mode != mode){
            if (PlayerManager.instance.mode == "idle")
            {
                ButtonAttack.SetActive(false);
                mode = PlayerManager.instance.mode;
                return;
            }
            ButtonAttack.SetActive(true);
            if (PlayerManager.instance.mode == "sword") {
                image.sprite = sword;
            } else if (PlayerManager.instance.mode == "bow")
            {
                image.sprite = arrow;
            }
            mode = PlayerManager.instance.mode;
        }
    }
}
