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
    public Sprite sword;
    public Sprite arrow;

    public string mode;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

       
    }

    void Update() {
        if(PlayerManager.instance.mode != this.mode){
            if (PlayerManager.instance.mode == "idle")
            {
                ButtonAttack.SetActive(false);
                return;
            }
            ButtonAttack.SetActive(true);
            Image image = Icon.GetComponentInChildren<Image>();
            EventTrigger trigger = ButtonAttack.GetComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick;
            PlayerAttack playerAttack = player.GetComponent<PlayerAttack>();

            entry.callback.AddListener((data) => { playerAttack.Attack(); });
            trigger.triggers.Add(entry);
            if (PlayerManager.instance.mode == "sword")
            {
                image.sprite = sword;
            } else if (PlayerManager.instance.mode == "bow")
            {
                image.sprite = arrow;
            }
        }
    }
}
